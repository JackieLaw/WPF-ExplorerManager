using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using HeBianGu.Product.ExplorePlayer.View.Movie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Movie")]
    public class MovieViewModel : MvcEntityViewModelBase<MovieModelViewModel>
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


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.Edit")
            {

                EditDialog detial = new EditDialog() { DataContext = this };

                MessageService.ShowWithLayer(detial);

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }


}
