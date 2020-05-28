using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Respository.IService;
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
            registry.Register<IMovieRespository,MovieRespository> ();

            registry.Register<ICaseRespository, CaseRespository>();

            registry.Register<IExtendRespository, ExtendRespository>();

            registry.Register<ITagRespository, TagRespository>();

            registry.Register<IFromRespository, FromRespository>();

            registry.Register<IAreaRespository, AreaRespository>();

            registry.Register<IVipRespository, VipRespository>();

            registry.Register<IMediaRespository, MediaRespository>();

            registry.Register<IArticulationRespository, ArticulationRespository>();
            registry.Register<IImageRespository, ImageRespository>();
            

        }

    }
}
