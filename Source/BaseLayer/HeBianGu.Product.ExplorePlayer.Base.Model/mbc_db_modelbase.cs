using HeBianGu.Base.Interface;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.Base.Model
{
   public class mbc_db_modelbase : StringEntityBase
    {
        [Display(Name = "创建时间")]
        [ReadOnly]
        public string CDATE { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Display(Name = "修改时间")]
        [ReadOnly]
        public string UDATE { get; set; }

        [Display(Name = "是否可用")]
        [Hidden]
        public int ISENBLED { get; set; } = 1;
    }
}
