﻿using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Movie")]
    public class MovieViewModel : MvcEntityViewModelBase<mbc_dv_movie>
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


        private ObservableCollection<mbc_db_tagtype> _selectTag= new ObservableCollection<mbc_db_tagtype>();
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

    }

}
