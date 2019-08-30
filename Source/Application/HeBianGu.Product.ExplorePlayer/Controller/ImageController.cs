using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.MvcRespository;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Image")]
    class ImageController : Controller<ImageViewModel, ImageRespository>
    {
        TagRespository _tagRespository;

        ClipBoardService _clipBoardService;

        CaseRespository _caseRespository;

        public ImageController(TagRespository tagRespository, ClipBoardService clipBoardService, CaseRespository caseRespository)
        {
            _tagRespository = tagRespository;

            _clipBoardService = clipBoardService;

            _caseRespository = caseRespository;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _clipBoardService.Register(Application.Current.MainWindow);
            });


        }
        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var tags = _tagRespository.GetListAsync()?.Result;

            this.Invoke(() =>
            {
                this.ViewModel.TagCollection.Clear();

                foreach (var item in tags)
                {
                    this.ViewModel.TagCollection.Add(item);
                }
            });


            if (this.ViewModel.SelectCase == null)
            {
                var cases = await this._caseRespository.GetListAsync();

                var select = cases.FirstOrDefault();

                if (select == null)
                {

                    return View();
                }
                else
                {
                    this.ViewModel.SelectCase = select;
                }
            }

            var from = this.Respository.GetListAsync(l => l.CaseType == this.ViewModel.SelectCase.ID).Result;

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Clear();
            });

            if (from == null)
            {
                return View();
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in from)
                {
                    ImageModelViewModel viewModel = new ImageModelViewModel(item);
                    this.ViewModel.Collection.Add(viewModel);
                }
            });

            return View();
        }



        public async Task<IActionResult> Left()
        {

            this.ViewModel.GroupObject.Clear();

            this.ViewModel.GroupObject = await this._caseRespository.GetGroupObject();

            return View();
        }

        public async Task<IActionResult> Right()
        {
            return View();
        }


        public async Task<IActionResult> Refresh()
        {
            if (this.ViewModel.SelectCase == null)
            {
                MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                return await List();
            }

            if (!Directory.Exists(this.ViewModel.SelectCase.BaseUrl))
            {
                MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                return await List();
            }

            await this.Respository.RefreshImage(this.ViewModel.SelectCase);

            return await List();
        }
    }
}
