using SystemRecognize.bean;
using SystemRecognize.utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemRecognize.utlis
{
    public class DataUtils
    {
        private static Stopwatch stopWatch = new Stopwatch();
        /**************************判斷網址屬於哪一個系統****************************/
        public static string GetSystemTag(string url)
        {
            string tag = "未知系统";



            stopWatch.Start();
            Parallel.Invoke(() =>
            {
                Console.WriteLine("=====IsSystemA======" + tag);
                bool flag = IsSystemA(url);
                if (flag)
                {
                    tag = "A";
                }
            }, () => {
                Console.WriteLine("=====IsSystemB======" + tag);
                bool flag = IsSystemB(url);
                if (flag)
                {
                    tag = "B";
                }
            }, () => {
                Console.WriteLine("=====IsSystemI======" + tag);
                bool flag = IsSystemI(url);
                if (flag)
                {
                    tag = "I";
                }
            }, () => {
                Console.WriteLine("=====IsSystemU======" + tag);
                bool flag = IsSystemU(url);
                if (flag)
                {
                    tag = "U";
                }
            }, () => {
                Console.WriteLine("=====IsSystemR======" + tag);
                bool flag = IsSystemR(url);
                if (flag)
                {
                    tag = "R";
                }
            }, () => {
                Console.WriteLine("=====IsSystemG======" + tag);
                bool flag = IsSystemG(url);
                if (flag)
                {
                    tag = "G";
                }
            }, () => {
                Console.WriteLine("=====IsSystemK======" + tag);
                bool flag = IsSystemK(url);
                if (flag)
                {
                    tag = "K";
                }
            });
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            //var firstTask = new Task<bool>(() => {
            //    return IsSystemA(url);
            //});
            //var secondTask = new Task<bool>(() =>
            //{
            //    return IsSystemB(url);
            //});
            //var whenAllTask = Task.WhenAll(firstTask, secondTask);
            //whenAllTask.ContinueWith(
            //    t => Console.WriteLine("The first answer is {0}, the second is {1}", t.Result[0], t.Result[1]),
            //    TaskContinuationOptions.OnlyOnRanToCompletion
            //);

            //firstTask.Start();
            //secondTask.Start();

            Console.WriteLine("tag===========" + tag);

            return tag;
        }

        private static bool IsSystemA(string url)
        {
            String uidUrl = url + "/sport.aspx";
            String rlt = HttpUtils.httpGet(uidUrl, "", new System.Net.CookieContainer());
            if (String.IsNullOrEmpty(rlt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemB(string url)
        {
            String getDataUrl = url + "/show/ft_gunqiu_data.php?leaguename=&CurrPage=0&_=" + FormUtils.getCurrentTime();
            String rlt = HttpUtils.httpGet(getDataUrl, "", null);
            if (String.IsNullOrEmpty(rlt) || !rlt.Contains("Match_Master"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemI(string url)
        {
            String getDataUrl = url + "/app/hsport/sports/match";
            String paramsStr = "t=" + FormUtils.getCurrentTime() + "&day=2&class=1&type=1&page=1&num=10000&league=";
            JObject headJObject = new JObject();
            headJObject["Host"] = url;
            headJObject["Origin"] = url;
            headJObject["Referer"] = url + "/hsport/index.html";
            String rlt = HttpUtils.HttpPostHeader(getDataUrl, paramsStr, "application/x-www-form-urlencoded; charset=UTF-8", null, headJObject);
            if (String.IsNullOrEmpty(rlt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemU(string url)
        {
            String uid = "";// userInfo.uid;
            if (String.IsNullOrEmpty(uid)) uid = "";
            JObject headJObject = new JObject();
            headJObject["Host"] = url;// userInfo.baseUrl;
            String getDataUrl = url + "/app/member/FT_browse/body_var?uid=" + uid + "&rtype=re&langx=zh-cn&mtype=3&page_no=0&league_id=&hot_game=";
            String rlt = HttpUtils.HttpGetHeader(getDataUrl, "", null, headJObject);
            if (String.IsNullOrEmpty(rlt) || !rlt.Contains("t_page"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemR(string url)
        {
            String getDataUrl = url.Replace("www", "mkt") + "/foot/redata/" + "1";
            String baseUrl = url.Replace("www", "mkt");
            JObject headJObject = new JObject();
            headJObject["Host"] = baseUrl.Replace("http://", "");
            headJObject["Origin"] = baseUrl;
            headJObject["Referer"] = baseUrl + "/foot/re";

            String rlt = HttpUtils.HttpPostHeader(getDataUrl, "", "", null, headJObject);
            if (String.IsNullOrEmpty(rlt) || !rlt.Contains("LeagueTr"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemG(string url)
        {
            String getDataUrl = url + "/index.php/sports/Match/FootballPlaying?t=" + FormUtils.getCurrentTime();
            JObject headJObject = new JObject();
            headJObject["Host"] = url;
            headJObject["Origin"] = url;
            headJObject["Referer"] = url + "/index.php/sports/main?token=&uid=";
            String p = "p=1&oddpk=H&leg=";
            String rlt = HttpUtils.HttpPostHeader(getDataUrl, p, "application/x-www-form-urlencoded; charset=UTF-8", null, headJObject);

            if (String.IsNullOrEmpty(rlt) || !FormUtils.IsJsonObject(rlt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool IsSystemK(string url)
        {
            JObject headJObject = new JObject();
            CookieContainer cookie = new CookieContainer();

            headJObject["Host"] = url;// userInfo.baseUrl;
            headJObject["Referer"] = url;//userInfo.dataUrl;
            String getUidUrl = url + "/app/member/";//userInfo.dataUrl + "/app/member/";
            String uidRlt = HttpUtils.HttpGetHeader(getUidUrl, "", cookie, headJObject);
            if (String.IsNullOrEmpty(uidRlt) || !uidRlt.Contains("uid="))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
