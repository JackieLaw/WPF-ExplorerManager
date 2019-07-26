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
        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            this.GoToLink("Case");
        }

        private bool CanLoaded(string args)
        {
            return true;
        }

    }
}
