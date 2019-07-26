
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public class CaseRespository : ServiceRepositoryBase<mbc_dc_case>, ICaseRespository
    {
        public CaseRespository(DataContext dbcontext) : base(dbcontext)
        {

        }



        public async Task<ObservableCollection<GroupObject>> GetGroupObject()
        {
            ObservableCollection<GroupObject> result = new ObservableCollection<GroupObject>();

            GroupObject group = new GroupObject();

            var from = await this.GetListAsync();

            if (from == null) return result;

            foreach (var item in from)
            {
                group.Add(item);
            }

            result.Add(group);

            return result;
        }

    }
}
