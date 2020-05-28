using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("MovieManager")]
    public class MovieManagerViewModel : MvcViewModelBase<IMovieRespository, ICaseRespository, mbc_dv_movie>
    {

        private mbc_dc_case _selectCase;
        /// <summary> 说明  </summary>
        public mbc_dc_case SelectCase
        {
            get { return _selectCase; }
            set
            {
                _selectCase = value;
                RaisePropertyChanged("SelectCase");
            }
        }

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            var cases = await this.Respository2.GetListAsync();

            this.SelectCase = cases?.FirstOrDefault();
        }


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.ConvertAll")
            {
                if (this.SelectCase == null)
                {
                    MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                    return;
                }

                if (!Directory.Exists(this.SelectCase.BaseUrl))
                {
                    MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                    return;
                }

                Action<IStringProgress> actionProgress = async l =>
                {
                    for (int i = 0; i < this.Collection.Count; i++)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            l.MessageStr = $"正在转换第{i + 1}条视频，共计{this.Collection.Count}条视频";
                        });

                        var movie = this.Collection[i];

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
                            this.Invoke(() => MessageService.ShowSystemNotifyMessageWithError(ex.Message));
                        }

                    }
                };

                await MessageService.ShowStringProgress(actionProgress);
            }
            //  Do：取消
            else if (command == "Button.Click.Load")
            {
                if (this.SelectCase == null)
                {
                    MessageService.ShowSnackMessageWithNotice("请先选择案例");
                    return;
                }

                var from = await await MessageService.ShowWaittingResultMessge(() => this.Respository.GetListAsync(l => l.CaseType == this.SelectCase.ID));


                if (from == null)
                {
                    MessageService.ShowSnackMessageWithNotice("没有数据");
                }


                this.RunAsync(() =>
                {
                    this.Collection.Invoke(l => l.Clear());

                    foreach (var item in from)
                    {
                        this.Collection.Invoke(l => l.Add(item));
                    }

                    MessageService.ShowSnackMessageWithNotice("加载完成...");

                });

            }

            else if (command == "Button.Click.Create")
            {
                if (this.SelectCase == null)
                {
                    MessageService.ShowSnackMessageWithNotice("请先选择案例");
                    return;
                }

                if (this.SelectCase.State == 1)
                {
                    var result = await MessageService.ShowResultMessge("当前案例已经加载过,是否重新扫描！");

                    if (!result) return;
                }

                await this.Respository.RefreshMovie(this.SelectCase);

                this.RelayMethod("Button.Click.Load");
            }
            else if (command == "Button.Click.Clear")
            {
                if (this.SelectCase == null)
                {
                    MessageService.ShowSnackMessageWithNotice("请先选择案例");
                    return;
                }



                Action<IStringProgress> actionProgress = c =>
                {
                    //var from = this.Respository.GetListAsync(l => l.CaseType == this.SelectCase.ID).Result;

                    int count = this.Collection.Count;

                    for (int i = 0; i < count; i++)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            c.MessageStr = $"正在处理第{i + 1}条视频，共计{count}条数据";
                        });

                        this.Respository.DeleteAsync(this.Collection[0]).Wait();

                        this.Collection.Invoke(l => l.Remove(this.Collection[0]));
                    }
                };


                await MessageService.ShowStringProgress(actionProgress);

                MessageService.ShowSnackMessageWithNotice("清理完成！");
            }
        }
    }
}
