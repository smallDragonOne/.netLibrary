using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace library.Common
{
    public class JsonHelper
    {

        /// <summary>
        /// json转换为字典
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Dictionary<string,string> JosonToDic(string json)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            JObject jo = JObject.Parse(json);
            foreach (var p in jo.Properties())
            {
                dic.Add(p.Name, p.Value.ToString());
            }
            return dic;
                
         }
    }
}