using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace J3D_Template_Model_Generator.FileSys
{
    public class FormObjControlToTranslate
    {
        //トップメニュー
        protected static ToolStripMenuItem tsfile = Form1.Form1Instance.ファイルToolStripMenuItem;
        protected static ToolStripMenuItem tsopen = Form1.Form1Instance.開くToolStripMenuItem;
        protected static ToolStripMenuItem tssave = Form1.Form1Instance.保存ToolStripMenuItem;
        protected static ToolStripMenuItem tsfoldercheck = Form1.Form1Instance.作業フォルダチェックToolStripMenuItem;
        protected static ToolStripMenuItem tsedit = Form1.Form1Instance.編集ToolStripMenuItem;
        protected static ToolStripMenuItem tsinfo = Form1.Form1Instance.情報ToolStripMenuItem;
        //ボトムメニュー
        protected static ToolStripStatusLabel ssstate1 = Form1.Form1Instance.toolStripStatusLabel1;
        protected static ToolStripStatusLabel ssstate2 = Form1.Form1Instance.toolStripStatusLabel1;
        //メインメニュー
        protected static GroupBox gp1 = Form1.Form1Instance.groupBox1;
        protected static Label lb1 = Form1.Form1Instance.label1;
        protected static Label lb2 = Form1.Form1Instance.label2;
        protected static Label lb3 = Form1.Form1Instance.label3;
        protected static Label lb4 = Form1.Form1Instance.label4;
        protected static Label lb5 = Form1.Form1Instance.label5;
        protected static Label CMD_Error = Form1.Form1Instance.label7;
        protected static Label need_mat = Form1.Form1Instance.label6;
        protected static Button BDL_Button = Form1.Form1Instance.GenerateBdl_Button;
        protected static Button ARC_Button = Form1.Form1Instance.ConvertArc_Button;
        protected static Button Col_Button = Form1.Form1Instance.button3;
        protected static Button Wh_Button = Form1.Form1Instance.button4;
        protected static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        protected static CheckBox cb2 = Form1.Form1Instance.checkBox2;
        protected static CheckBox cb3 = Form1.Form1Instance.checkBox3;
        protected static ComboBox com1 = Form1.Form1Instance.comboBox1;
        protected static ComboBox com2 = Form1.Form1Instance.comboBox2;

        protected static TextBox tx2 = Form1.Form1Instance.NeedMaterial;
        protected static ToolStripMenuItem setting_tsmi = Form1.Form1Instance.settingsToolStripMenuItem;
        //protected static ComboBox com3 = SettingsForm.SettingsFormInstance.comboBox3;
    }
    public class Language : FormObjControlToTranslate
    {
        public static void Form1_Translater() 
        {

            switch (Properties.Settings.Default.LangageType)
            {
                case "日本語":
                    JP();
                    Properties.Settings.Default.LangageType = "日本語";
                    Properties.Settings.Default.Save();
                    break;
                case "EN":
                    EN();
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
                default:
                    EN();
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
            }
            
        }

        public static void JP() 
        {
            tsfile.Text = "ファイル";
            tsopen.Text = "保存先変更";
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
            cb2.Text = "CMDも起動(ホワイトホール)";
            cb3.Text = "任意のjsonを追加(上級者向け)";

            //BTKComboBoxItem And Text
            com1.Items.Clear();
            foreach (string combox1str in SystemFileLoader.ComboxItem_JP) com1.Items.Add(combox1str);
            com1.Text = "なし";

            //BRKComboBoxItem And Text
            com2.Items.Clear();
            com2.Items.Add("なし");
            com2.Items.Add("フラッシュブラック");
            com2.Text = "なし";
        }

        public static void EN() 
        {
            tsfile.Text = "File";
            tsopen.Text = "Save Directory Change";
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

            //BTKComboBoxItem And Text
            com1.Items.Clear();
            foreach (string combox1str in SystemFileLoader.ComboxItem_EN) com1.Items.Add(combox1str);
            com1.Text = "None";

            //BRKComboBoxItem And Text
            com2.Items.Clear();
            com2.Items.Add("None");
            com2.Items.Add("FlashBlack");
            com2.Text = "None";
        }

    }
}
