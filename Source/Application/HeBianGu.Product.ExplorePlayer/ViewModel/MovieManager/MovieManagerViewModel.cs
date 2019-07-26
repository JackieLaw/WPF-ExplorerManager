using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
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

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("MovieManager")]
    public class MovieManagerViewModel : MvcEntityViewModelBase<mbc_dv_movie>
    {
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

    }
}
