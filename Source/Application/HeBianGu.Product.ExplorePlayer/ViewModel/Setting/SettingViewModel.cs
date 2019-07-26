using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [ViewModel("Setting")]
    public class SettingViewModel : MvcViewModelBase
    { 
        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {



            }
            //  Do：取消
            else if (command == "GroupExpander.SelectChanged")
            {


            }

        } 

        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            ILinkActionBase link = new LinkActionBase();
            link.DisplayName = "总体概览";
            link.Logo = "&#xe69f;";
            link.Controller = "Setting";
            link.Action = "Case";

            this.SelectLink = link;
        }

        private bool CanLoaded(string args)
        {
            return true;
        }
 
    }
}
