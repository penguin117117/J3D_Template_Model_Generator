using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace J3D_Template_Model_Generator
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            FileVersionInfo AppVer = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            label1.Text = AppVer.FileVersion.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            Process.Start("https://github.com/penguin117117/J3D_Template_Model_Generator/issues");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linkLabel2.LinkVisited = true;
            //Process.Start("https://github.com/penguin117117/J3D_Template_Model_Generator/issues");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.LinkVisited = true;
            Process.Start("https://github.com/penguin117117/J3D_Template_Model_Generator/releases");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel4.LinkVisited = true;
            Process.Start("https://github.com/KairosSMG/J3D-Template-Model-Generator/releases");
        }
    }
}
