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
using efe = J3D_Template_Model_Generator.FileSys.External_File_Executor;
using fe = J3D_Template_Model_Generator.FileSys.File_Edit;

namespace J3D_Template_Model_Generator.FileSys
{
    class File_Path_State 
    {
        //宣言
        protected static string mainfilePath;
        protected static string[] btktype = new string[] { "None", "Lava_Temp", "Water_Temp", "WaterFall_Temp" };
        protected static string[] brktype = new string[] { "None", "Flash_Black_Temp" };
        protected static string setpath = Properties.Settings.Default.設定;
        
        //フォームコントロールのインスタンス作成
        //トップメニュー
        protected static ToolStripMenuItem tsfile = Form1.Form1Instance.ファイルToolStripMenuItem;
        protected static ToolStripMenuItem tsopen = Form1.Form1Instance.開くToolStripMenuItem;
        protected static ToolStripMenuItem tssave = Form1.Form1Instance.保存ToolStripMenuItem;
        protected static ToolStripMenuItem tsfoldercheck = Form1.Form1Instance.作業フォルダチェックToolStripMenuItem;
        protected static ToolStripMenuItem tsedit = Form1.Form1Instance.編集ToolStripMenuItem;
        
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
        protected static Button BDL_Button = Form1.Form1Instance.button1;
        protected static Button ARC_Button = Form1.Form1Instance.button2;
        protected static Button Col_Button = Form1.Form1Instance.button3;
        protected static Button Wh_Button = Form1.Form1Instance.button4;
        protected static Button Debug_Button = Form1.Form1Instance.Debug;
        protected static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        protected static CheckBox cb2 = Form1.Form1Instance.checkBox2;
        protected static CheckBox cb3 = Form1.Form1Instance.checkBox3;
        protected static ComboBox com1 = Form1.Form1Instance.comboBox1;
        protected static ComboBox com2 = Form1.Form1Instance.comboBox2;
        protected static ComboBox com3 = Form1.Form1Instance.comboBox3;
        protected static TextBox txt1 = Form1.Form1Instance.textBox1;
        protected static TextBox txt2 = Form1.Form1Instance.textBox2;
    }
    class File_Path_Create_Working_Folder : File_Path_State
    {
        //フォームのロード時に処理する項目
        public  void Processing_Form1_Load()
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
            //com2.Enabled = false;
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
    class File_Path_State2:File_Path_State
    {
        protected static bool[] json_flags = new bool[] { false, false, false };
        protected static int json_flags_counter;
        protected static int json_flags_true_point1;
        protected static int json_flags_true_point2;

        //ファイルパス変数相対
        protected static string btkcomb;
        protected static string brkcomb;
        protected static string rootfolder;

        protected static string fbxname;
        protected static string bdlname;
        protected static string comand1;

        protected static string btk_mat_path;
        protected static string btk_tex_path;
        protected static string btktemp;

        protected static string brk_mat_path;
        protected static string brk_tex_path;
        protected static string brktemp;


        protected static string cmdpath;
        


        protected static string tmp_btk_mat_json;
        protected static string tmp_btk_tex_json;
        protected static string tmp_btk_folder;

        protected static string userjson;
        protected static string root_user_json;


        protected static string mixjson;
        protected static string root_mixjson;

        protected static string user_materiar_json;
        protected static string user_texheader_json;

        protected static string user_json_cmd_command;

        protected static string[] json_type_array;


        
    }

    class File_Path_CMD_Path_And_Comand:File_Path_State2
    {
        private string tmp_brk_mat_json;
        private string tmp_brk_tex_json;
        private string tmp_brk_folder;
        ////mat宣言
        //private string  user_mat_json;
        //private string  temp_btk_mat_json;
        //private string  temp_brk_mat_json;
        //private string  mix_mat_json;
        ////tex宣言
        //private string  user_tex_json;
        //private string  temp_btk_tex_json;
        //private string  temp_brk_tex_json;
        //private string  mix_tex_json;
        //mat配列
        private string[] mat_array;
        //tex配列
        private string[] tex_array;

