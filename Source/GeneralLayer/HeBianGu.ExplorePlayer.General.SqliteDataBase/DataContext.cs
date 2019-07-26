using HeBianGu.ExplorePlayer.Base.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.General.SqliteDataBase
{
    public class DataContext : SqliteDataContextBase 
    { 
        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_dv_movie> mbc_dv_movies { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_areatype> mbc_db_areatypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_articulationtype> mbc_db_articulationtypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_extendtype> mbc_db_extendtypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_fromtype> mbc_db_fromtypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_mediatype> mbc_db_mediatypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_tagtype> mbc_db_tagtypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_db_viptype> mbc_db_viptypes { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_dc_case> mbc_dc_cases { get; set; }

        /// <summary>
        /// 实时缓存数据 用于绘制实时曲线
        /// </summary>
        public DbSet<mbc_dv_movieimage> mbc_dv_movieimages { get; set; }

    } 


}
