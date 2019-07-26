using HeBianGu.ExplorePlayer.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.General.SqliteDataBase
{
   public class SqliteDBService
    {

        public static SqliteDBService Instance = new SqliteDBService();


        public void AddCase(params mbc_dc_case[] from)
        {
            using (DataContext context = new DataContext())
            { 
                context.mbc_dc_cases.AddRange(from); 
                context.SaveChanges();
            }
        }
    }
}
