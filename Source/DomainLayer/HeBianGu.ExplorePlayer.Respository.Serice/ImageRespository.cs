
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public class ImageRespository : ServiceRepositoryBase<mbc_dv_image>, IImageRespository
    {
        public ImageRespository(DataContext dbcontext) : base(dbcontext)
        {

        }

        List<FileInfo> GetAllFiles(string dirs, Predicate<FileInfo> predicate)
        {
            List<FileInfo> file = new List<FileInfo>();

            Action<FileInfo> action = l =>
            {
                if (!predicate(l)) return;
                file.Add(l);
            };

            this.DoAllFiles(dirs, action);

            return file;
        }


        void DoAllFiles(string dirs, Action<FileInfo> action)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);

            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();

            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {
                    //递归调用
                    DoAllFiles(fsinfo.FullName, action);
                }
                else if (fsinfo is FileInfo)
                {
                    action(fsinfo as FileInfo);
                }
            }
        }

        public async Task<IEnumerable<mbc_db_extendtype>> GetExtends()
        {
            return await _dataContext.mbc_db_extendtypes.Where(l => l.ISENBLED == 1 && l.MediaType == "Image").ToListAsync();
        }


        public async Task RefreshImage(mbc_dc_case item)
        {
            if (item.State == 1)
            {
                var result = await MessageService.ShowResultMessge("当前案例已经加载过,是否重新扫描！");

                if (!result) return;
            }

            var extends = await this.GetExtends();

            List<string> allextends = new List<string>();

            if (extends != null)
            {
                foreach (var item1 in extends)
                {
                    allextends.AddRange(item1.Value.Trim().ToLower().Split('/'));
                }
            }

            Predicate<FileInfo> match = l =>
            {
                if (allextends.Count == 0) return true;

                return allextends.Exists(k => k == l.Extension);
            };

            if (!Directory.Exists(item.BaseUrl))
            {
                Directory.CreateDirectory(item.BaseUrl);
            }

            var movies = await this.GetListAsync(l => l.CaseType == item.ID);

            List<FileInfo> files = null;

            await MessageService.ShowWaittingMessge(() =>
            {
                files = this.GetAllFiles(item.BaseUrl, match);
            });


            Action<FileInfo> action = l =>
            {
                if (movies != null)
                {
                    if (movies.Exists(k => k.Url == l.FullName)) return;
                }

                if (!match(l)) return;

                mbc_dv_image movie = new mbc_dv_image();
                //  Message：基础数据
                movie.Name = l.Name;
                movie.Url = l.FullName;
                movie.ExtendType = l.Extension;
                movie.CaseType = item.ID;
                movie.Size = l.Length;
                movie.FromType = "local";

                var tags = _dataContext.mbc_db_tagtypes.Where(k =>
                l.Name.Contains(k.Value));

                List<string> list = new List<string>();

                foreach (var ss in _dataContext.mbc_db_tagtypes)
                {
                    if (l.Name.Contains(ss.Value))
                        list.Add(ss.Value);
                }

                if (list != null && list.Count > 0)
                {
                    movie.TagTypes = list.Aggregate((m, k) => m + "," + k);
                }

                this.Insert(movie);
            };


            //this.DoAllFiles(item.BaseUrl, action);

            Action<IStringProgress> actionProgress = l =>
            {

                for (int i = 0; i < files.Count; i++)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        l.MessageStr = $"正在处理第{i + 1}条图片，共计{files.Count}条图片";
                    });

                    action(files[i]);
                }
            };


            await MessageService.ShowStringProgress(actionProgress);


            item.State = 1;
        }
    }
}
