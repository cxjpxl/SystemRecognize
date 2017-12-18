using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemRecognize.utils;
using SystemRecognize.utlis;

namespace SystemRecognize
{
    public partial class Form1 : Form
    {
        

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


        }


        private void DataInit()
        {
            //获取到用户数据
            FileUtils.ReadUserJObject(@"C:\urls.txt");
        }

    }
}
