using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.ExplorePlayer.General.FFmpegService
{
    public class FFmpegService
    {
        public static FFmpegService Instance = new FFmpegService();


        FFmpegProcess _ffmpegProcess = new FFmpegProcess();

        FFmpegConvert _ffmpegConvert = new FFmpegConvert();

        FFmpegParameter _ffmpegParameter = new FFmpegParameter();

        public void ConvertWithParams(string param, Action<string> progressAction, Action<int> existAction)
        {
            Action<string> action = l =>
            {
                progressAction(this._ffmpegConvert.GetProgress(l));
            };

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

        }

        public void MediaToWmv(string from, string to, Action<string> progressAction, Action<int> existAction)
        {
            Action<string> action = l =>
            {
                progressAction(this._ffmpegConvert.GetProgress(l));

            };

            string param = string.Format(_ffmpegParameter.media_to_wmv, from, to);

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

        }

        public void MediaToMP3(string from, string to, Action<string> progressAction, Action<int> existAction)
        {

            Action<string> action = l =>
              {
                  progressAction(this._ffmpegConvert.GetProgress(l));
              };

            string param = string.Format(_ffmpegParameter.mToSound, from, to);

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

        }

        public void ShootCut(string from, string to, string timespan)
        {
            if (!Directory.Exists(Path.GetDirectoryName(to)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(to));
            }

            string param = string.Format(FFmpegParameter.ffmpeg_screen_shot, from, timespan, to);

            //_ffmpegProcess.ExecuteWithRecevied(param, l => Console.WriteLine(l), l => Console.WriteLine(l), Exited);

            _ffmpegProcess.ExecuteWithOutWait(param);

        }

        /// <summary> int timespan = 60 一分钟截图一次 string span= "00:10:00" 取前十分钟 </summary>
        public List<string> ShootCutBat(string from, string to, int timespan = 60, int spansecond = 600)
        {
            if (!Directory.Exists(Path.GetDirectoryName(to)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(to));
            }

            string span = TimeSpan.FromSeconds(spansecond).ToString();

            string param = string.Format(FFmpegParameter.ffmpeg_screen_shot_bat, from, timespan, span, to);

            List<string> retsult = new List<string>();

            int count = spansecond / timespan;

            for (int i = 0; i < count; i++)
            {
                string path = to + (i + 1).ToString().PadLeft(5, '0') + ".jpg";

                retsult.Add(path);
            }

            //Action<int> ExitedInner = l =>
            //  {
            //      Exited?.Invoke(l, retsult);
            //  };

            //_ffmpegProcess.ExecuteWithRecevied(param, null, null, ExitedInner);

            _ffmpegProcess.ExecuteWithOutWait(param);

            return retsult;

        }


        public List<SupportFormatEntity> GetFormats()
        {
            string result = _ffmpegProcess.ExecuteWithOutWait(_ffmpegParameter.ffmpeg_formats);

            return _ffmpegConvert.GetFomarts(result);
        }

        public List<SupportFormatEntity> GetCodecs()
        {
            string result = _ffmpegProcess.ExecuteWithOutWait(_ffmpegParameter.ffmpeg_codecs);

            return _ffmpegConvert.GetCodecs(result);
        }

        public string GetDetail(string from)
        {
            string param = string.Format(_ffmpegParameter.ffmpeg_detial, from);

            var result = _ffmpegProcess.ExecuteWithErr(param);

            return result;
        }


        #region - 转换命令 -

        public MediaEntity GetMediaEntity(string filePath)
        {
            string txt = this.GetDetail(filePath);

            return _ffmpegConvert.GetMediaEntity(txt);
        }

        #endregion
    }
}
