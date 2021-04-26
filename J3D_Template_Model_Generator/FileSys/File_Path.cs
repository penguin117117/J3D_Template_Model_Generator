using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using fc = J3D_Template_Model_Generator.FileSys.FolderCreate;
using mes = J3D_Template_Model_Generator.FileSys.Message;
using sff = J3D_Template_Model_Generator.FileSys.Select_File_Folder;

namespace J3D_Template_Model_Generator.FileSys
{
    class File_Path
    {
        //宣言
        protected static string mainfilePath;
        protected static string[] btktype;
        protected static string[] brktype;
        protected static string setpath;
        protected static ComboBox com2 = Form1.Form1Instance.comboBox2;
        protected static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        protected static Label lb2 = Form1.Form1Instance.label2;

        //数値をセット＆処理の実行
        public void Set(string type)
        {
            //初期化
            btktype = new string[] { "None", "Lava_Temp", "Water_Temp", "WaterFall_Temp" };
            brktype = new string[] { "None", "Flash_Black" };
            setpath = Properties.Settings.Default.設定;

            //処理
            switch (type)
            {
                case "Load":
                    Processing_Form1_Load();
                    break;
                case "BDL":
                    break;
            }

        }
        //フォームのロード時に処理する項目
        public void Processing_Form1_Load()
        {
            Root_or_Sub();
            Form1_Items_Setting();
            Language.Form1_Translater();
            Form1_Check_Working_Directory();
        }

        //ルートディレクトリかサブディレクトリかチェック
        public void Root_or_Sub()
        {
            if (setpath.Substring(setpath.Length - 1, 1) != "\\")
            {
                setpath += "\\";
            }
            lb2.Text = setpath;
            mainfilePath = setpath + "J3D_Template_Model_Generator";
            
        }

        //フォーム1のロード時のアイテム設定
        public void Form1_Items_Setting()
        {
            com2.Enabled = false;
            cb1.Checked = true;

        }

        public void Form1_Check_Working_Directory()
        {

            //作業フォルダチェック＆作成処理
            if (Directory.Exists(mainfilePath) == false)
            {
                Form1_Check_Create_Folder();
            }

        }

        public void Form1_Check_Create_Folder(bool load = true)
        {
            short mestype = 0;
            if (load==false)mestype =2 ;
            //フォルダを作成してもいいかの確認
            DialogResult result = mes.sysmes(mestype);
            if (result == DialogResult.Yes)
            {
                Folder_Selection_By_User();
                //フォルダの作成&チェック
                fc.Set_Folder();
                mes.sysmes(1);
            }
            //「いいえ」の場合
            else if (result == DialogResult.No)
            {
                if(load == true)
                Form1.Form1Instance.Close();

            }
        }

        public void Folder_Selection_By_User() 
        {
            //ユーザーにフォルダ作成位置を決めるか選ばせる
            DialogResult user_path = mes.sysmes(2);
            if (user_path == DialogResult.Yes)
            {
                //Settingsの設定を書き換え
                Properties.Settings.Default.設定 = sff.Folder_Select();
                setpath = Properties.Settings.Default.設定;
                Root_or_Sub();
                Properties.Settings.Default.設定 = setpath;

                //Settingsの設定を保存
                Properties.Settings.Default.Save();

            }

            //指定したディレクトリにフォルダがないかの最終確認
            if (Directory.Exists(mainfilePath + "J3D_Template_Model_Generator") == false)
            {

            }
            else
            {
                mes.sysmes(4);
            }
        }

        
    }
    
}
