using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.VLCMediaPlayer;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using HeBianGu.Product.ExplorePlayer.View.Movie;
using HeBianGu.Product.ExplorePlayer.View.Movie.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Movie")]
    public class MovieViewModel : MvcViewModelBase<IMovieRespository, ITagRespository, ICaseRespository, ClipBoardService, IMovieimageRespository, MovieModelViewModel>
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


        private ObservableCollection<mbc_dc_case> _cases = new ObservableCollection<mbc_dc_case>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_dc_case> Cases
        {
            get { return _cases; }
            set
            {
                _cases = value;
                RaisePropertyChanged("Cases");
            }
        }

        private ObservableCollection<mbc_dv_movieimage> _imagecollection = new ObservableCollection<mbc_dv_movieimage>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_dv_movieimage> ImageCollection
        {
            get { return _imagecollection; }
            set
            {
                _imagecollection = value;
                RaisePropertyChanged("ImageCollection");
            }
        }

        private mbc_dv_movieimage _selectImage;
        /// <summary> 说明  </summary>
        public mbc_dv_movieimage SelectImage
        {
            get { return _selectImage; }
            set
            {
                _selectImage = value;
                RaisePropertyChanged("SelectImage");
            }
        }

        private ObservableCollection<mbc_db_tagtype> _tagcollection = new ObservableCollection<mbc_db_tagtype>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_db_tagtype> TagCollection
        {
            get { return _tagcollection; }
            set
            {
                _tagcollection = value;
                RaisePropertyChanged("TagCollection");
            }
        }


        private ObservableCollection<mbc_db_tagtype> _selectTag = new ObservableCollection<mbc_db_tagtype>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_db_tagtype> SelectTag
        {
            get { return _selectTag; }
            set
            {
                _selectTag = value;
                RaisePropertyChanged("SelectTag");
            }
        }

        private ObservableCollection<mbc_db_tagtype> _editSelectTag = new ObservableCollection<mbc_db_tagtype>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_db_tagtype> EditSelectTag
        {
            get { return _editSelectTag; }
            set
            {
                _editSelectTag = value;
                RaisePropertyChanged("EditSelectTag");
            }
        }

        private bool _isEditting;
        /// <summary> 说明  </summary>
        public bool IsEditting
        {
            get { return _isEditting; }
            set
            {
                _isEditting = value;
                RaisePropertyChanged("IsEditting");
            }
        }


        private string _orderBy;
        /// <summary> 说明  </summary>
        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                _orderBy = value;
                RaisePropertyChanged("OrderBy");
            }
        }


        private bool _desc;
        /// <summary> 说明  </summary>
        public bool Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                RaisePropertyChanged("Value");
            }
        }

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            this.Invoke(() =>
            {
                this.Service3.Register(Application.Current.MainWindow);
            });
        }


        private ObservableSource<MovieModelViewModel> _observableSource = new ObservableSource<MovieModelViewModel>() { PageCount = 6 };
        /// <summary> 说明  </summary>
        public ObservableSource<MovieModelViewModel> ObservableSource
        {
            get { return _observableSource; }
            set
            {
                _observableSource = value;
                RaisePropertyChanged("ObservableSource");
            }
        }



        private Func<object, ImageSource> _convertTo = l =>
              {
                  if (l is mbc_dv_movieimage image)
                  {
                      if (string.IsNullOrEmpty(image.Image)) return null;

                      byte[] byteArray = System.Convert.FromBase64String(image.Image);

                      BitmapImage bmp = null;

                      bmp = new BitmapImage();
                      bmp.BeginInit();
                      bmp.StreamSource = new MemoryStream(byteArray);
                      bmp.EndInit();

                      return bmp;
                  }
                  else
                  {
                      return null;
                  }


              };
        /// <summary> 说明  </summary>
        public Func<object, ImageSource> ConverTo
        {
            get { return _convertTo; }
            set
            {
                _convertTo = value;
                RaisePropertyChanged("ConverTo");
            }
        }

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.Edit")
            {

                this.ImageCollection.Clear();

                EditDialog detial = new EditDialog() { DataContext = this };

                MessageService.ShowWithLayer(detial);

                var model = await await MessageService.ShowWaittingResultMessge(() =>
                {
                    string id = this.SelectedItem?.ID;

                    return this.Respository.GetMovieWIthDetial(id);

                });

                this.ImageCollection = model.Item2?.ToObservable();
            }
            //  Do：取消
            else if (command == "Button.Click.Load")
            {
                if (this.SelectCase == null)
                {
                    MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                    return;
                }

                var from = await MessageService.ShowWaittingResultMessge(() =>
                 {
                     return this.Respository.GetListAsync(l => l.CaseType == this.SelectCase.ID).Result;
                 });

                if (from == null)
                {
                    MessageService.ShowSnackMessageWithNotice("没有视频数据，请先生成视频数据");
                    return;
                }

#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                Task.Run(() =>
                {
                    this.ObservableSource.Clear();

                    foreach (var item in from)
                    {
                        MovieModelViewModel viewModel = new MovieModelViewModel(item);

                        this.ObservableSource.Add(viewModel);

                        Thread.Sleep(2);

                    }

                    MessageService.ShowSnackMessageWithNotice("加载完成...");

                });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

            }

            else if (command == "ListBox.SelectionChanged.Filter")
            {
                var tags = this.SelectTag.ToList();

                Func<MovieModelViewModel, bool> expression = l =>
                {

                    if (tags == null || tags.Count == 0) return true;

                    if (string.IsNullOrEmpty(l.TagTypes)) return false;

                    return tags.TrueForAll(k => l.TagTypes.Trim().Split(',').ToList().Exists(m => m == k.Name));

                };

                this.ObservableSource.Fileter = l => expression(l);
            }

            else if (command == "ListBox.SelectionChanged.OrderBy")
            {
                if (this.OrderBy == "按名称排序")
                {
                    this.ObservableSource.Sort(l => l.Name, this.Desc);
                }
                else if (this.OrderBy == "按大小")
                {
                    this.ObservableSource.Sort(l => l.Size, this.Desc);
                }
                else if (this.OrderBy == "按评分")
                {
                    this.ObservableSource.Sort(l => l.Score, this.Desc);
                }
                else if (this.OrderBy == "按总时间")
                {
                    this.ObservableSource.Sort(l => l.Duration, this.Desc);
                }
                else if (this.OrderBy == "按播放次数")
                {
                    this.ObservableSource.Sort(l => l.PlayCount, this.Desc);
                }
                else if (this.OrderBy == "按清晰度")
                {
                    this.ObservableSource.Sort(l => l.ArticulationType, this.Desc);
                }
                else if (this.OrderBy == "按缩略图")
                {
                    this.ObservableSource.Sort(l => l.Image, this.Desc);
                }
            }

            else if (command == "Button.Click.Set")
            {
                SetControl detial = new SetControl() { DataContext = this };

                MessageService.ShowWithLayer(detial);
            }

            else if (command == "ListBox.SelectionChanged.TagEdit")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (this.SelectedItem == null) return;

                    if (this.EditSelectTag == null || this.EditSelectTag.Count == 0) return;

                    this.SelectedItem.TagTypes = this.EditSelectTag?.Select(l => l.Name).Aggregate((l, k) => l + "," + k);
                });
            }

            else if (command == "Button.Click.SetUpdate")
            {
                string message;

                if (!this.ModelState(this.SelectedItem.Model, out message))
                {
                    MessageService.ShowSnackMessage(message);
                    return;
                }

                await this.Respository.UpdateAsync(this.SelectedItem.Model);

                MessageService.CloseWithLayer();

                MessageService.ShowSnackMessage("保存成功！");
            }

            else if (command == "Button.Click.DeleteDeep")
            {
                if (this.SelectedItem == null) return;

                var result = await MessageService.ShowResultMessge("确定要彻底删除文件?");

                if (result)
                {
                    if (File.Exists(this.SelectedItem.Url))
                    {
                        File.Delete(this.SelectedItem.Url);

                        MessageService.ShowSnackMessage("文件已删除：" + this.SelectedItem?.Url);

                        this.RelayMethod("Button.Click.Remove");


                    }
                }
            }

            else if (command == "Button.Click.Remove")
            {
                if (this.SelectedItem == null) return;

                await this.Respository.DeleteAsync(this.SelectedItem.Model.ID);

                this.Invoke(() => this.ObservableSource.Remove(this.SelectedItem));
            }

            else if (command == "BulletCheckBox.CheckedChanged.Click")
            {
                {
                    if (!this.IsEditting)
                    {
                        this.Service3.ClipBoardChanged = null;
                        return;

                    }

                    this.Service3.ClipBoardChanged = async () =>
                    {
                        //Todo  ：复制的图片 
                        BitmapSource bit = Clipboard.GetImage();

                        if (bit != null)
                        {

                            mbc_dv_movieimage image = new mbc_dv_movieimage();

                            image.MovieID = this.SelectedItem.ID;

                            image.Text = DateTime.Now.ToDateTimeString();

                            image.TimeSpan = DateTime.Now.ToDateTimeString();

                            image.Image = ImageService.BitmapSourceToString(bit);

                            await this.Respository.AddMovieImage(image);

                            this.ImageCollection.Add(image);
                        }
                    };
                }

            }

            else if (command == "Button.Click.DeleteImage")
            {
                await this.Service4.DeleteAsync(this.SelectImage);

                this.ImageCollection.Remove(this.SelectImage);

                MessageService.ShowSnackMessageWithNotice("删除成功！");
            }

            else if (command == "Button.Click.SetImage")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.SelectedItem.Image = this.SelectImage?.Image;
                });

                await this.Respository.SaveAsync();
            }

            else if (command == "ListBox.SelectionChanged.SelectedtemChanged")
            {

                if (this.SelectedItem == null) return;

                var from = this.SelectedItem.TagTypes?.Split(',').ToList();


                if (from == null)
                {
                    this.EditSelectTag = new ObservableCollection<mbc_db_tagtype>();
                    return;
                }

                var result = this.TagCollection.Where(l => from.Exists(k => k == l.Name));

                this.EditSelectTag.Clear();

                ObservableCollection<mbc_db_tagtype> collection = new ObservableCollection<mbc_db_tagtype>();

                foreach (var item in result)
                {
                    collection.Add(item);
                }

                this.EditSelectTag = collection;
            }

            else if (command == "Button.Click.Play")
            {
                if (this.SelectedItem == null) return;

                PlayerDialog player = new PlayerDialog();

                player.Source = new Uri(this.SelectedItem.Url, UriKind.Absolute);


                List<TimeFlagViewModel> times = new List<TimeFlagViewModel>();

                var model = await await MessageService.ShowWaittingResultMessge(() =>
                {
                    string id = this.SelectedItem?.ID;

                    return this.Respository.GetMovieWIthDetial(id);

                });

                if (model.Item2 != null)
                {
                    foreach (var item in model.Item2)
                    {
                        bool b = TimeSpan.TryParse(item.TimeSpan,out TimeSpan span);

                        if (!b) continue;

                        times.Add(new TimeFlagViewModel() { DisPlay = item.Text, TimeSpan = span });
                    }

                    player.Times = times.ToObservable();
                }


                player.FlagClick += async (l, k) =>
              {
                  TimeSpan time = player.GetTime();

                  var flag = new TimeFlagViewModel() { TimeSpan = time };

                  mbc_dv_movieimage image = new mbc_dv_movieimage();

                  string imageFile = await player.BeginShootCut();

                  if (!File.Exists(imageFile)) return;

                  image.Image = ImageService.BitmapSourceToString(new BitmapImage(new Uri(imageFile, UriKind.Absolute)));

                  bool r = await MessageService.ShowObjectWithPropertyForm(flag, null, "请输入标记信息", 1);

                  if (!r) return;

                  image.MovieID = this.SelectedItem.ID;

                  image.Text = flag.DisPlay;

                  image.TimeSpan = time.ToString();



                  await this.Respository.AddMovieImage(image);

                  times = new List<TimeFlagViewModel>();

                  model = await await MessageService.ShowWaittingResultMessge(() =>
                  {
                      string id = this.SelectedItem?.ID;

                      return this.Respository.GetMovieWIthDetial(id);

                  });

                  if (model.Item2 != null)
                  {
                      foreach (var item in model.Item2)
                      {
                          times.Add(new TimeFlagViewModel() { DisPlay = item.Text, TimeSpan = TimeSpan.Parse(item.TimeSpan) });
                      }

                      player.Times = times.ToObservable();
                  }

              };


                MessageService.ShowWithLayer(player);
            }
        }






    }
}
