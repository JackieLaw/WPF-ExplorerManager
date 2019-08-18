using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public static class RespositoryService
    {
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