        private string[] folder_array;

        private string superbmd_cmd_path;

        public void Path_Set() 
        {
            //ファイルパス変数相対
            btkcomb = @"\" + btktype[com1.SelectedIndex];
            brkcomb = @"\" + brktype[com2.SelectedIndex];
            rootfolder = @" ..\";

            fbxname = @" ..\FBX\" + txt1.Text + ".fbx";
            bdlname = @" ..\BDL_BMD\" + txt1.Text + ".bdl";
            comand1 = @" --rotate --bdl";

            btk_mat_path = "BTK" + btkcomb + btkcomb + "_materials.json";
            btk_tex_path = "BTK" + btkcomb + btkcomb + "_tex_headers.json";
            btktemp = @" --mat" + rootfolder + btk_mat_path;
            btktemp += @" --texheader" + rootfolder + btk_tex_path;

            brk_mat_path = "BRK" + brkcomb + brkcomb + "_materials.json";
            brk_tex_path = "BRK" + brkcomb + brkcomb + "_tex_headers.json";
            brktemp = @" --mat" + rootfolder + brk_mat_path;
            brktemp += @" --texheader" + rootfolder + brk_tex_path;

            

            //ファイルパス変数絶対
            mainfilePath = mainfilePath+@"\";
            tmp_btk_mat_json = mainfilePath + btk_mat_path;
            tmp_btk_tex_json = mainfilePath + btk_tex_path;
            tmp_btk_folder   = mainfilePath + @"BTK" + btkcomb;

            tmp_brk_mat_json = mainfilePath + brk_mat_path;
            tmp_brk_tex_json = mainfilePath + brk_tex_path;
            tmp_brk_folder = mainfilePath + @"BRK" + brkcomb;

            userjson = @"User_json\" + txt1.Text;
            root_user_json = rootfolder + userjson;
            userjson = mainfilePath + userjson;

            mixjson = userjson + @"\Mix_json";
            root_mixjson = root_user_json + @"\Mix_json";

            user_materiar_json = @"\" + txt1.Text + @"_materials.json";
            user_texheader_json = @"\" + txt1.Text + @"_tex_headers.json";

            user_json_cmd_command  = @" --mat"       + root_user_json + user_materiar_json ;
            user_json_cmd_command += @" --texheader" + root_user_json + user_texheader_json;

            json_type_array = new string[] { user_json_cmd_command, btktemp, brktemp };

            superbmd_cmd_path = fbxname + bdlname + comand1;

            //mat 初期化
            mat_array = new string[3];
            mat_array[0] = userjson + user_materiar_json;
            mat_array[1] = tmp_btk_mat_json;
            mat_array[2] = tmp_brk_mat_json;
            //tex 初期化
            tex_array = new string[3];
            tex_array[0] = userjson + user_texheader_json;
            tex_array[1] = tmp_btk_tex_json;
            tex_array[2] = tmp_brk_tex_json;
            //folder
            folder_array = new string[3];
            folder_array[0] = userjson;
            folder_array[1] = tmp_btk_folder;
            folder_array[2] = tmp_brk_folder;
        }
        public void SuperBMD_Processing() 
        {
            Path_Set();
            Json_Flags_Check();
            if ((json_flags[0] == true) && User_Json_Checker() != true) return;
            Json_Joint_Type();

            //SuperBMDの実行
            if (efe.File_Executor(0, superbmd_cmd_path) != 0) { return; }
            
            //J3DViewの実行
            if (efe.File_Executor(1, bdlname) != 0) { return; }
        }
        public void Json_Flags_Check() 
        {
            json_flags[0] = cb3.Checked;
            if (btktype[com1.SelectedIndex] != "None") json_flags[1] = true;
            if (btktype[com2.SelectedIndex] != "None") json_flags[2] = true;
        }

