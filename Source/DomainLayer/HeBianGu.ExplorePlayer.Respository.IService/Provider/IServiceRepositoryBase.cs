using HeBianGu.Base.Interface;
using HeBianGu.ExplorePlayer.Base.Model;
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
