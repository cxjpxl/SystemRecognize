using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Management;
using SystemRecognize.bean;
using SystemRecognize.utils;

namespace SystemRecognize.utlis
{
    class FileUtils
    {

        //规则 
        //  1  不允许有重复 
        //  2  admin权限最大  
        //  3  其他用户只能添加程序配置的

        public static void ReadUserJObject(string path)
        {
            
            try
            {

                StreamReader sr = new StreamReader(path, Encoding.Default);
                if (sr == null) return;
                String line;
                
                ArrayList urlList = new ArrayList();
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine("line:"+line);
                    if (line.StartsWith("http"))
                    {
                        UrlInfo urlInfo = new UrlInfo(); //添加登陆显示的用户数据
                        urlInfo.baseUrl = line.Trim();
                        urlInfo.loginUrl = line.Trim();
                        urlInfo.dataUrl = line.Trim();
                        urlList.Add(urlInfo);
                    }
                    else
                    {
                        continue;
                    }

                }
                ////用户表全部存在本地静态变量里面
                if (urlList != null && urlList.Count > 0)
                {
                    //排序
                    Config.urlList = urlList;
                }

            }
            catch (SystemException e)
            {

                Config.urlList = null;
                Console.WriteLine("读取失败！");
            }

        }

    }

}
