 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public static class RespositoryService
    {

        ///// <summary> 依赖注入Respository</summary>
        //public static void AddRespositorys(this IServiceCollection services)
        //{
        //    services.AddScoped<IUserAccountRespositroy, UserAccountRespositroy>();

        //    services.AddScoped<IMonitorSetRespository, MonitorSetRespository>();

        //    services.AddScoped<ICustomerRespository, CustomerRespository>();

        //    services.AddScoped<IBedRespository, BedRespositroy>();

        //    services.AddScoped<IUserLoggerRespository, UserLoggerRespository>();

        //    services.AddScoped<IMovieRespository, MovieRespository>();

        //    services.AddScoped<ICaseRespository, CaseRespository>();

        //    services.AddScoped<IExtendRespository, ExtendRespository>();

        //    services.AddScoped<ITagRespository, TagRespository>();

        //    services.AddScoped<IFromRespository, FromRespository>();

        //    services.AddScoped<IAreaRespository, AreaRespository>();

        //    services.AddScoped<IVipRespository, VipRespository>();

        //    services.AddScoped<IMediaRespository, MediaRespository>();

        //    services.AddScoped<IArticulationRespository, ArticulationRespository>();

        //}

        /// <summary> 依赖注入Respository</summary>
        public static void UseRespositorys(this ServiceRegistry registry)
        {
            registry.Register<MovieRespository > ();

            registry.Register<CaseRespository>();

            registry.Register<ExtendRespository>();

            registry.Register<TagRespository>();

            registry.Register<FromRespository>();

            registry.Register<AreaRespository>();

            registry.Register<VipRespository>();

            registry.Register<MediaRespository>();

            registry.Register<ArticulationRespository>();

        }

    }
}
