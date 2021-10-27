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
using env = System.Environment;

namespace J3D_Template_Model_Generator.FileSys
{
    public class TempName
    {
        public static readonly string[] btktype = { "None", "Lava_Temp", "Water_Temp", "WaterFall_Temp", "Quicksand_Temp", "Slipsand_Temp", "Poison_Temp", "Mud_Temp", "GliderStarWater_Temp" };
        public static readonly string[] brktype = { "None", "Flash_Black_Temp" };
        public static readonly string[] brkName = { "None" , "Appear" };
    }

    public interface IMatType 
    {
        string[] GetMats();
    }

    public class MatName
    {
        private readonly IMatType _matType;
        public MatName(IMatType imatType) 
        {
            _matType = imatType;
        }

        /// <summary>
        /// BTKファイルの必要マテリアルを表示します。
        /// </summary>
        /// <returns>BTKの必要マテリアルのstring配列</returns>
        public string[] GetNeedMats()
        {
            return _matType.GetMats();
        }
    }

    public class BTK : IMatType 
    {
        public string[] GetMats() 
        {
            string NoTmp = "";
            //if (Properties.Settings.Default.LangageType == "日本語")
            //{
            //    NoTmp = "なし";
            //}

            string LavaTmp = "Lava00_v";
            string WaterTmp = "a_WaterBFMat" + env.NewLine + "b_WaterMat";
            string WaterFallTmp = "FallMat_v" + env.NewLine + "e_FallMat_v_x" + env.NewLine + "d_FallAlfaMat_v_x";
            string Quicksand = "Sand00_v";
            string Slipsand = "SandRiver_v";
            string Poison = "Dark01_v";
            string Mud = "lambert16_v";
            string GliderStarWaterTemp = "WaterMat00_v_x";

            var Mats = new string[]
            {
                NoTmp,
                LavaTmp,
                WaterTmp,
                WaterFallTmp,
                Quicksand,
                Slipsand,
                Poison,
                Mud,
                GliderStarWaterTemp
            };
            return Mats;
        }
    }

    public class BRK : IMatType
    {
        public string[] GetMats()
        {
            string NoTmp = "";
            //if (Properties.Settings.Default.LangageType == "日本語")
            //{
            //    NoTmp = "なし";
            //}

            string LavaTmp = "brktest_v";
            
            var Mats = new string[]
            {
                NoTmp,
                LavaTmp
            };
            return Mats;
        }
    }

    class File_Path_State : TempName
    {
        //宣言
        protected static string mainfilePath;
        //protected static string[] btktype = new string[] { "None", "Lava_Temp", "Water_Temp", "WaterFall_Temp", "Quicksand_Temp", "Slipsand_Temp", "Poison_Temp", "Mud_Temp"};
        //protected static string[] brktype = new string[] { "None", "Flash_Black_Temp", "Test"};
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
        protected static Label lb5 = Form1.Form1Instance.label5;
        protected static Label CMD_Error = Form1.Form1Instance.label7;
        protected static Label need_mat = Form1.Form1Instance.label6;
        protected static Button BDL_Button = Form1.Form1Instance.GenerateBdl_Button;
        protected static Button ARC_Button = Form1.Form1Instance.ConvertArc_Button;
        protected static Button Col_Button = Form1.Form1Instance.button3;
        protected static Button Wh_Button = Form1.Form1Instance.button4;
        protected static Button Debug_Button = Form1.Form1Instance.Debug;
        protected static CheckBox cb1 = Form1.Form1Instance.checkBox1;
        protected static CheckBox cb2 = Form1.Form1Instance.checkBox2;
        protected static CheckBox cb3 = Form1.Form1Instance.checkBox3;
        protected static ComboBox com1 = Form1.Form1Instance.comboBox1;
        protected static ComboBox com2 = Form1.Form1Instance.comboBox2;
        protected static TextBox txt1 = Form1.Form1Instance.FbxAndObj_ModelNameTextBox;
        protected static TextBox txt2 = Form1.Form1Instance.NeedMaterial;

        
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
        protected static string btkcomb_path;
        protected static string brkcomb;
        protected static string brkcomb_path;
        protected static string rootfolder;
        protected static string fbxname;
        protected static string bdlname;
        protected static string comand1;
        protected static string comand2;
        protected static string comand_mat;
        protected static string comand_tex;
        protected static string userjson;
        protected static string root_user_json;
        protected static string mixjson;
        protected static string root_mixjson;
        protected static string user_materiar_json;
        protected static string user_texheader_json;
        protected static string user_json_cmd_command;
    }

    class File_Path_CMD_Path_And_Comand : File_Path_State2
    {
        private string[] folder_relative_array;
        private string superbmd_cmd_path;
        private string absolute_folder;
        private string[] mat_read , tex_read;
        public void Path_Set()
        {
            btkcomb = btktype[com1.SelectedIndex];
            brkcomb = brktype[com2.SelectedIndex];

            //ファイルパス変数相対
            btkcomb_path = @"\" + btkcomb;
            brkcomb_path = @"\" + brkcomb;

            rootfolder = @" ..\";

            fbxname = @" ..\FBX\" + txt1.Text + ".fbx";
            bdlname = @" ..\BDL_BMD\" + txt1.Text + ".bdl";
            comand1 = @" --rotate --bdl";

            string user_directory = @"User_json\" + txt1.Text;
            string btk_directory  = @"BTK" + btkcomb_path + btkcomb_path;
            string brk_directory  = @"BRK" + brkcomb_path + brkcomb_path;
            string mix_directory  = user_directory + @"\Mix_json\" + txt1.Text;

            //ファイルパス変数絶対
            absolute_folder = mainfilePath + @"\";

            userjson = @"User_json\" + txt1.Text;
            root_user_json = rootfolder + userjson;
            userjson = absolute_folder + userjson;

            mixjson = userjson + @"\Mix_json";
            root_mixjson = root_user_json + @"\Mix_json";

            user_materiar_json = @"\" + txt1.Text + @"_materials.json";
            user_texheader_json = @"\" + txt1.Text + @"_tex_headers.json";

            user_json_cmd_command = @" --mat" + root_user_json + user_materiar_json;
            user_json_cmd_command += @" --texheader" + root_user_json + user_texheader_json;

            

            //SuperBMDのjsonを使わないコマンド(追記でjsonを使える)
            superbmd_cmd_path = fbxname + bdlname + comand1;

            //folder Relative Path
            folder_relative_array = new string[4];
            folder_relative_array[0] = user_directory;
            folder_relative_array[1] = btk_directory;
            folder_relative_array[2] = brk_directory;
            folder_relative_array[3] = mix_directory;

            mat_read = new string[3];
            tex_read = new string[3];
        }

