using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using library.Extension;

namespace library.Common
{
    public class Utils
    {

        /// <summary>
        /// 时间转换成字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToStr(DateTime date)
        {
            if (date == null)
            {
                return DateTime.Now.ToString("yyyy-MM-dd").Trim();
            }
            else
            {
                return date.ToString("yyyy-MM-dd").Trim();
            }
        }

        

        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="time">时间戳</param>
        /// <returns>日期</returns>
        public static DateTime UnixTimeToTime(string time)
        {
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ltime = long.Parse(time + "0000000");
            TimeSpan toNow = new TimeSpan(ltime);
            return dt.Add(toNow);
        }


        /// <summary>
        ///日期转换为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns>时间</returns>
        public static long TimeToUxtime(DateTime time)
        {
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - dt).TotalSeconds;
        }


        /// <summary>
        /// sql 文本 (过滤特殊字符)
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//去除,
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            return sqlText;
        }


        /// <summary>
        /// 替换换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplcaeChangeLine(string str)
        {
            str = Regex.Replace(str, @"[\n]", "<br>");
            return str;
        }


        /// <summary>
        ///  获取配置文件
        /// </summary>
        public static string GetConfingValue(string key)
        {
            string str = "";
            str = ConfigurationManager.AppSettings[key];
            return str;
        }

        /// <summary>
        /// 字典中获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDicValue(string key, Dictionary<string, string> model)
        {
            string str = "";
            model.TryGetValue(key, out str);
            if (str == null)
            {
                str = "";
            }
            return str;

        }


        /// <summary>
        /// 简单字典反射到model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T GetModel<T>(T model,Dictionary<string,string> dic) where T : class
        {
            Type m = model.GetType();

            foreach (var p in m.GetProperties())
            {
                string name = p.Name;
                foreach (var d in dic)
                {
                    if (d.Key == name)
                    {
                        object value = string.IsNullOrWhiteSpace(d.Value)  ? null : Convert.ChangeType(d.Value, p.PropertyType);
                        p.SetValue(model, value);
             
                    }
                }
            }
            return model;
        }


        /// <summary>
        ///  获取多媒体长度
        /// </summary>
        /// <param name="filePath">读取文件位置</param>
        /// <param name="ffmpgePath">ffmpeg位置</param>
        /// <returns></returns>
        public static int getMediaLength(string filePath,string ffmpgePath)
        {
            string path = HttpContext.Current.Server.MapPath("~" + filePath);
            int length = 0;
            try
            {
                using (System.Diagnostics.Process pro = new System.Diagnostics.Process())
                {
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.ErrorDialog = false;
                    pro.StartInfo.RedirectStandardError = true;
                    pro.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/" + ffmpgePath);
                    pro.StartInfo.Arguments = "  -i  " + HttpContext.Current.Server.MapPath("~" + filePath);

                    pro.Start();
                    StreamReader errorReader = pro.StandardError;
                    pro.WaitForExit(1000);
                    string result = errorReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = result.Substring(result.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00").Length);
                        string[] arr = result.Split(':');
                        length = arr[0].ToInt() * 3600 + arr[1].ToInt() * 60 + arr[2].ToInt() + 1;
                    }
                }
            }
            catch (Exception)
            {
                length = 0;
            }

            return length;
        }

    }
}