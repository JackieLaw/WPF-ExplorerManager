using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.MvcRespository;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Case")]
    class CaseController : EntityBaseController<mbc_dc_case, CaseViewModel, ICaseRespository>
    {
        public override async Task<IActionResult> List()
        {
            return await ViewAsync();
        }
    }
}
