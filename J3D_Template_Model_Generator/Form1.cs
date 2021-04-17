using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Diagnostics;

using System.Collections.ObjectModel;
using J3D_Template_Model_Generator.FileSys;

using fc = J3D_Template_Model_Generator.FileSys.FolderCreate;
using mes = J3D_Template_Model_Generator.FileSys.Message;
using sff = J3D_Template_Model_Generator.FileSys.Select_File_Folder;
using efe = J3D_Template_Model_Generator.FileSys.External_File_Executor;
using env = System.Environment;


namespace J3D_Template_Model_Generator
{
    


    public partial class Form1 : Form
    {
        public string mainfilePath = "";
        public string[] btktype = new string[] { "None", "Lava_Temp", "Water_Temp", "WaterFall_Temp" };
        public string[] brktype = new string[] { "None", "Flash_Black" };
        public Form1()
        {
            InitializeComponent();
        }



        //インスタンスの作成
        private static Form1 _form1Instance;
        public static Form1 Form1Instance
        {
            get
            {
                return _form1Instance;
            }
            set
            {
                _form1Instance = value;
            }
        }

        

        //フォームがロードされる際に実行される処理
        private void Form1_Load(object sender, EventArgs e)
        {
            //Form1のインスタンスの作成
            Form1 f = new Form1();
            //Form1Instanceに代入
            Form1.Form1Instance = f;
            //Form1の表示
            //f.Show();

            Form1.Form1Instance = this;
            comboBox2.Enabled = false;
            checkBox1.Checked = true;
            label2.Text = Properties.Settings.Default.設定;
            mainfilePath =Properties.Settings.Default.設定 + "J3D_Template_Model_Generator";


            switch (Properties.Settings.Default.LangageType)
            {
                case "JP":
                    Language.JP();
                    comboBox3.SelectedIndex = 0;
                    Properties.Settings.Default.LangageType = "JP";
                    break;
                case "EN":
                    Language.EN();
                    comboBox3.SelectedIndex = 1;
                    Properties.Settings.Default.LangageType = "EN";
                    break;
                default:
                    break;
            }

            

            //作業フォルダチェック＆作成処理
            if (Directory.Exists(mainfilePath)==false){
                //フォルダ作成許可下記if文で分岐
                DialogResult result = mes.sysmes();
                //何が選択されたか調べる
                if (result == DialogResult.Yes){
                    //ユーザーにフォルダ作成位置を決めるか選ばせる
                    DialogResult user_path = mes.sysmes(2);
                    if (user_path == DialogResult.Yes){
                        //Settingsの設定を書き換え
                        Properties.Settings.Default.設定 = sff.Folder_Select();
                        mainfilePath = Properties.Settings.Default.設定;
                        label2.Text = Properties.Settings.Default.設定;
                        //Settingsの設定を保存
                        Properties.Settings.Default.Save();
                        
                    }

                    //指定したディレクトリにフォルダがないかの最終確認
                    if (Directory.Exists(mainfilePath + "J3D_Template_Model_Generator") == false){
                        
                    }
                    else {
                        mes.sysmes(4);
                    }

                }
                else if (result == DialogResult.No){
                    this.Close();
                }

                //フォルダの作成&チェック
                fc.Set_Folder();
                mes.sysmes(1);

            }


            
        }

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            


            //ファイルパス変数相対
            string btkcomb = @"\" + btktype[comboBox1.SelectedIndex];
            string brkcomb = @"\" + brktype[comboBox2.SelectedIndex];
            string rootfolder = @" ..\";

            string fbxname = @" ..\FBX\" + textBox1.Text + ".fbx";
            string bdlname = @" ..\BDL_BMD\" + textBox1.Text + ".bdl";
            string comand1 = @" --rotate --bdl";

            string btk_mat_path = "BTK" + btkcomb + btkcomb + "_materials.json";
            string btk_tex_path = "BTK" + btkcomb + btkcomb + "_tex_headers.json";
            string btktemp = @" --mat" + rootfolder + btk_mat_path;
            btktemp += @" --texheader" + rootfolder + btk_tex_path;

            string brk_mat_path = "BRK" + brkcomb + brkcomb + "_materials.json";
            string brk_tex_path = "BRK" + brkcomb + brkcomb + "_tex_headers.json";
            string brktemp = @" --mat" + rootfolder + brk_mat_path;
            brktemp += @" --texheader" + rootfolder + brk_tex_path;

