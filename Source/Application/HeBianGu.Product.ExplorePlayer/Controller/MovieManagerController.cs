using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("MovieManager")]
    class MovieManagerController : Controller<MovieManagerViewModel, MovieRespository, CaseRespository>
    {
        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {

            if (this.ViewModel.SelectCase == null)
            {
                return View();
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
                    this.ViewModel.Collection.Add(item);
                }
            });

            return View();
        }

        public async Task<IActionResult> Left()
        {
            this.ViewModel.GroupObject = this.Extend.GetGroupObject().Result;

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


            if (this.ViewModel.SelectCase.State==1)
            {
                MessageService.ShowSnackMessageWithNotice("当前案例已经加载过了！");
                return await List();
            }



            await this.Respository.RefreshMovie(this.ViewModel.SelectCase);

            return await List();
        }

        public async Task<IActionResult> Convert()
        {
            if (this.ViewModel.SeletItem == null)
            {
                MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                return await List();
            }

            if (!File.Exists(this.ViewModel.SeletItem.Url))
            {
                MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                return await List();
            }


            try
            {
                await this.Respository.ConvertMovie(this.ViewModel.SeletItem);
            }
            catch (Exception ex)
            {
                MessageService.ShowSnackMessageWithNotice(ex.Message);
            }


            return await List();
        }

        public async Task<IActionResult> Detial()
        {
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        public async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SeletItem, out message))
            {
                MessageService.ShowSnackMessage(message);
                return await Edit();
            }

            await this.Respository.UpdateAsync(this.ViewModel.SeletItem);

            return await List();
        }
    }
}