        public void Json_Joint_Type() 
        {
            //trueの数をカウント
            json_flags_counter = json_flags.Count(value => value == true);

            //配列内のtrueの場所を取得
            json_flags_true_point1 = Array.IndexOf(json_flags, true);
            json_flags_true_point2 = Array.IndexOf(json_flags, true, json_flags_true_point1+1 );

            //jsonファイルを0個または1個使用する場合
            var json = json_type_array[json_flags_true_point1];
            if (json_flags_counter == 0) return;
            if (json_flags_counter == 1) { superbmd_cmd_path += json; return; }
            
            //Jsonファイルを2つ以上使う場合の処理
            Json_Mixer(json_flags_true_point1, json_flags_true_point2);
            superbmd_cmd_path += (@" --mat" + root_mixjson + user_materiar_json);
            superbmd_cmd_path += (@" --texheader" + root_mixjson + user_texheader_json);
        }

        public void Json_Mixer(int arg1 , int arg2) 
        {
            string mat1, mat2, tex1, tex2;

            //フォルダの作成
            if (mes.sysmes(9) == DialogResult.No)return ;
            fc.Set_User_Json_Folder(userjson, @"User_json\" + txt1.Text, txt1.Text);

            //mat,texファイル読み取り
            mat1 = File.ReadAllText(mat_array[arg1]);
            mat2 = File.ReadAllText(mat_array[arg2]);
            tex1 = File.ReadAllText(tex_array[arg1]);
            tex2 = File.ReadAllText(tex_array[arg2]);

            //フォルダの必要ファイルを複製して移動する
            fe.Directry_Files_Copy(folder_array[arg1], mixjson);
            fe.Directry_Files_Copy(folder_array[arg2], mixjson);

            //jsonファイルの結合
            var Mat = Mix_System_Jsons(mat1, mat2, user_materiar_json);
            var Tex = Mix_System_Jsons(tex1, tex2,user_texheader_json);

            //全てのjsonファイルを使用する
            if (json_flags_counter == 3) 
            {
                //mat,texファイル読み取り
                var mat3 = File.ReadAllText(mat_array[2]);
                var tex3 = File.ReadAllText(tex_array[2]);

                //フォルダの必要ファイルを複製して移動する
                fe.Directry_Files_Copy(folder_array[2], mixjson);

                //jsonファイルの結合
                Mat = Mix_System_Jsons(Mat, mat3, user_materiar_json);
                Tex = Mix_System_Jsons(Tex, tex3, user_texheader_json);
            }
        }

        public string Mix_System_Jsons(string mt1 , string mt2 ,string matortex)
        {
            var mat_tex = "";

            //mat or tex jsonを結合してMix_jsonに移動する
            mat_tex = mt1.Substring(0, (mt1.Length) - 3);
            mat_tex += ("," + mt2.Substring(1, mt2.Length - 1));
            File.WriteAllText(mixjson + matortex, mat_tex);
            return mat_tex;
        }

        public bool User_Json_Checker() 
        {

            //ユーザー定義のmateriar_jsonとtexheader_jsonをユーザーが
            //指定フォルダに作成しているかのチェック
            if (File.Exists(userjson + user_materiar_json) && File.Exists(userjson + user_texheader_json))
            {
                return true;
            }
            else
            {
                //ユーザー定義のmateriar_jsonとtexheader_jsonがない場合の処理
                if (Properties.Settings.Default.LangageType == "日本語")
                {
                    MessageBox.Show(userjson + user_materiar_json + "\n\rまたは\n\r" + userjson + user_texheader_json + "\n\rが見つかりませんでした", "ファイルが見つかりません", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(userjson + user_materiar_json + "\n\ror\n\r" + userjson + user_texheader_json + "\n\rwas not found", "file not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }
        }
    }
    
}
