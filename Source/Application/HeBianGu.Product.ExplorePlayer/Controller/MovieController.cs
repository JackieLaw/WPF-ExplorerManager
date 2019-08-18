using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using HeBianGu.Domain.MvcRespository;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.Respository.Serice;
using HeBianGu.ExplorePlayer.Respository.ViewModel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Movie")]
    internal class MovieController : Controller<MovieViewModel, MovieRespository>
    {

        TagRespository _tagRespository;

        ClipBoardService _clipBoardService;

        CaseRespository _caseRespository;

        public MovieController(TagRespository tagRespository, ClipBoardService clipBoardService, CaseRespository caseRespository)
        {
            _tagRespository = tagRespository;

            _clipBoardService = clipBoardService;

            _caseRespository = caseRespository;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _clipBoardService.Register(Application.Current.MainWindow);
            });


        }
        public async Task<IActionResult> Center()
        {
            return View();
        }


        
        public async Task<IActionResult> List()
        {
            var tags = _tagRespository.GetListAsync()?.Result;

            this.Invoke(() =>
            {
                this.ViewModel.TagCollection.Clear();

                foreach (var item in tags)
                {
                    this.ViewModel.TagCollection.Add(item);
                }
            });


            if (this.ViewModel.SelectCase == null)
            {
                var cases= await this._caseRespository.GetListAsync();

                var select= cases.FirstOrDefault();

                if(select==null)
                {

                    return View();
                }
                else
                {
                    this.ViewModel.SelectCase = select;
                } 
            }

            var from = this.Respository.GetListAsync(l => l.CaseType == this.ViewModel.SelectCase.ID).Result;

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Clear();
            });

            if (from == null)
            {
                return View();
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in from)
                {
                    MovieModelViewModel viewModel = new MovieModelViewModel(item);
                    this.ViewModel.Collection.Add(viewModel);
                }
            });

            return View();
        }


        public async Task DeleteSelectImage()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.ImageCollection.Remove(this.ViewModel.SelectImage);
            });
        }

        public async Task SetViewImage()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //this.ViewModel.ImageCollection.Remove(this.ViewModel.SelectImage);

                this.ViewModel.SeletItem.Image = this.ViewModel.SelectImage?.Image;


            });

            await this.Respository.SaveAsync();
        }


        public async Task SelectionTagEdit()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this.ViewModel.SeletItem == null) return;

                if (this.ViewModel.EditSelectTag == null || this.ViewModel.EditSelectTag.Count == 0) return;

                this.ViewModel.SeletItem.TagTypes = this.ViewModel.EditSelectTag?.Select(l => l.Name).Aggregate((l, k) => l + "," + k);
            });

        }

        public async Task OrderBy()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this.ViewModel.OrderBy == "按名称排序")
                {
                    this.ViewModel.Collection.Sort(l => l.Name, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按大小")
                {
                    this.ViewModel.Collection.Sort(l => l.Size, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按评分")
                {
                    this.ViewModel.Collection.Sort(l => l.Score, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按总时间")
                {
                    this.ViewModel.Collection.Sort(l => l.Duration, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按播放次数")
                {
                    this.ViewModel.Collection.Sort(l => l.PlayCount, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按清晰度")
                {
                    this.ViewModel.Collection.Sort(l => l.ArticulationType, this.ViewModel.Desc);
                }
                else if (this.ViewModel.OrderBy == "按缩略图")
                {
                    this.ViewModel.Collection.Sort(l => l.Image, this.ViewModel.Desc);
                }
            });



        }

        public async Task SeletItemChanged()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this.ViewModel.SeletItem == null) return;

                var from = this.ViewModel.SeletItem.TagTypes?.Split(',').ToList();


                if (from == null)
                {
                    this.ViewModel.EditSelectTag = new ObservableCollection<mbc_db_tagtype>();
                    return;
                }

                var result = this.ViewModel.TagCollection.Where(l => from.Exists(k => k == l.Name));

                this.ViewModel.EditSelectTag.Clear();


                ObservableCollection<mbc_db_tagtype> collection = new ObservableCollection<mbc_db_tagtype>();

                foreach (var item in result)
                {
                    collection.Add(item);
                }

                this.ViewModel.EditSelectTag = collection;
            });

        }

        public async Task UpdateSelect()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SeletItem.Model, out message))
            {
                MessageService.ShowSnackMessage(message);
                return;
            }

            await this.Respository.UpdateAsync(this.ViewModel.SeletItem.Model);

            MessageService.ShowSnackMessage("保存成功！");
        }


        public async Task Play()
        {
            string file = this.ViewModel.SeletItem?.Url;

            if(File.Exists(file))
            {
                Process.Start(file);
            }
        }


        public async Task CheckEdittingChanged()
        {
            if (!this.ViewModel.IsEditting)
            {
                _clipBoardService.ClipBoardChanged = null;
                return;

            }

            _clipBoardService.ClipBoardChanged = async () =>
            {
                //Todo  ：复制的图片 
                BitmapSource bit = Clipboard.GetImage();

                if (bit != null)
                {

                    mbc_dv_movieimage image = new mbc_dv_movieimage();

                    image.MovieID = this.ViewModel.SeletItem.ID;

                    image.Text = DateTime.Now.ToDateTimeString();

                    image.TimeSpan = DateTime.Now.ToDateTimeString();

                    image.Image = ImageService.BitmapSourceToString(bit);

                    await this.Respository.AddMovieImage(image);

                    this.ViewModel.ImageCollection.Insert(0, image);
                }
            };
        }

        public async Task SelectionFilter()
        {
            var tags = this.Invoke(() =>
            {
                return this.ViewModel.SelectTag.ToList();
            });

            Func<mbc_dv_movie, bool> expression = l =>
             {

                 if (tags == null || tags.Count == 0) return true;

                 if (string.IsNullOrEmpty(l.TagTypes)) return false;

                 return tags.TrueForAll(k => l.TagTypes.Trim().Split(',').ToList().Exists(m => m == k.Name));

             };

            foreach (var item in this.ViewModel.Collection)
            {
                item.Visible = expression(item.Model);
            }
        }


        public async Task<IActionResult> Left()
        {

            this.ViewModel.GroupObject.Clear();

            this.ViewModel.GroupObject = await this._caseRespository.GetGroupObject();

            return View();
        }

        public async Task<IActionResult> Right()
        {
            return View();
        }

        public async Task<IActionResult> Refresh()
        {
            if (this.ViewModel.SelectCase == null)
            {
                MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                return await List();
            }

            if (!Directory.Exists(this.ViewModel.SelectCase.BaseUrl))
            {
                MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                return await List();
            }

            await this.Respository.RefreshMovie(this.ViewModel.SelectCase);

            return await List();

        }

        public async Task<IActionResult> Convert()
        {
            if (this.ViewModel.SeletItem == null)
            {
                MessageService.ShowSnackMessageWithNotice("请先选择案例！");
                return await List();
            }

            if (!File.Exists(this.ViewModel.SeletItem.Url))
            {
                MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
                return await List();
            }


            try
            {
                await this.Respository.ConvertMovie(this.ViewModel.SeletItem.Model);
            }
            catch (Exception ex)
            {
                MessageService.ShowSnackMessageWithNotice(ex.Message);
            }


            return await List();
        }


        public async Task Detial()
        {
            string id = this.ViewModel.SeletItem?.ID;

            var model = await this.Respository.GetMovieWIthDetial(id);

            if (model == null)
            {
                MessageService.ShowNotifyMessage("没有生成预览图，请先生成预览图！");
                return;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.ImageCollection.Clear();

                foreach (var item in model.Item2)
                {
                    this.ViewModel.ImageCollection.Add(item);
                }
            });

            var result = View();



            //ILinkActionBase link = new LinkAction();
            //link.Controller = "Movie";
            //link.Action = "Detial";

            MessageService.ShowWithLayer(result);
        }

        public async Task DeleteDeep()
        {
            if (this.ViewModel.SeletItem == null) return;

            var result = await MessageService.ShowResultMessge("确定要彻底删除文件?");

            if (result)
            {
                if (File.Exists(this.ViewModel.SeletItem.Url))
                {
                    File.Delete(this.ViewModel.SeletItem.Url);

                    MessageService.ShowSnackMessage("文件已删除：" + this.ViewModel.SeletItem?.Url);

                    await this.Remove();

                    
                }
            }
        }

        public async Task Remove()
        {

            if (this.ViewModel.SeletItem == null) return;

            await this.Respository.DeleteAsync(this.ViewModel.SeletItem.Model.ID);

            this.Invoke(() => this.ViewModel.Collection.Remove(this.ViewModel.SeletItem));
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        public async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SeletItem.Model, out message))
            {
                MessageService.ShowSnackMessage(message);
                return await Edit();
            }

            await this.Respository.UpdateAsync(this.ViewModel.SeletItem.Model);

            return await List();
        }
    }
}
