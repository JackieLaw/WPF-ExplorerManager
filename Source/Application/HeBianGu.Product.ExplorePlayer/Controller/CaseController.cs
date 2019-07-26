using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Case")]
    class CaseController : Controller<CaseViewModel>
    {
        public async Task<IActionResult> List()
        {
            var models = await this.ViewModel.Respository.GetListAsync();

            if (models == null)
            {
                return View();
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Clear();

                foreach (var item in models)
                {
                    this.ViewModel.Collection.Add(item);
                }

            });


            return View();
        }

        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> Insert()
        {
            string message;

            if (!this.ModelState(this.ViewModel.AddCase, out message))
            {
                MessageService.ShowSnackMessage(message);
                return await Add();
            }

            await this.ViewModel.Respository.InsertAsync(this.ViewModel.AddCase);

            return await List();
        }


        public async Task<IActionResult> Add()
        {

            this.ViewModel.AddCase = new mbc_dc_case();

            return View();
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            await this.ViewModel.Respository.DeleteAsync(this.ViewModel.SeletItem?.ID);

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Remove(this.ViewModel.SeletItem);
            });

            return await List();
        }

        public async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SeletItem, out message))
            {
                MessageService.ShowSnackMessage(message);
                return await Edit();
            }

            await this.ViewModel.Respository.UpdateAsync(this.ViewModel.SeletItem);

            return await List();
        }
    }
}
