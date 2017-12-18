using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecognize.bean
{
    public class UrlInfo
    {
        public String baseUrl = "";//用户网址
        public String loginUrl = "";//登录使用接口前缀
        public String dataUrl = "";//获取数据的接口

        public String tag = null;//网址系统


        public int status = -1;//网址识别状态 -1：未进行识别 0：识别中 1：识别成功 2：识别失败
    }
}
