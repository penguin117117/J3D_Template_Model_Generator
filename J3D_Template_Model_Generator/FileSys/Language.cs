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
        public static Button BDL_Button = Form1.Form1Instance.button1;
        public static Button ARC_Button = Form1.Form1Instance.button2;
        public static Button Col_Button = Form1.Form1Instance.button3;
        public static Button Wh_Button = Form1.Form1Instance.button4;
        public static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        public static CheckBox cb2 = Form1.Form1Instance.checkBox2;
        public static CheckBox cb3 = Form1.Form1Instance.checkBox3;
        public static ComboBox com1 = Form1.Form1Instance.comboBox1;

        public static TextBox tx2 = Form1.Form1Instance.textBox2;
        public static void JP() 
        {
            tsfile.Text = "ファイル";
            tsopen.Text = "開く";
            tssave.Text = "保存(未実装)";
            tsfoldercheck.Text = "作業フォルダチェック";
            tsedit.Text = "編集";
            ssstate1.Text = "状態：";
            gp1.Text = "設定";
            lb1.Text = "保存先";
            lb3.Text = "BTK設定";
            lb4.Text = "BTK設定";
            lb5.Text = "FBX,OBJ名";
            need_mat.Text = "必要なマテリアル";
            CMD_Error.Text = "CMDエラーメッセージ";
            BDL_Button.Text = "BDLファイルを生成";
            ARC_Button.Text = "ARC圧縮";
            Col_Button.Text = "コリジョンファイルを生成";
            Wh_Button.Text = "ホワイトホール起動";
            cb2.Text = "CMDも起動";
            cb3.Text = "任意のjsonを追加(上級者向け)";
            com1.Items.Clear();
            com1.Items.Add("なし");
            com1.Items.Add("溶岩");
            com1.Items.Add("水");
            com1.Items.Add("滝");
            com1.Text = "なし";
            tx2.Text = "なし";
        }

        public static void EN() 
        {
            tsfile.Text = "File";
            tsopen.Text = "Open";
            tssave.Text = "Save(Unimplemented)";
            tsfoldercheck.Text = "Working folder check";
            tsedit.Text = "Edit";
            ssstate1.Text = "State：";
            gp1.Text = "Setting";
            lb1.Text = "Save Directory";
            lb3.Text = "BTK Setting";
            lb4.Text = "BRK Setting";
            lb5.Text = "FBX,OBJ　Name";
            need_mat.Text = "Need Material";
            CMD_Error.Text = "CMD Error Message ";
            BDL_Button.Text = "Generate BDL";
            ARC_Button.Text = "ARC Compression";
            Col_Button.Text = "Generate Collision";
            Wh_Button.Text = "Open Whitehole";
            cb2.Text = "CMD Also Started ";
            cb3.Text = "Add Any Json File (Advanced)";
            com1.Items.Clear();
            com1.Items.Add("None");
            com1.Items.Add("Lava");
            com1.Items.Add("Water");
            com1.Items.Add("WaterFall");
            com1.Text = "None";
            tx2.Text = "None";
        }

    }
}
