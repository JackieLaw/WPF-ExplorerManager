using HeBianGu.Base.WpfBase;
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
    class MovieManagerController : ExtendEntityBaseController<mbc_dv_movie, MovieManagerViewModel, MovieRespository, CaseRespository>
    {
        //public async Task<IActionResult> Center()
        //{
        //    return View();
        //}

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
            this.ViewModel.GroupObject = this.Extend.GetGroupObject().Result;

            return View();
        }

        //public async Task<IActionResult> Delete()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Edit()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Update()
        //{
        //    string message;

        //    if (!this.ModelState(this.ViewModel.SeletItem, out message))
        //    {
        //        MessageService.ShowSnackMessage(message);
        //        return await Edit();
        //    }

        //    await this.Respository.UpdateAsync(this.ViewModel.SeletItem);

        //    return await List();
        //}
    }
}
