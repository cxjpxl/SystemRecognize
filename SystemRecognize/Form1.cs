using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemRecognize.bean;
using SystemRecognize.utils;
using SystemRecognize.utlis;

namespace SystemRecognize
{
    public partial class Form1 : Form
    {
        private DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataInit(); //读取配置文件
            if (Config.urlList == null || Config.urlList.Count == 0)
            {
                MessageBox.Show("读取配置文件出错");
                Application.Exit();
                return;
            }

            ViewInit();

            for (int i = 0; i < Config.urlList.Count; i++)
            {
                UrlInfo urlInfo = (UrlInfo)Config.urlList[i];
                if (urlInfo != null)
                {
                    Console.WriteLine("=============网址:" + urlInfo.baseUrl);
                    if (i>=0)
                    {
                        string tag = DataUtils.GetSystemTag(urlInfo.baseUrl);
                        MessageBox.Show(urlInfo.baseUrl + "=================" + tag);

                    }
                }
            }
        }


        private void DataInit()
        {
            //获取到用户数据
            FileUtils.ReadUserJObject(@"C:\urls.txt");
        }

        //初始化数据源
        private void ViewInit()
        {

            this.dt = new DataTable();
            dt.Columns.Add("0");
            dt.Columns.Add("1");
            this.mianDataGridView.DataSource = this.dt;

            this.updateUI();
            
        }

        public void updateUI()
        {
            this.dt.Clear();
            for (int i = 0; i < Config.urlList.Count; i++)
            {
                UrlInfo urlInfo = (UrlInfo)Config.urlList[i];
                if (urlInfo != null)
                {
                    string tag = "";
                    switch (urlInfo.status)
                    {
                        case -1:
                            tag = "尚未进行识别";
                            break;
                        case 0:
                            tag = "系统识别中...";
                            break;
                        case 1:
                            tag = urlInfo.tag;
                            break;
                        case 2:
                            tag = "系统识别失败";
                            break;
                    }

                    this.dt.Rows.Add( urlInfo.baseUrl.ToString(), tag.ToString() );
                }
            }

        }

    }
}
