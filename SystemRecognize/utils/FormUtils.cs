using System;
using Newtonsoft.Json.Linq;
using SystemRecognize.bean;
using System.Text.RegularExpressions;
using System.Text;

//格式化工具类
namespace SystemRecognize.utlis
{
    class FormUtils
    {
        
        //获取当前的系统时间  毫秒
        public static long getCurrentTime() {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        
        //判断是否json
        public static bool IsJsonObject(String str) {
            if (String.IsNullOrEmpty(str)) {
                return false;
            }

            if (str.StartsWith("{") && str.EndsWith("}")) {
                return true;
            }
            return false;
        }

        public static bool IsJsonArray(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return false;
            }

            if (str.StartsWith("[") && str.EndsWith("]"))
            {
                return true;
            }
            return false;
        }

    }
}
