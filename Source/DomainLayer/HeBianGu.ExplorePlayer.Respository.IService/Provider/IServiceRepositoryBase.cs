using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.Respository.IService
{
    public interface IServiceRepositoryBase<T> : IStringRepository<T> where T : StringEntityBase
    {
        
    }
}
