using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PageGenerator.Util;
using SqlSugar;

namespace PageGenerator
{
    public partial class Form1 : Form
    {
        Util.Util util = new Util.Util();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tables = util.LoadTables();
            cboxTableNames.DataSource = tables.ToList();
            cboxTableNames.DisplayMember = nameof(DbTableInfo.Name);
            cboxTableNames.ValueMember = nameof(DbTableInfo.Name);

            webBrowser.ObjectForScripting = this;
            webBrowser.DocumentText = HtmlUtil.GetHtmlContent("main.html");

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            webBrowser.Document.InvokeScript("manager.methods.test()");
        }

        private void cboxTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tb = (DbTableInfo)cboxTableNames.SelectedItem;
            var columns = util.GetColumns(tb.Name);
            if (webBrowser.Document != null)
            {
                webBrowser.Document.InvokeScript("setData", new string[] { "columns", JsonConvert.SerializeObject(columns) });
            }
        }
    }
}
