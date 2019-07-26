using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Case")]
    public class CaseViewModel : MvcViewModelBase<CaseRespository>
    {
        private ObservableCollection<mbc_dc_case> _collection = new ObservableCollection<mbc_dc_case>();
        /// <summary> 说明  </summary>
        public ObservableCollection<mbc_dc_case> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private mbc_dc_case _addCase=new mbc_dc_case();
        /// <summary> 说明  </summary>
        public mbc_dc_case AddCase
        {
            get { return _addCase; }
            set
            {
                _addCase= value;
                RaisePropertyChanged("AddCase");
            }
        }


        private mbc_dc_case _seletItem;
        /// <summary> 说明  </summary>
        public mbc_dc_case SeletItem
        {
            get { return _seletItem; }
            set
            {
                _seletItem = value;
                RaisePropertyChanged("SeletItem");
            }
        }


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
           
            }
            //  Do：取消
            else if (command == "Delete")
            {
               

            }

        }


        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            ILinkActionBase link = new LinkActionBase();
            link.DisplayName = "总体概览";
            link.Logo = "&#xe69f;";
            link.Controller = "Case";
            link.Action = "List";

            this.SelectLink = link;
        }

        private bool CanLoaded(string args)
        {
            return true;
        }

    }
}
