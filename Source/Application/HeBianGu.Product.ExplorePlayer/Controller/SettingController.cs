using HeBianGu.Common.PublicTool;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Setting")]
    class SettingController : Controller<SettingViewModel>
    {
        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> Case()
        {
            return View();
        }
    }
}
