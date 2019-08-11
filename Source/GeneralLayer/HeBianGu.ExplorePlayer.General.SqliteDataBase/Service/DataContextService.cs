using HeBianGu.Common.PublicTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.General.SqliteDataBase
{
    public static class DataContextService
    { 
        /// <summary> 依赖注入Respository</summary>
        public static void UseDataContext(this ServiceRegistry registry)
        {
            registry.Register<DataContext>(); 

        }

    }
}
