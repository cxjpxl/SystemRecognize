
using System;
using System.Collections;

namespace SystemRecognize.utils
{
    public class Config
    {
       
        //用户配置登录数据结构
        public static ArrayList urlList = null; // 网址列表


        public static bool isDeug = false;  //控制打印
        public static String userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3253.3 Safari/537.36";              
        public static void console(String str) {
            if(isDeug) Console.WriteLine(str);
        }
        
    }
}
