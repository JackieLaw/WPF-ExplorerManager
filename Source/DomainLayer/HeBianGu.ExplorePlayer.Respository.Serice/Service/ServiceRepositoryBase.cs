using HeBianGu.Base.Interface;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using HeBianGu.ExplorePlayer.Respository.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public class ServiceRepositoryBase<T> : RepositoryBase<T>, IServiceRepositoryBase<T> where T : StringEntityBase
    {

        protected DataContext _dataContext = null;
        public ServiceRepositoryBase(DataContext dbcontext) : base(dbcontext)
        {
            _dataContext = dbcontext;
        }
    }


}
