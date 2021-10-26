using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using J3D_Template_Model_Generator.FileSys;

namespace J3D_Template_Model_Generator
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        //private static SettingsForm _settingsFormInstance;
        //public static SettingsForm SettingsFormInstance
        //{
        //    get => _settingsFormInstance;
        //    set => _settingsFormInstance = value;
        //}

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LangageType = comboBox3.Text;
            Properties.Settings.Default.Save();
            Language.Form1_Translater();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.LangageType) 
            {
                case "日本語":
                    comboBox3.SelectedIndex = 0;
                    Properties.Settings.Default.LangageType = "日本語";
                    Properties.Settings.Default.Save();
                    break;
                case "EN":
                    comboBox3.SelectedIndex = 1;
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
                default:
                    comboBox3.SelectedIndex = 1;
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
            }
            //comboBox3.SelectedIndex = 0;
            //SettingsForm f = new SettingsForm();
            //SettingsForm.SettingsFormInstance = f;
            //SettingsForm.SettingsFormInstance = this;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Language.Form1_Translater();
        }
    }
}
