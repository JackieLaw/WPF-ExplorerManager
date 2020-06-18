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
    public class CaseViewModel : MvcViewModelBase<ICaseRespository, IMovieRespository, IMovieimageRespository, mbc_dc_case>
    {
        //protected override void Loaded(string args)
        //{
        //    this.RefreshData();
        //}

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.AddCase")
            {

                //AddControl detial = new AddControl() { DataContext = this };

                //MvcAddFrame addFrame = new MvcAddFrame() { DataContext = this };

                var from = await MessageService.ShowObjectWithPropertyForm(this.AddItem,null, "新增案例");

                if (from)
                {
                    var result = await this.Respository.InsertAsync(this.AddItem);

                    this.AddItem = new mbc_dc_case();

                    MessageService.ShowSnackMessageWithNotice("新增成功！");

                    await this.RefreshData();
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
            else if (command == "Button.Click.Delete")
            {
                if (this.SelectedItem == null) return;

                Action<IStringProgress> action = async m =>
                  {
                      m.MessageStr = "正在加载,请等待...";

                      var movies = await this.Service1.GetListAsync(l => l.CaseType == this.SelectedItem.ID);

                      //  Do ：删除缩略图

                      for (int i = 0; i < movies.Count; i++)
                      {
                          var movie = movies[i];

                          m.MessageStr = $"正在删除第{i + 1}个视频，共{movies.Count}个视频";

                          var images = await this.Service2.GetListAsync(l => l.MovieID == movie.ID);

                          foreach (var item in images)
                          {
                              await this.Service2.DeleteAsync(item);
                          }

                          //  Do ：删除视频
                          await this.Service1.DeleteAsync(movie);
                      }

                      //  Do ：删除案例
                      await this.Respository.DeleteAsync(this.SelectedItem);

                      this.Collection.Invoke(l=>l.Remove(this.SelectedItem));
                  };

                await MessageService.ShowStringProgress(action);

            }
            //  Do：取消
            else if (command == "Button.Click.Edit")
            {


            }
        }


        public async Task RefreshData()
        {
            var source = await this.Respository.GetListAsync();

            this.Collection = source.ToObservable();

            //this.Collection.Invoke(l => l.Clear());

            //foreach (var item in source.Result)
            //{
            //    this.Collection.Invoke(l => l.Add(item));

            //    Thread.Sleep(10);
            //}
        }

    }


}
