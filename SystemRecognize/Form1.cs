using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemRecognize.bean;
using SystemRecognize.utils;
using SystemRecognize.utlis;

namespace SystemRecognize
{
    public partial class Form1 : Form
    {
        private ArrayList upDateList = new ArrayList();

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



            //bool tag = DataUtils.IsSystemR("https://www.ty0009.com");
            //MessageBox.Show("==============" + tag);
            //return;

            for (int i = 0; i < Config.urlList.Count; i++)
            {
                UrlInfo urlInfo = (UrlInfo)Config.urlList[i];
                if (urlInfo != null)
                {
                    //Console.WriteLine("=============网址:" + urlInfo.baseUrl);
                    Thread t = new Thread(new ParameterizedThreadStart(this.recongnizeSystem));
                    t.Start(i);
                }
            }
        }


        private void recongnizeSystem(Object indexObj)
        {
            int index = (int)indexObj;
            UrlInfo urlInfo = (UrlInfo)Config.urlList[index];
            string tag = DataUtils.GetSystemTag(urlInfo.baseUrl);
            urlInfo.tag = tag;
            urlInfo.status = tag == "未知系统" ? 2 : 1;
            this.AddToListToUpDate(index);
        }

        private void DataInit()
        {
            //获取到用户数据
            FileUtils.ReadUserJObject(@"C:\urls.txt");
        }


        private void ViewInit()
        {
            mianDataGridView.MultiSelect = false; //只能选中一行
            foreach (DataGridViewColumn column in mianDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable; //设置列头不能点击
            }
            for (int i = 0; i < Config.urlList.Count; i++)
            {
                UrlInfo userInfo = (UrlInfo)Config.urlList[i];
                if (userInfo != null)
                {
                    int position = this.mianDataGridView.Rows.Add();
                    AddToListToUpDate(position);
                }

            }
        }

        //添加到消息队列里面
        public void AddToListToUpDate(int position)
        {
            upDateList.Add(position);
            upDateRow();
        }

        //更新表格信息
        private void upDateRow()
        {
            if (upDateList == null || upDateList.Count == 0 || upDateList.Count > 1) return;
            int index = (int)upDateList[0];
            try
            {
                UrlInfo urlInfo = (UrlInfo)Config.urlList[index];
                if (urlInfo != null)
                {
                    this.mianDataGridView.Rows[index].Cells[0].Value = urlInfo.baseUrl;

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

                    this.mianDataGridView.Rows[index].Cells[1].Value = tag;
                }
            }
            catch (SystemException e)
            {

                Console.WriteLine(e.ToString());
            }
            upDateList.RemoveAt(0);
            if (upDateList.Count >= 1)
            {
                upDateRow(); //自己消耗自己的资源  防止线程阻塞
            }
        }
    }
}

