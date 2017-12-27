using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Management;
using SystemRecognize.bean;
using SystemRecognize.utils;
using System.Windows.Forms;

namespace SystemRecognize.utlis
{
    class FileUtils
    {

        //规则 
        //  1  不允许有重复 
        //  2  admin权限最大  
        //  3  其他用户只能添加程序配置的

        public static void ReadTargetUrls(string path)
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

        // 输出识别成功的系统和网址
        public static void WriteSuccessRlt(string str)
        {

            try
            {
                //判断文件是否存在  
                if (!File.Exists(Application.StartupPath + "\\UrlSuccessRlt.txt"))
                {
                    FileStream fs1 = new FileStream(Application.StartupPath + "\\UrlSuccessRlt.txt", FileMode.Create, FileAccess.Write);//创建写入文件   
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(str);

                    sw.Close();
                    fs1.Close();

                }
                else
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\UrlSuccessRlt.txt", true);
                    sw.WriteLine(str);
                    sw.Close();//写入
                }


            }
            catch (SystemException e)
            {
                MessageBox.Show("写入失败！"+"\n"+str);
            }

        }

        // 输出识别失败的系统和网址
        public static void WriteFailRlt(string str)
        {

            try
            {
                //判断文件是否存在  
                if (!File.Exists(Application.StartupPath + "\\urls.txt"))
                {
                    FileStream fs1 = new FileStream(Application.StartupPath + "\\urls.txt", FileMode.Create, FileAccess.Write);//创建写入文件   
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(str);

                    sw.Close();
                    fs1.Close();

                }
                else
                {
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\urls.txt", true);
                    sw.WriteLine(str);
                    sw.Close();//写入
                }


            }
            catch (SystemException e)
            {
                Console.WriteLine("写入失败！");
            }

        }











    }

}
