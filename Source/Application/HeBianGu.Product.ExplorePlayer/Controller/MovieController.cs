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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HeBianGu.Product.ExplorePlayer
{
    [Route("Movie")]
    internal class MovieController : Controller<MovieViewModel, MovieRespository>
    {

        ////TagRespository _tagRespository;

        //ClipBoardService _clipBoardService;

        ////CaseRespository _caseRespository;

        //public MovieController(TagRespository tagRespository, ClipBoardService clipBoardService, CaseRespository caseRespository)
        //{
        //    _tagRespository = tagRespository;

        //    _clipBoardService = clipBoardService;

        //    _caseRespository = caseRespository;

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        _clipBoardService.Register(Application.Current.MainWindow);
        //    });


        //}
       
        //public async Task<IActionResult> Center()
        //{
        //    return View();
        //}

        public async Task<IActionResult> List()
        {
            return View();
        }


        //public async Task DeleteSelectImage()
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        this.ViewModel.ImageCollection.Remove(this.ViewModel.SelectImage);
        //    });
        //}

        //public async Task SetViewImage()
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        //this.ViewModel.ImageCollection.Remove(this.ViewModel.SelectImage);

        //        this.ViewModel.SelectedItem.Image = this.ViewModel.SelectImage?.Image;


        //    });

        //    await this.Respository.SaveAsync();
        //}



        //public async Task SeletItemChanged()
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        if (this.ViewModel.SelectedItem == null) return;

        //        var from = this.ViewModel.SelectedItem.TagTypes?.Split(',').ToList();


        //        if (from == null)
        //        {
        //            this.ViewModel.EditSelectTag = new ObservableCollection<mbc_db_tagtype>();
        //            return;
        //        }

        //        var result = this.ViewModel.TagCollection.Where(l => from.Exists(k => k == l.Name));

        //        this.ViewModel.EditSelectTag.Clear();


        //        ObservableCollection<mbc_db_tagtype> collection = new ObservableCollection<mbc_db_tagtype>();

        //        foreach (var item in result)
        //        {
        //            collection.Add(item);
        //        }

        //        this.ViewModel.EditSelectTag = collection;
        //    });

        //}




        //public async Task CheckEdittingChanged()
        //{
        //    if (!this.ViewModel.IsEditting)
        //    {
        //        _clipBoardService.ClipBoardChanged = null;
        //        return;

        //    }

        //    _clipBoardService.ClipBoardChanged = async () =>
        //    {
        //        //Todo  ：复制的图片 
        //        BitmapSource bit = Clipboard.GetImage();

        //        if (bit != null)
        //        {

        //            mbc_dv_movieimage image = new mbc_dv_movieimage();

        //            image.MovieID = this.ViewModel.SelectedItem.ID;

        //            image.Text = DateTime.Now.ToDateTimeString();

        //            image.TimeSpan = DateTime.Now.ToDateTimeString();

        //            image.Image = ImageService.BitmapSourceToString(bit);

        //            await this.Respository.AddMovieImage(image);

        //            this.ViewModel.ImageCollection.Insert(0, image);
        //        }
        //    };
        //}
         

        //public async Task<IActionResult> Refresh()
        //{
        //    if (this.ViewModel.SelectCase == null)
        //    {
        //        MessageService.ShowSnackMessageWithNotice("请先选择案例！");
        //        return await List();
        //    }

        //    if (!Directory.Exists(this.ViewModel.SelectCase.BaseUrl))
        //    {
        //        MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
        //        return await List();
        //    }

        //    await this.Respository.RefreshMovie(this.ViewModel.SelectCase);

        //    return await List();

        //}

        //public async Task<IActionResult> Convert()
        //{
        //    if (this.ViewModel.SelectedItem == null)
        //    {
        //        MessageService.ShowSnackMessageWithNotice("请先选择案例！");
        //        return await List();
        //    }

        //    if (!File.Exists(this.ViewModel.SelectedItem.Url))
        //    {
        //        MessageService.ShowSnackMessageWithNotice("案例路径不存在，请检查！");
        //        return await List();
        //    }


        //    try
        //    {
        //        await this.Respository.ConvertMovie(this.ViewModel.SelectedItem.Model);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageService.ShowSnackMessageWithNotice(ex.Message);
        //    }


        //    return await List();
        //}


        //public async Task Detial()
        //{
        //    string id = this.ViewModel.SelectedItem?.ID;

        //    var model = await this.Respository.GetMovieWIthDetial(id);

        //    if (model == null)
        //    {
        //        MessageService.ShowNotifyMessage("没有生成预览图，请先生成预览图！");
        //        return;
        //    }

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        this.ViewModel.ImageCollection.Clear();

        //        foreach (var item in model.Item2)
        //        {
        //            this.ViewModel.ImageCollection.Add(item);
        //        }
        //    });

        //    var result = View();



        //    //ILinkActionBase link = new LinkAction();
        //    //link.Controller = "Movie";
        //    //link.Action = "Detial";

        //    MessageService.ShowWithLayer(result);
        //}

        //public async Task DeleteDeep()
        //{
        //    if (this.ViewModel.SelectedItem == null) return;

        //    var result = await MessageService.ShowResultMessge("确定要彻底删除文件?");

        //    if (result)
        //    {
        //        if (File.Exists(this.ViewModel.SelectedItem.Url))
        //        {
        //            File.Delete(this.ViewModel.SelectedItem.Url);

        //            MessageService.ShowSnackMessage("文件已删除：" + this.ViewModel.SelectedItem?.Url);

        //            await this.Remove();


        //        }
        //    }
        //}

        //public async Task Remove()
        //{

        //    if (this.ViewModel.SelectedItem == null) return;

        //    await this.Respository.DeleteAsync(this.ViewModel.SelectedItem.Model.ID);

        //    this.Invoke(() => this.ViewModel.Collection.Remove(this.ViewModel.SelectedItem));
        //}

        //public async Task<IActionResult> Edit()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Update()
        //{
        //    string message;

        //    if (!this.ModelState(this.ViewModel.SelectedItem.Model, out message))
        //    {
        //        MessageService.ShowSnackMessage(message);
        //        return await Edit();
        //    }

        //    await this.Respository.UpdateAsync(this.ViewModel.SelectedItem.Model);

        //    return await List();
        //}
    }
}
