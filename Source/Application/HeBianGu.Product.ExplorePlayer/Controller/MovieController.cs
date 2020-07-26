using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using HeBianGu.Domain.MvcRespository;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Movie")]
    internal class MovieController : Controller<MovieViewModel, MovieRespository>
    {
        public async Task<IActionResult> List()
        {
            //  Do ：只运行一遍
            this.RunOnlyInitailizing(()=>
            {
                //  Do ：异步运行
                this.RunAsync(async () =>
                {
                    var cases = await this.ViewModel.Service2.GetListAsync();

                    this.ViewModel.Cases.BeginInvoke(l => l.Clear());

                    foreach (var item in cases)
                    {
                        this.ViewModel.Cases.BeginInvoke(l =>
                        {
                            l.Add(item);
                        });

                        Thread.Sleep(5);
                    }

                    this.ViewModel.SelectCase = this.ViewModel.Cases?.FirstOrDefault();

                    var tags = await this.ViewModel.Service1.GetListAsync();

                    this.ViewModel.TagCollection.BeginInvoke(l => l.Clear());

                    foreach (var item in tags)
                    {
                        this.ViewModel.TagCollection.BeginInvoke(l =>
                        {
                            l.Add(item);
                        });

                        Thread.Sleep(50);
                    }

                });
            });

            return await this.ViewAsync();
        }
    }
}
