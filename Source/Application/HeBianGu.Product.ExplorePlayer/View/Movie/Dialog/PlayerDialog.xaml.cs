using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Product.ExplorePlayer.View.Movie.Dialog
{
    /// <summary>
    /// PlayerDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerDialog : UserControl
    {
        public PlayerDialog()
        {
            InitializeComponent();
        }


        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(PlayerDialog), new PropertyMetadata(default(Uri), (d, e) =>
             {
                 PlayerDialog control = d as PlayerDialog;

                 if (control == null) return;

                 Uri config = e.NewValue as Uri;

                 control.vlc.VedioSource = config;


             }));



        public ObservableCollection<TimeFlagViewModel> Times
        {
            get { return (ObservableCollection<TimeFlagViewModel>)GetValue(TimesProperty); }
            set { SetValue(TimesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimesProperty =
            DependencyProperty.Register("Times", typeof(ObservableCollection<TimeFlagViewModel>), typeof(PlayerDialog), new PropertyMetadata(default(ObservableCollection<TimeSpan>), (d, e) =>
             {
                 PlayerDialog control = d as PlayerDialog;

                 if (control == null) return;

                 ObservableCollection<TimeFlagViewModel> config = e.NewValue as ObservableCollection<TimeFlagViewModel>;

                 control.vlc.ItemSource = config;
             }));



        public TimeFlagViewModel Current
        {
            get { return (TimeFlagViewModel)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(TimeFlagViewModel), typeof(PlayerDialog), new PropertyMetadata(default(TimeFlagViewModel), (d, e) =>
             {
                 PlayerDialog control = d as PlayerDialog;

                 if (control == null) return;

                 TimeFlagViewModel config = (TimeFlagViewModel)e.NewValue;

                 if (config == null) return;

                 control.vlc.Time = config.TimeSpan;

             }));




        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.vlc.Stop();
        }

        private void FButton_Flag_Click(object sender, RoutedEventArgs e)
        {
            this.OnFlagClick();
        }

        public TimeSpan GetTime()
        {
           return this.vlc.GetTime();
        }

        public ImageSource GetVlc()
        {
            return this.vlc.GetVlc();
        }

        //声明和注册路由事件
        public static readonly RoutedEvent FlagClickRoutedEvent =
            EventManager.RegisterRoutedEvent("FlagClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PlayerDialog));
        //CLR事件包装
        public event RoutedEventHandler FlagClick
        {
            add { this.AddHandler(FlagClickRoutedEvent, value); }
            remove { this.RemoveHandler(FlagClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnFlagClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(FlagClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

        public async Task<string> BeginShootCut()
        {
            return await this.vlc.BeginShootCut();
        }
    }

    /// <summary> 说明</summary>
    public class TimeFlagViewModel : NotifyPropertyChanged
    {
        #region - 属性 -

        private string _display;
        /// <summary> 说明  </summary>
        [Display(Name ="描叙信息")]
        public string DisPlay
        {
            get { return _display; }
            set
            {
                _display = value;
                RaisePropertyChanged("DisPlay");
            }
        }


        private TimeSpan _timeSpan;
        /// <summary> 说明  </summary>
        [Display(Name = "时间")]
        [Editable(false)]
        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set
            {
                _timeSpan = value;
                RaisePropertyChanged("TimeSpan");
            }
        }

    

        #endregion

    }

}
