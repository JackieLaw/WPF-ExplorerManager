using HeBianGu.Base.WpfBase;

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
