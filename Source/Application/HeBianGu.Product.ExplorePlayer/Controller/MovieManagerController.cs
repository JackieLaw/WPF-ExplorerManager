using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.MvcRespository;
using HeBianGu.ExplorePlayer.Base.Model;
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
    class MovieManagerController : EntityBaseController<mbc_dv_movie, MovieManagerViewModel, MovieRespository>
    {
        CaseRespository _caseRespository;
        public MovieManagerController(CaseRespository caseRespository)
        {
             _caseRespository= caseRespository;
    }

        public override async Task<IActionResult> List()
        {

            if (this.ViewModel.SelectCase == null)
            {
                return View();
            }

            var from = await this.Respository.GetListAsync(l => l.CaseType == this.ViewModel.SelectCase.ID);

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
            this.ViewModel.GroupObject = this._caseRespository.GetGroupObject().Result;

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

            //if (this.ViewModel.SelectCase.State==1)
            //{
            //    MessageService.ShowSnackMessageWithNotice("当前案例已经加载过了！");
            //    return await List();
            //}

            await this.Respository.RefreshMovie(this.ViewModel.SelectCase);

            return await List();
        }

        public async Task<IActionResult> Convert()
        {
            if (this.ViewModel.SelectedItem == null)
            {
                MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                return await List();
            }

            if (!File.Exists(this.ViewModel.SelectedItem.Url))
            {
                MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                return await List();
            }


            try
            {
                await this.Respository.ConvertMovie(this.ViewModel.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageService.ShowSnackMessageWithNotice(ex.Message);
            }


            return await List();
        }

        public async Task<IActionResult> RemoveAll()
        {
            if (this.ViewModel.SelectCase == null)
            {
                return View();
            }

            var from = await this.Respository.GetListAsync(l => l.CaseType == this.ViewModel.SelectCase.ID);

            foreach (var item in from)
            {
                await this.Respository.DeleteAsync(item);
            }

            this.Invoke(()=> this.ViewModel.Collection.Clear());

            return await List();
        }

        public async Task<IActionResult> ConvertAll()
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

            Action<IStringProgress> actionProgress = async l =>
            {

                for (int i = 0; i < this.ViewModel.Collection.Count; i++)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        l.MessageStr = $"正在转换第{i + 1}条视频，共计{this.ViewModel.Collection.Count}条视频";
                    });

                    var movie = this.ViewModel.Collection[i];

                    try
                    {
                        if (!string.IsNullOrEmpty(movie.Image))
                        {
                            MessageService.ShowNotifyMessage("该视频已经转换！");
                            continue;
                        }

                        await this.Respository.ConvertMovie(movie);
                    }
                    catch (Exception ex)
                    {
                        MessageService.ShowNotifyMessage(ex.Message);
                    }

                }
            };

            await MessageService.ShowStringProgress(actionProgress);

            return await List();
        }
    }
}
