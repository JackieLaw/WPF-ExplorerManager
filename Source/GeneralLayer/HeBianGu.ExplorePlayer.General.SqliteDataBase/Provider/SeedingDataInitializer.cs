using HeBianGu.ExplorePlayer.Base.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.General.SqliteDataBase
{
    public class SeedingDataInitializer : SqliteCreateDatabaseIfNotExists<DataContext>
    {
        public SeedingDataInitializer(DbModelBuilder builder) :base(builder)
        {

        }
        protected override void Seed(DataContext context)
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    var item = new mbc_dc_case { Name = "Employer" + (i + 1) };
            //    context.mbc_dc_cases.Add(item);
            //}

            base.Seed(context);
        }
    }
}