        public void Mat_Tex_Command_Generator(string path_type , int arg) 
        {
            comand_mat =  path_type + folder_relative_array[arg] + @"_materials.json";
            comand_tex =  path_type + folder_relative_array[arg] + @"_tex_headers.json";
            comand2 = @" --mat" + comand_mat + @" --texheader" + comand_tex;
            Console.WriteLine(comand2);
        }

        public void SuperBMD_Processing() 
        {
            Path_Set();
            Json_Flags_Check();
            if ((json_flags[0] == true) && User_Json_Checker() != true) return;
            Json_Joint_Type();

            //SuperBMDの実行
            if (efe.File_Executor(0, superbmd_cmd_path) != 0) { return; }

            //
            if (btkcomb != "None" || brkcomb != "None")
            {
                mes.sysmes(4);
            }
            else
            {
                mes.sysmes(5);
            }

            //J3DViewの実行
            if (efe.File_Executor(1, bdlname) != 0) { return; }
        }
        public void Json_Flags_Check() 
        {
            json_flags[0] = cb3.Checked;
            if (btktype[com1.SelectedIndex] != "None") json_flags[1] = true;
            if (brktype[com2.SelectedIndex] != "None") json_flags[2] = true;

        }

        public void Json_Joint_Type() 
        {
            //trueの数をカウント
            json_flags_counter = json_flags.Count(value => value == true);

            //配列内のtrueの場所を取得
            json_flags_true_point1 = Array.IndexOf(json_flags, true);
            json_flags_true_point2 = Array.IndexOf(json_flags, true, json_flags_true_point1+1 );

            //jsonファイルを使用しない場合
            if (json_flags_counter == 0) return;

            //jsonファイルを一つ使う
            Mat_Tex_Command_Generator(rootfolder, json_flags_true_point1);
            if (json_flags_counter == 1) { superbmd_cmd_path += comand2; return; }
            
            //Jsonファイルを2つ以上使う場合の処理
            Json_Mixer(json_flags_true_point1, json_flags_true_point2);
            Mat_Tex_Command_Generator(rootfolder,3);
            superbmd_cmd_path += comand2;
        }

        public void Json_Mixer(int arg1 , int arg2) 
        {
            //フォルダの作成
            if (mes.sysmes(9) == DialogResult.No)return ;
            fc.Set_User_Json_Folder(userjson, @"User_json\" + txt1.Text, txt1.Text);

            //jsonファイル読み取り＆付属データ移動
            Json_Mixer_Read_sys(arg1);
            Json_Mixer_Read_sys(arg2);

            //jsonファイルの結合
            var Mat = Mix_System_Jsons(mat_read[arg1], mat_read[arg2], user_materiar_json);
            var Tex = Mix_System_Jsons(tex_read[arg1], tex_read[arg2], user_texheader_json);

            //全てのjsonファイルを使用する
            if (json_flags_counter == 3) 
            {
                //jsonファイル読み取り＆付属データ移動
                Json_Mixer_Read_sys(2);

                //jsonファイルの結合
                Mat = Mix_System_Jsons(Mat, mat_read[2], user_materiar_json);
                Tex = Mix_System_Jsons(Tex, tex_read[2], user_texheader_json);
            }
        }

        public void Json_Mixer_Read_sys(int arg1) 
        {
            Mat_Tex_Command_Generator(absolute_folder, arg1);
            mat_read[arg1] = File.ReadAllText(comand_mat);
            tex_read[arg1] = File.ReadAllText(comand_tex);
            var Target_Folder = Path.GetDirectoryName(comand_mat);
            fe.Directry_Files_Copy(Target_Folder, mixjson);
        }

        public string Mix_System_Jsons(string mt1 , string mt2 ,string matortex)
        {
            var mat_tex = "";

            //mat or tex jsonを結合してMix_jsonに移動する
            mat_tex = mt1.Substring(0, (mt1.Length) - 3);
            mat_tex += ("," + mt2.Substring(1, mt2.Length - 1));

            //結合内容をmix jsonに書き込む
            File.WriteAllText(mixjson + matortex, mat_tex);
            return mat_tex;
        }

        public bool User_Json_Checker() 
        {
            var mat = userjson + user_materiar_json;
            var tex = userjson + user_texheader_json;

            //ユーザー定義のmateriar_jsonとtexheader_jsonがある場合の処理
            if (File.Exists(mat) && File.Exists(tex)){return true;}

            //ユーザー定義のmateriar_jsonとtexheader_jsonがない場合の処理
            mes.sysmes(10,mat,tex);
            return false;
            
        }
    }
    
}
