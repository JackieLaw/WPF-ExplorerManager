using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.Respository.ViewModel
{

    public class MovieModelViewModel : ModelViewModel<mbc_dv_movie>
    {
        public MovieModelViewModel() : base()
        {

        }
        public MovieModelViewModel(mbc_dv_movie t) : base(t)
        {

        }

        /// <summary> 说明 </summary>
        public string Name
        {
            get { return this.Model.Name; }
            set
            {
                this.Model.Name = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string MediaType
        {
            get { return this.Model.MediaType; }
            set
            {
                this.Model.MediaType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string CaseType
        {
            get { return this.Model.CaseType; }
            set
            {
                this.Model.CaseType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Url
        {
            get { return this.Model.Url; }
            set
            {
                this.Model.Url = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string TagTypes
        {
            get { return this.Model.TagTypes; }
            set
            {
                this.Model.TagTypes = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string AreaType
        {
            get { return this.Model.AreaType; }
            set
            {
                this.Model.AreaType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string ExtendType
        {
            get { return this.Model.ExtendType; }
            set
            {
                this.Model.ExtendType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string ArticulationType
        {
            get { return this.Model.ArticulationType; }
            set
            {
                this.Model.ArticulationType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public long Size
        {
            get { return this.Model.Size; }
            set
            {
                this.Model.Size = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Image
        {
            get { return this.Model.Image; }
            set
            {
                this.Model.Image = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string VipType
        {
            get { return this.Model.VipType; }
            set
            {
                this.Model.VipType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string FromType
        {
            get { return this.Model.FromType; }
            set
            {
                this.Model.FromType = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string OrderNum
        {
            get { return this.Model.OrderNum; }
            set
            {
                this.Model.OrderNum = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string PlayCount
        {
            get { return this.Model.PlayCount; }
            set
            {
                this.Model.PlayCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public int Score
        {
            get { return this.Model.Score.ToInt(); }
            set
            {
                this.Model.Score = value.ToString();
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Duration
        {
            get { return this.Model.Duration; }
            set
            {
                this.Model.Duration = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Bitrate
        {
            get { return this.Model.Bitrate; }
            set
            {
                this.Model.Bitrate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string MediaCode
        {
            get { return this.Model.MediaCode; }
            set
            {
                this.Model.MediaCode = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string VedioType
        {
            get { return this.Model.VedioType; }
            set
            {
                this.Model.VedioType = value;
                RaisePropertyChanged();
            }
        }


        /// <summary> 说明 </summary>
        public string Resoluction
        {
            get { return this.Model.Resoluction; }
            set
            {
                this.Model.Resoluction = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Aspect
        {
            get { return this.Model.Aspect; }
            set
            {
                this.Model.Aspect = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string Rate
        {
            get { return this.Model.Rate; }
            set
            {
                this.Model.Rate = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string CDATE
        {
            get { return this.Model.CDATE; }
            set
            {
                this.Model.CDATE = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string UDATE
        {
            get { return this.Model.UDATE; }
            set
            {
                this.Model.UDATE = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public int ISENBLED
        {
            get { return this.Model.ISENBLED; }
            set
            {
                this.Model.ISENBLED = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 说明 </summary>
        public string ID
        {
            get { return this.Model.ID; }
            set
            {
                this.Model.ID = value;
                RaisePropertyChanged();
            }
        }

    }



}
