using HeBianGu.Base.WpfBase;
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
    [ViewModel("MovieManager")]
    public class MovieManagerViewModel : MvcViewModelBase
    {


        private ObservableCollection<mbc_dv_movie> _collection = new ObservableCollection<mbc_dv_movie>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_dv_movie> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private mbc_dv_movie _seletItem;
        /// <summary> 说明  </summary>
        public mbc_dv_movie SeletItem
        {
            get { return _seletItem; }
            set
            {
                _seletItem = value;
                RaisePropertyChanged("SeletItem");
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
                //ILinkActionBase link = new LinkActionBase();
                //link.DisplayName = "总体概览";
                //link.Logo = "&#xe69f;";
                //link.Controller = "Loyout";
                //link.Action = "OverView";
                //this.SelectLink = link;

                this.GoToLink("List");


            }
            //  Do：取消
            else if (command == "GroupExpander.SelectChanged")
            {


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



        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {

        }

        private bool CanLoaded(string args)
        {
            return true;
        }

    }
}
