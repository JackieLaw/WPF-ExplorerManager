using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using HeBianGu.Product.ExplorePlayer.View.Movie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Movie")]
    public class MovieViewModel : MvcViewModelBase<IMovieRespository, ITagRespository, ICaseRespository, MovieModelViewModel>
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

        private ObservableCollection<GroupObject> _groupObject = new ObservableCollection<GroupObject>();
        /// <summary> 说明  </summary>
        public ObservableCollection<GroupObject> GroupObject
        {
            get { return _groupObject; }
            set
            {
                _groupObject = value;
                RaisePropertyChanged("GroupObject");
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

        public bool IsChanged { get; set; } = true;

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            if (!this.IsChanged) return;

            var tags = await this.Respository2.GetListAsync();

            this.TagCollection.BeginInvoke(l => l.Clear());

            foreach (var item in tags)
            {
                this.TagCollection.BeginInvoke(l => l.Add(item));

                Thread.Sleep(2);
            }

            var cases = await this.Respository3.GetListAsync();

            this.SelectCase = cases?.FirstOrDefault();

            this.IsChanged = false;
        }


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.Edit")
            {

                EditDialog detial = new EditDialog() { DataContext = this };

                MessageService.ShowWithLayer(detial);

                var model = await await MessageService.ShowWaittingResultMessge(() =>
                {
                    string id = this.SelectedItem?.ID;

                    return this.Respository.GetMovieWIthDetial(id);

                });

                this.RunAsync(() =>
                {
                    this.ImageCollection.Invoke(l => l.Clear());

                    foreach (var item in model.Item2)
                    {
                        this.ImageCollection.Invoke(l => l.Add(item));
                        Thread.Sleep(50);
                    }
                });


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
                    this.Collection.BeginInvoke(l => l.Clear());

                    foreach (var item in from)
                    {
                        MovieModelViewModel viewModel = new MovieModelViewModel(item);

                        this.Collection.BeginInvoke(l => l.Add(viewModel));

                        Thread.Sleep(2);

                    }

                    MessageService.ShowSnackMessageWithNotice("加载完成...");

                });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

            }
        }
    }


}
