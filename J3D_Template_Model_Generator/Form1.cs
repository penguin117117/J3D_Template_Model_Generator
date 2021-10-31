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
        public Form1()
        {
            InitializeComponent();
        }

        private static Form1 _form1Instance;
        public static Form1 Form1Instance
        {
            get => _form1Instance;
            set => _form1Instance = value;
        }

        //フォームがロードされる際に実行される処理
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Form1 f = new Form1();
            Form1Instance = f;
            Form1Instance = this;
            Language.Form1_Translater();


            //ファイルパスクラスのインスタンス作成
            File_Path_Create_Working_Folder FPCW = new File_Path_Create_Working_Folder();
            FPCW.Processing_Form1_Load();

            Debug.Visible = false;
        }

        private void GenerateBdl_Button_Click(object sender, EventArgs e)
        {
            File_Path_CMD_Path_And_Comand FPCPAC = new File_Path_CMD_Path_And_Comand();
            FPCPAC.SuperBMD_Processing();
        }

        private void ConvertArc_Button_Click(object sender, EventArgs e)
        {
            if (FbxAndObj_ModelNameTextBox.Text == "")
            {
                FbxAndObj_ModelNameTextBox.Text = "Test";
            }
            string combotex = TempName.btktype[comboBox1.SelectedIndex];
            string BrkTempFolderName = TempName.brktype[comboBox2.SelectedIndex];
            string BrkFileName = TempName.brkName[comboBox2.SelectedIndex];
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string bdl_file = User_Root + @"BDL_BMD\" + FbxAndObj_ModelNameTextBox.Text + @".bdl";
            string BTK_file = User_Root + @"BTK\" + combotex + @"\" + combotex + @".btk";
            string BRK_file = User_Root + @"BRK\" + BrkTempFolderName + @"\" + BrkFileName + @".brk";
            string arcfolder = @"ARC\" + @"" + FbxAndObj_ModelNameTextBox.Text;
            if (Directory.Exists(arcfolder) == false)
            {
                fc.Set_New_Folder(arcfolder);
            }


            if (File.Exists(bdl_file) == true)
            {
                File.Copy(bdl_file, User_Root + arcfolder + "\\" + FbxAndObj_ModelNameTextBox.Text + @".bdl", true);

                Console.WriteLine(BTK_file + "★");
                if (combotex != "None")
                {
                    Console.WriteLine(BTK_file + "★");

                    if (File.Exists(BTK_file) == true)
                    {
                        Console.WriteLine(BTK_file);
                        File.Copy(BTK_file, User_Root + arcfolder + @"\" + FbxAndObj_ModelNameTextBox.Text + @".btk", true);
                    }


                }
                if (BrkTempFolderName != "None")
                {
                    Console.WriteLine(BRK_file + "★");

                    if (File.Exists(BRK_file) == true)
                    {
                        Console.WriteLine(BRK_file);
                        File.Copy(BRK_file, User_Root + arcfolder + @"\" + BrkFileName + @".brk", true);
                    }


                }
                //RARCの実行エラー時は中断
                if (efe.File_Executor(2, User_Root + arcfolder) != 0) { return; }

                //Yaz0エンコードのチェックボックスチェック
                if (checkBox1.Checked == true)
                {

                    //yaz0encの実行エラー時は中断
                    if (efe.File_Executor(3, User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @".arc") != 0) { return; }

                    //不要ファイルの削除やフォルダへの移動
                    File.Delete(User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @".arc");
                    File.Move(User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @".arc.yaz0", User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @".arc");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            if (File.Exists(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".obj") == true)
            {
                //コリジョンツールの実行エラー時は中断
                if (efe.File_Executor(4, User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".obj") != 0) { return; }

            }
            else
            {
                mes.sysmes(6);
            }


            if (Directory.Exists(User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text) == false)
            {
                fc.Set_New_Folder(@"ARC\" + FbxAndObj_ModelNameTextBox.Text);
            }

            if (File.Exists(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".kcl") == true)
            {
                File.Copy(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".kcl", User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @"\" + FbxAndObj_ModelNameTextBox.Text + @".kcl", true);
                File.Copy(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".kcl", User_Root + @"Collision\" + FbxAndObj_ModelNameTextBox.Text + @".kcl", true);
                File.Delete(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".kcl");
            }
            else
            {
                mes.sysmes(7);
            }

            if (File.Exists(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".pa") == true)
            {
                File.Copy(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".pa", User_Root + @"ARC\" + FbxAndObj_ModelNameTextBox.Text + @"\" + FbxAndObj_ModelNameTextBox.Text + @".pa", true);
                File.Copy(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".pa", User_Root + @"Collision\" + FbxAndObj_ModelNameTextBox.Text + @".pa", true);
                File.Delete(User_Root + @"OBJ\" + FbxAndObj_ModelNameTextBox.Text + @".pa");
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

        private void BtkOrBrkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var BtkNeedMats = new MatName(new BTK()).GetNeedMats();
            NeedMaterial.Text = BtkNeedMats[comboBox1.SelectedIndex];
            if(comboBox2.SelectedIndex < 0) comboBox2.SelectedIndex = 0;
            var BrkNeedMats = new MatName(new BRK()).GetNeedMats();
            NeedMaterial.AppendText(env.NewLine + BrkNeedMats[comboBox2.SelectedIndex]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            toolStripStatusLabel2.Text = "Whiteholeを開いています";
            this.Enabled = false;
            if (File.Exists(User_Root + @"Whitehole\Whitehole.jar") == true)
            {
                ProcessStartInfo white = new ProcessStartInfo
                {
                    WorkingDirectory = User_Root + @"Whitehole",
                    Arguments = "-jar " + User_Root + @"Whitehole\Whitehole.jar"
                };

                if (checkBox2.Checked == false)
                {
                    //CMDを起動しない
                    white.FileName = "javaw";
                    white.UseShellExecute = false;
                    Process w = Process.Start(white);
                    w.WaitForExit();
                    if (w.ExitCode != 0) { return; }



                }
                else
                {
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
            string userjson = @"User_json\" + FbxAndObj_ModelNameTextBox.Text;
            if (Directory.Exists(mainfilePath + userjson) == false)
            {
                if (Properties.Settings.Default.LangageType == "日本語")
                {
                    if (MessageBox.Show(mainfilePath + userjson + "\n\rフォルダと" + userjson + @"\Mix_json" + "\n\rフォルダを作成しますか？", "フォルダを作成します", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        fc.Set_New_Folder(userjson);
                        fc.Set_New_Folder(userjson + @"\Mix_json");
                        MessageBox.Show(mainfilePath + userjson + "\n\rフォルダを作成しました\n\r" + FbxAndObj_ModelNameTextBox.Text + ".jsonファイル(mat,tex_headerおよび画像ファイル)を\n\rフォルダに入れてください", "案内", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to create\r\n" + mainfilePath + userjson + "\n\rfolders and" + userjson + @"\Mix_json" + "\n\rfolders? ", "Confirm folder creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        fc.Set_New_Folder(userjson);
                        fc.Set_New_Folder(userjson + @"\Mix_json");
                        MessageBox.Show("Created\r\n" + mainfilePath + userjson + " folder!!\n\rPut The\n\r" + FbxAndObj_ModelNameTextBox.Text + ".json Files(mat,tex_header and need Textures)\n\rin a folder", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            //Language.Form1_Translater(true);
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイルパスクラスのインスタンス作成
            File_Path_Create_Working_Folder FPCW = new File_Path_Create_Working_Folder();
            FPCW.Form1_Check_Create_Folder(false);
        }

        private void Debug_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //sus
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void 情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var InfoForm = new InfoForm();
            InfoForm.Show();
        }
    }
}
