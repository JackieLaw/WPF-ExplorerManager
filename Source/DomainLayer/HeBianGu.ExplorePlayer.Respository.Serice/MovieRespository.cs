
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.FFmpegService;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using HeBianGu.ExplorePlayer.Respository.IService;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.ExplorePlayer.Respository.Serice
{
    public class MovieRespository : ServiceRepositoryBase<mbc_dv_movie>, IMovieRespository
    {
        public MovieRespository(DataContext dbcontext) : base(dbcontext)
        {

        }

        public async Task<IEnumerable<mbc_dc_case>> GetCases()
        {
            return await _dbContext.mbc_dc_cases.Where(l => l.ISENBLED == 1).ToListAsync();
        }

        public async Task<IEnumerable<mbc_db_extendtype>> GetExtends()
        {
            return await _dbContext.mbc_db_extendtypes.Where(l => l.ISENBLED == 1).ToListAsync();
        }

        public async Task<Tuple<mbc_dv_movie, List<mbc_dv_movieimage>>> GetMovieWIthDetial(string id)
        {
            var movie = await this.GetByIDAsync(id);

            var images = await this._dbContext.mbc_dv_movieimages.Where(l => l.MovieID == id)?.ToListAsync();

            return Tuple.Create(movie, images);
        }


        public async Task<int> Insert(FileInfo file, string caseid)
        {

            mbc_dv_movie movie = new mbc_dv_movie();
            movie.Name = file.Name;
            movie.Url = file.FullName;
            movie.ExtendType = file.Extension;
            movie.CaseType = caseid;
            movie.Size = file.Length;
            movie.FromType = "local";

            //var detial = FFmpegService.Instance.GetMediaEntity(file.FullName);

            //var tags = this._dbContext.mbc_db_tagtypes.Where(l => file.Name.Contains(l.Value));

            //var list = tags.ToList(); 

            //if (list != null && list.Count > 0)
            //{
            //    movie.TagTypes = list.Select(l => l.Value).Aggregate((l, k) => l + "," + k);
            //}


            //if (detial != null)
            //{
            //    movie.Duration = detial.Duration;
            //    movie.Bitrate = detial.Bitrate;
            //    movie.MediaCode = detial.MediaCode;
            //    movie.VedioType = detial.MediaType;
            //    movie.Resoluction = detial.Resoluction;
            //    movie.Aspect = detial.Aspect;
            //    movie.Rate = detial.Rate;
            //}

            return await this.InsertAsync(movie, false);
        }

        public async Task<List<string>> UpdateImage(string id, string timespan = "00:01:00")
        {

            return null;

            //var find = await this.GetByIDAsync(id);

            //string shootcutpath = Path.Combine(Path.GetDirectoryName(find.Url), Path.GetFileNameWithoutExtension(find.Url) + "_shootcut.png");

            //Action<int> exitAction = k =>
            //{
            //    if (k != 0) return;

            //    if (!File.Exists(shootcutpath)) return;

            //    find.Image = "data:image/jpeg;base64," + EncodeImageToString(shootcutpath);

            //    this.UpdateAsync(find);

            //    List<string> result = new List<string>();
            //    result.Add(shootcutpath);

            //    return result;
            //};

            //FFmpegService.Instance.ShootCut(find.Url, shootcutpath, timespan, exitAction);

            //find.Image = "data:image/jpeg;base64," + EncodeImageToString(shootcutpath);


        }

        //将图片以二进制流
        public byte[] SaveImage(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
            return imgBytesIn;
        }

        public string EncodeImageToString(string imageFilePath)
        {
            byte[] image = null;
            FileStream fstrm = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fstrm))
            {
                image = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                {
                    image[i] = reader.ReadByte();
                }
            }
            return Base64EncodeBytes(image);
        }

        public string Base64EncodeBytes(byte[] inputBytes)
        {
            return Convert.ToBase64String(inputBytes);
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


        public async Task ConvertMovie(mbc_dv_movie movie)
        {
            //  Message：ffmpeg数据
            var detial = FFmpegService.Instance.GetMediaEntity(movie.Url);

            if (detial != null)
            {
                movie.Duration = detial.Duration;
                movie.Bitrate = detial.Bitrate;
                movie.MediaCode = detial.MediaCode;
                movie.VedioType = detial.MediaType;
                movie.Resoluction = detial.Resoluction;
                movie.Aspect = detial.Aspect;
                movie.Rate = detial.Rate;
            }

            System.Console.WriteLine("加载缩略图:" + movie.Url);
            //  Message：缩略图和预览图
            string shootcutpath = Path.Combine(Path.GetDirectoryName(movie.Url), Path.GetFileNameWithoutExtension(movie.Url) + "_shootcut.png");


            //  Message：默认一分钟图片作为缩略图
            FFmpegService.Instance.ShootCut(movie.Url, shootcutpath, "00:01:00");


            if (File.Exists(shootcutpath))
            {
                movie.Image =EncodeImageToString(shootcutpath);

                File.Delete(shootcutpath);
            }

            string shootcutbatpath = Path.Combine(Path.GetDirectoryName(movie.Url), Path.GetFileNameWithoutExtension(movie.Url) + "_shootcut");

            //  Message：默认一分钟图片作为缩略图
            var images = FFmpegService.Instance.ShootCutBat(movie.Url, shootcutbatpath);

            foreach (var m in images)
            {
                if (!File.Exists(m)) continue;

                mbc_dv_movieimage image = new mbc_dv_movieimage();

                image.MovieID = movie.ID;
                image.Image = EncodeImageToString(m);
                image.Text = Path.GetFileName(m);

                _dbContext.mbc_dv_movieimages.Add(image);

                //  Message：保存完删除图片
                File.Delete(m);
            }

            await this.SaveAsync();
        }
        public async Task RefreshMovie(mbc_dc_case item)
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
                //if (movies != null)
                //{
                //    if (movies.Exists(k => k.Url == l.FullName)) return;
                //}

                if (!match(l)) return;

                mbc_dv_movie movie = new mbc_dv_movie();
                //  Message：基础数据
                movie.Name = l.Name;
                movie.Url = l.FullName;
                movie.ExtendType = l.Extension;
                movie.CaseType = item.ID;
                movie.Size = l.Length;
                movie.FromType = "local";

                var tags = _dbContext.mbc_db_tagtypes.Where(k =>
                l.Name.Contains(k.Value));

                List<string> list = new List<string>();

                foreach (var ss in _dbContext.mbc_db_tagtypes)
                {
                    if (l.Name.Contains(ss.Value))
                        list.Add(ss.Value);
                }

                if (list != null && list.Count > 0)
                {
                    movie.TagTypes = list.Aggregate((m, k) => m + "," + k);
                }

                System.Console.WriteLine("加载文件详情:" + l.FullName);
                //try
                //{
                //    //  Message：ffmpeg数据
                //    var detial = FFmpegService.Instance.GetMediaEntity(l.FullName);

                //    if (detial != null)
                //    {
                //        movie.Duration = detial.Duration;
                //        movie.Bitrate = detial.Bitrate;
                //        movie.MediaCode = detial.MediaCode;
                //        movie.VedioType = detial.MediaType;
                //        movie.Resoluction = detial.Resoluction;
                //        movie.Aspect = detial.Aspect;
                //        movie.Rate = detial.Rate;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    System.Console.WriteLine("获取ffmpeg详情信息错误:" + ex);
                //}

                System.Console.WriteLine("加载缩略图:" + l.FullName);
                //  Message：缩略图和预览图
                string shootcutpath = Path.Combine(Path.GetDirectoryName(movie.Url), Path.GetFileNameWithoutExtension(movie.Url) + "_shootcut.png");


                ////  Message：默认一分钟图片作为缩略图
                //FFmpegService.Instance.ShootCut(movie.Url, shootcutpath, "00:01:00");


                //if (File.Exists(shootcutpath))
                //{
                //    movie.Image = "data:image/jpeg;base64," + EncodeImageToString(shootcutpath);

                //    File.Delete(shootcutpath);
                //}

                _dbContext.mbc_dv_movies.Add(movie);

                System.Console.WriteLine("加载预览图:" + l.FullName);

                string shootcutbatpath = Path.Combine(Path.GetDirectoryName(movie.Url), Path.GetFileNameWithoutExtension(movie.Url) + "_shootcut");

                ////  Message：默认一分钟图片作为缩略图
                //var images = FFmpegService.Instance.ShootCutBat(movie.Url, shootcutbatpath);

                //foreach (var m in images)
                //{
                //    if (!File.Exists(m)) continue;

                //    mbc_dv_movieimage image = new mbc_dv_movieimage();

                //    image.MovieID = movie.ID;
                //    image.Image = "data:image/jpeg;base64," + EncodeImageToString(m);
                //    image.Text = Path.GetFileName(m);

                //    _dbContext.mbc_dv_movieimages.Add(image);

                //    //  Message：保存完删除图片
                //    File.Delete(m);
                //}

                //  Message：一个文件一保存
                _dbContext.SaveChanges();
            };


            //this.DoAllFiles(item.BaseUrl, action);

            Action<StringProgressDialog> actionProgress = l =>
            {

                for (int i = 0; i < files.Count; i++)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        l.MessageStr = $"正在处理第{i + 1}条视频，共计{files.Count}条视频";
                    });

                    action(files[i]);
                }
            };


            await MessageService.ShowStringProgress(actionProgress);


            item.State = 1;
        }
    }
}
