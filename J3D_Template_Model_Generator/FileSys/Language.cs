using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace J3D_Template_Model_Generator.FileSys
{
    public class Language
    {
        //トップメニュー
        public static ToolStripMenuItem tsfile = Form1.Form1Instance.ファイルToolStripMenuItem;
        public static ToolStripMenuItem tsopen = Form1.Form1Instance.開くToolStripMenuItem;
        public static ToolStripMenuItem tssave = Form1.Form1Instance.保存ToolStripMenuItem;
        public static ToolStripMenuItem tsfoldercheck = Form1.Form1Instance.作業フォルダチェックToolStripMenuItem;
        public static ToolStripMenuItem tsedit = Form1.Form1Instance.編集ToolStripMenuItem;
        public static ToolStripMenuItem tsinfo = Form1.Form1Instance.情報ToolStripMenuItem;
        //ボトムメニュー
        public static ToolStripStatusLabel ssstate1 = Form1.Form1Instance.toolStripStatusLabel1;
        public static ToolStripStatusLabel ssstate2 = Form1.Form1Instance.toolStripStatusLabel1;
        //メインメニュー
        public static GroupBox gp1 = Form1.Form1Instance.groupBox1;
        public static Label lb1 = Form1.Form1Instance.label1;
        public static Label lb2 = Form1.Form1Instance.label2;
        public static Label lb3 = Form1.Form1Instance.label3;
        public static Label lb4 = Form1.Form1Instance.label4;
        public static Label lb5 = Form1.Form1Instance.label5;
        public static Label CMD_Error = Form1.Form1Instance.label7;
        public static Label need_mat = Form1.Form1Instance.label6;
        public static Button BDL_Button = Form1.Form1Instance.GenerateBdl_Button;
        public static Button ARC_Button = Form1.Form1Instance.ConvertArc_Button;
        public static Button Col_Button = Form1.Form1Instance.button3;
        public static Button Wh_Button = Form1.Form1Instance.button4;
        public static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        public static CheckBox cb2 = Form1.Form1Instance.checkBox2;
        public static CheckBox cb3 = Form1.Form1Instance.checkBox3;
        public static ComboBox com1 = Form1.Form1Instance.comboBox1;
        public static ComboBox com2 = Form1.Form1Instance.comboBox2;

        public static TextBox tx2 = Form1.Form1Instance.NeedMaterial;
        public static ToolStripMenuItem setting_tsmi = Form1.Form1Instance.settingsToolStripMenuItem;
        //public static ComboBox com3 = SettingsForm.SettingsFormInstance.comboBox3;

        public static void Form1_Translater() 
        {

            switch (Properties.Settings.Default.LangageType)
            {
                case "日本語":
                    JP();
                    //com3.SelectedIndex = 0;
                    Properties.Settings.Default.LangageType = "日本語";
                    Properties.Settings.Default.Save();
                    break;
                case "EN":
                    EN();
                    //com3.SelectedIndex = 1;
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
                default:
                    EN();
                    //com3.SelectedIndex = 1;
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
            }
            
        }

        public static void JP() 
        {
            tsfile.Text = "ファイル";
            tsopen.Text = "開く";
            tssave.Text = "保存(未実装)";
            tsfoldercheck.Text = "作業フォルダチェック";
            tsedit.Text = "編集";
            tsinfo.Text = "情報";
            setting_tsmi.Text = "設定";
            ssstate1.Text = "状態：";
            gp1.Text = "設定";
            lb1.Text = "保存先";
            lb3.Text = "BTK設定";
            lb4.Text = "BRK設定";
            lb5.Text = "FBX,OBJ名";
            need_mat.Text = "必要なマテリアル";
            CMD_Error.Text = "CMDエラーメッセージ";
            BDL_Button.Text = "BDLファイルを生成";
            ARC_Button.Text = "ARC圧縮";
            Col_Button.Text = "コリジョンファイル生成";
            Wh_Button.Text = "ホワイトホール起動";
            cb2.Text = "CMDも起動";
            cb3.Text = "任意のjsonを追加(上級者向け)";
            com1.Items.Clear();
            com1.Items.Add("なし");
            com1.Items.Add("溶岩");
            com1.Items.Add("水");
            com1.Items.Add("滝");
            com1.Items.Add("毒");
            com1.Items.Add("流砂");
            com1.Items.Add("流れる砂");
            com1.Items.Add("泥");
            com1.Items.Add("グライバード水");
            com2.Items.Clear();
            com2.Items.Add("なし");
            com2.Items.Add("フラッシュブラック");
            com1.Text = "なし";
            com2.Text = "なし";
            //tx2.Text = "なし";
        }

        public static void EN() 
        {
            tsfile.Text = "File";
            tsopen.Text = "Open";
            tssave.Text = "Save(Unimplemented)";
            tsfoldercheck.Text = "Working folder check";
            tsedit.Text = "Edit";
            tsinfo.Text = "Info";
            setting_tsmi.Text = "Settings";
            ssstate1.Text = "State：";
            gp1.Text = "Setting";
            lb1.Text = "Save Directory";
            lb3.Text = "BTK Settings";
            lb4.Text = "BRK Settings";
            lb5.Text = "FBX/OBJ　Names";
            need_mat.Text = "Material Name(s)";
            CMD_Error.Text = "CMD Error Message ";
            BDL_Button.Text = "Generate BDL";
            ARC_Button.Text = "ARC Compression";
            Col_Button.Text = "Generate Collision";
            Wh_Button.Text = "Open Whitehole";
            cb2.Text = "Start CMD ";
            cb3.Text = "Add Any .Json File (Advanced)";
            com1.Items.Clear();
            com1.Items.Add("None");
            com1.Items.Add("Lava");
            com1.Items.Add("Water");
            com1.Items.Add("WaterFall");
            com1.Items.Add("Poison");
            com1.Items.Add("Quicksand");
            com1.Items.Add("Slipsand");
            com1.Items.Add("Mud");
            com1.Items.Add("GliderStarWater");
            com2.Items.Clear();
            com2.Items.Add("None");
            com2.Items.Add("FlashBlack");
            com1.Text = "None";
            com2.Text = "None";
            //tx2.Text = "None";
        }

    }
}
