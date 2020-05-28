using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using HeBianGu.Product.ExplorePlayer.View.Case;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Case")]
    public class CaseViewModel : MvcViewModelBase<ICaseRespository, mbc_dc_case>
    {
        protected override void Loaded(string args)
        {
            this.RefreshData();
        }

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.AddCase")
            {

                //AddControl detial = new AddControl() { DataContext = this };

                //MvcAddFrame addFrame = new MvcAddFrame() { DataContext = this };

                var from = await MessageService.ShowWithObject(this.AddItem);

                if (from)
                {
                    var result = await this.Respository.InsertAsync(this.AddItem);

                    this.AddItem = new mbc_dc_case();

                    MessageService.ShowSnackMessageWithNotice("新增成功！");

                    this.RefreshData();
                }

                //var model = await await MessageService.ShowWaittingResultMessge(() =>
                //{
                //    string id = this.SelectedItem?.ID;

                //    return this.Respository.GetMovieWIthDetial(id);

                //});

                //this.RunAsync(() =>
                //{
                //    this.ImageCollection.Invoke(l => l.Clear());

                //    foreach (var item in model.Item2)
                //    {
                //        this.ImageCollection.Invoke(l => l.Add(item));
                //        Thread.Sleep(50);
                //    }
                //});

            }
            //  Do：取消
            else if (command == "Button.Click.Load")
            {


            }
        }


        public void RefreshData()
        {
            var source = this.Respository.GetListAsync();

            this.Collection.Invoke(l => l.Clear());

            foreach (var item in source.Result)
            {
                this.Collection.Invoke(l => l.Add(item));

                Thread.Sleep(10);
            }
        }

    }


}