            //btktemp += btktype[comboBox1.SelectedIndex] + "\\" + btktype[comboBox1.SelectedIndex] + "_materials.json --texheader";
            //btktemp += rootfolder + "BTK" + "\\" + btktype[comboBox1.SelectedIndex] + "\\" + btktype[comboBox1.SelectedIndex] + "_tex_headers.json";
            string cmdpath;

            //ファイルパス変数絶対
            mainfilePath = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string tmp_btk_mat_json = mainfilePath + btk_mat_path;
            string tmp_btk_tex_json = mainfilePath + btk_tex_path;
            string tmp_btk_folder = mainfilePath + @"BTK" + btkcomb;

            string userjson = @"User_json\" + textBox1.Text;
            string root_user_json = rootfolder + userjson;
            userjson = mainfilePath + userjson;

            string mixjson =  userjson + @"\Mix_json";
            string root_mixjson = root_user_json + @"\Mix_json";

            string user_materiar_json = @"\" + textBox1.Text + @"_materials.json";
            string user_texheader_json = @"\" + textBox1.Text + @"_tex_headers.json";

            //ユーザー定義のjsonファイルを使用するかのチェック
            if (checkBox3.Checked == true){
                //ユーザー定義のmateriar_jsonとtexheader_jsonをユーザーが
                //指定フォルダに作成しているかのチェック
                if (File.Exists(userjson + user_materiar_json) && File.Exists(userjson + user_texheader_json))
                {
                    //ユーザー定義のmateriar_jsonとtexheader_jsonがある場合の処理

                    //mat宣言
                    string u_mat_json;
                    string t_mat_json;
                    string m_mat_json;
                    //tex宣言
                    string u_tex_json;
                    string t_tex_json;
                    string m_tex_json;

                    //BTKテンプレートを使うかの判別
                    if ((btktype[comboBox1.SelectedIndex] != "None")){
                        //BTKテンプレートを使う場合の処理

                        //mat 初期化
                        u_mat_json = File.ReadAllText(userjson + user_materiar_json);
                        t_mat_json = File.ReadAllText(tmp_btk_mat_json);
                        //tex 初期化
                        u_tex_json = File.ReadAllText(userjson + user_texheader_json);
                        t_tex_json = File.ReadAllText(tmp_btk_tex_json);

                        //コピー元のディレクトリにあるファイルをコピー
                        string[] files = Directory.GetFiles(tmp_btk_folder);
                        foreach (string file in files)
                        {
                            if (Path.GetExtension(file)!=".json") {
                                File.Copy(file, mixjson +@"\"+ Path.GetFileName(file), true);
                            }
                        }

                        //コピー元のディレクトリにあるファイルをコピー
                        string[] ufiles = Directory.GetFiles(userjson);
                        foreach (string ufile in ufiles)
                        {
                            if (Path.GetExtension(ufile) != ".json")
                            {
                                File.Copy(ufile, mixjson + @"\" + Path.GetFileName(ufile), true);
                            }
                        }

                        //mat結合とファイルをMix_jsonに移動する
                        m_mat_json = u_mat_json.Substring(0, (u_mat_json.Length) - 3);
                        m_mat_json += ("," + t_mat_json.Substring(1, t_mat_json.Length - 1));
                        File.WriteAllText(mixjson + user_materiar_json, m_mat_json);
                        //tex結合
                        m_tex_json = u_tex_json.Substring(0, (u_tex_json.Length) - 3);
                        m_tex_json += ("," + t_tex_json.Substring(1, t_tex_json.Length - 1));
                        File.WriteAllText(mixjson + user_texheader_json, m_tex_json);
                        //SuperBMDのコマンドを指定
                        cmdpath = fbxname + bdlname + comand1;
                        cmdpath += (@" --mat" + root_mixjson +user_materiar_json);
                        cmdpath += (@" --texheader" + root_mixjson + user_texheader_json);
                    }
                    else {
                        //BTKテンプレートを使わない場合の処理

                        //SuperBMDのコマンドをユーザー定義のjsonファイルに指定
                        cmdpath = fbxname + bdlname + comand1;
                        cmdpath += (@" --mat" + root_user_json +  user_materiar_json);
                        cmdpath += (@" --texheader" + root_user_json + user_texheader_json);
                    }
                }
                else
                {
                    //ユーザー定義のmateriar_jsonとtexheader_jsonがない場合の処理
                    if (Properties.Settings.Default.LangageType == "JP")
                    {
                        MessageBox.Show(userjson + user_materiar_json + "\n\rまたは\n\r" + userjson + user_texheader_json + "\n\rが見つかりませんでした", "ファイルが見つかりません", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        MessageBox.Show(userjson + user_materiar_json + "\n\ror\n\r" + userjson + user_texheader_json + "\n\rwas not found", "file not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
            }
            else {
                //ユーザー定義のjsonファイルを使用しない場合

                //SuperBMDのコマンド生成BTKを使うか使わないか
                if ((btkcomb != "None"))
                {

                    cmdpath = fbxname + bdlname + comand1 + btktemp;
                    
                }
                else
                {
                    cmdpath = fbxname + bdlname + comand1;
                }

            }

            //SuperBMDの実行エラー時は中断
            if (efe.File_Executor(0,cmdpath ) != 0) { return; }

            //エラーのない場合下記を実行
            if (btktype[comboBox1.SelectedIndex] != "None"){
                mes.sysmes(4);
            }
            else {
                mes.sysmes(5);
            }

            //J3D_Viewの実行エラー時は中断
            if (efe.File_Executor(1, bdlname) != 0) { return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="") { 
                textBox1.Text = "Test"; 
            }
            string combotex = btktype[comboBox1.SelectedIndex];
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string bdl_file = User_Root + @"BDL_BMD\" + textBox1.Text +@".bdl";
            string BTK_file = User_Root + @"BTK\" + combotex+ @"\"+ combotex + @".btk";
            string arcfolder =  @"ARC\" + @"" + textBox1.Text;
            if (Directory.Exists(arcfolder)==false){
                fc.Set_New_Folder(arcfolder);
            }


            if (File.Exists(bdl_file) == true){
                File.Copy(bdl_file,User_Root + arcfolder+ "\\" + textBox1.Text +@".bdl",true);
                
                Console.WriteLine(BTK_file + "★");
                if (combotex != "None")
                {
                    Console.WriteLine(BTK_file + "★");

                    if (File.Exists(BTK_file) == true)
                    {
                        Console.WriteLine(BTK_file);
                        File.Copy(BTK_file, User_Root + arcfolder + @"\" + textBox1.Text + @".btk", true);
                    }


                }
                //RARCの実行エラー時は中断
                if (efe.File_Executor(2, User_Root+arcfolder) != 0) { return; }
                
                //Yaz0エンコードのチェックボックスチェック
                    if (checkBox1.Checked==true) {

                        //yaz0encの実行エラー時は中断
                        if (efe.File_Executor(3, User_Root + @"ARC\" + textBox1.Text + @".arc") != 0) { return; }

                        //不要ファイルの削除やフォルダへの移動
                        File.Delete(User_Root + @"ARC\" + textBox1.Text + @".arc");
                        File.Move(User_Root + @"ARC\" + textBox1.Text + @".arc.yaz0", User_Root + @"ARC\" + textBox1.Text + @".arc");
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            if (File.Exists(User_Root + @"OBJ\" + textBox1.Text + @".obj") == true)
            {
                //コリジョンツールの実行エラー時は中断
                if (efe.File_Executor(4, User_Root + @"OBJ\" + textBox1.Text + @".obj") != 0) { return; }

            }
            else
            {
                mes.sysmes(6);
            }


            if (Directory.Exists(User_Root + @"ARC\" + textBox1.Text) ==false) {
                    fc.Set_New_Folder(@"ARC\" + textBox1.Text);
                }

                if (File.Exists(User_Root + @"OBJ\" + textBox1.Text + @".kcl") == true)
                {
                    File.Copy(User_Root + @"OBJ\" + textBox1.Text + @".kcl", User_Root + @"ARC\" + textBox1.Text + @"\" + textBox1.Text + @".kcl", true);
                    File.Copy(User_Root + @"OBJ\" + textBox1.Text + @".kcl", User_Root + @"Collision\" + textBox1.Text + @".kcl", true);
                    File.Delete(User_Root + @"OBJ\" + textBox1.Text + @".kcl");
                }
                else 
                {
                mes.sysmes(7);
                }

                if (File.Exists(User_Root + @"OBJ\" + textBox1.Text + @".pa") == true) {
                    File.Copy(User_Root + @"OBJ\" + textBox1.Text + @".pa", User_Root + @"ARC\" + textBox1.Text + @"\" + textBox1.Text + @".pa", true);
                    File.Copy(User_Root + @"OBJ\" + textBox1.Text + @".pa", User_Root + @"Collision\" + textBox1.Text + @".pa", true);
                    File.Delete(User_Root + @"OBJ\" + textBox1.Text + @".pa");
                }
                else
                {
                mes.sysmes(8);
                }
            
        }

        

        private void 作業フォルダチェックToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fc.Set_Folder();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NoTmp;
            if (Properties.Settings.Default.LangageType=="JP") { NoTmp = "なし"; } else { NoTmp = "None"; }
             

            string LavaTmp = "Lava00_v";
            string WaterTmp = "a_WaterBFMat" + env.NewLine + "b_WaterMat";
            string WaterFallTmp = "FallMat_v" + env.NewLine + "e_FallMat_v_x" + env.NewLine + "d_FallAlfaMat_v_x";

            string[] Materials = {NoTmp, LavaTmp, WaterTmp , WaterFallTmp };
            textBox2.Text = Materials[comboBox1.SelectedIndex];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            toolStripStatusLabel2.Text = "Whiteholeを開いています";
            this.Enabled = false;
            if (File.Exists(User_Root + @"Whitehole\Whitehole.jar") == true)
            {
                ProcessStartInfo white =new ProcessStartInfo();
                
                white.WorkingDirectory = User_Root + @"Whitehole";
                white.Arguments = "-jar " + User_Root + @"Whitehole\Whitehole.jar";
                
                if (checkBox2.Checked == false)
                {
                    //CMDを起動しない
                    white.FileName = "javaw";
                    white.UseShellExecute = false;
                    Process w = Process.Start(white);
                    w.WaitForExit();
                    if (w.ExitCode != 0) { return; }
                    


                }
                else {
                    //CMDも起動する
                    white.FileName = "java";
                    white.UseShellExecute = true;

                    Process w = Process.Start(white);
                    toolStripStatusLabel2.Text = "";
                    this.Enabled = true;
                    w.WaitForExit();
                    
                    if (w.ExitCode != 0) { return; }

                    //Process p = Process.Start("java", "-jar " + User_Root + @"Whitehole\Whitehole.jar");
                    //p.WaitForExit();
                    //if (p.ExitCode != 0) { return; }

                }
                
                toolStripStatusLabel2.Text = "";
                this.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            mainfilePath = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string userjson =  @"User_json\" + textBox1.Text;
            if (Directory.Exists(mainfilePath + userjson)==false) {
                if (Properties.Settings.Default.LangageType == "JP")
                {
                    if (MessageBox.Show(mainfilePath + userjson + "\n\rフォルダと" + userjson + @"\Mix_json" + "\n\rフォルダを作成しますか？", "フォルダを作成します", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        fc.Set_New_Folder(userjson);
                        fc.Set_New_Folder(userjson + @"\Mix_json");
                        MessageBox.Show(mainfilePath + userjson + "\n\rフォルダを作成しました\n\r" + textBox1.Text + ".jsonファイル(mat,tex_headerおよび画像ファイル)を\n\rフォルダに入れてください", "案内", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else 
                {
                    if (MessageBox.Show("Do you want to create\r\n" + mainfilePath + userjson + "\n\rfolders and" + userjson + @"\Mix_json" + "\n\rfolders? ", "Confirm folder creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        fc.Set_New_Folder(userjson);
                        fc.Set_New_Folder(userjson + @"\Mix_json");
                        MessageBox.Show("Created\r\n"+mainfilePath + userjson + " folder!!\n\rPut The\n\r" + textBox1.Text + ".json Files(mat,tex_header and need Textures)\n\rin a folder", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "JP":
                Language.JP();
                    Properties.Settings.Default.LangageType = "JP";
                    Properties.Settings.Default.Save();
                    break;
                case "EN":
                    Language.EN();
                    Properties.Settings.Default.LangageType = "EN";
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                
                    //ユーザーにフォルダ作成位置を決めるか選ばせる
                    DialogResult user_path = mes.sysmes(2);
                    if (user_path == DialogResult.Yes)
                    {
                        //Settingsの設定を書き換え
                        Properties.Settings.Default.設定 = sff.Folder_Select();
                        mainfilePath = Properties.Settings.Default.設定;
                        label2.Text = Properties.Settings.Default.設定;
                        //Settingsの設定を保存
                        Properties.Settings.Default.Save();

                    }

                    //指定したディレクトリにフォルダがないかの最終確認
                    if (Directory.Exists(mainfilePath + "J3D_Template_Model_Generator") == false)
                    {

                    }
                    else
                    {
                        mes.sysmes(3);
                    }

                

                //フォルダの作成&チェック
                fc.Set_Folder();
                mes.sysmes(1);

            }
        
    }
}
