using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Drawing;
using System.IO;
using env = System.Environment;


namespace J3D_Template_Model_Generator.FileSys
{
    public class External_File_Executor
    {
        //Form1のtextbox3のModifireをpublicにしてForm1インスタンスを作成しないと使えない
        public static System.Windows.Forms.TextBox txt3 = Form1.Form1Instance.textBox3;
        public static System.Windows.Forms.ToolStripStatusLabel tssl2 = Form1.Form1Instance.toolStripStatusLabel2;
        public static string[] Tool_Names =   { "SuperBMD.exe", "J3DView.exe", @"res\arcPack.exe", @"res\yaz0enc.exe", "collision_creator.exe", "javaw" };
        public static string[] Tool_F_Names = { "SuperBMD"    , "J3D_View"   , "ARC_Tool"        , "ARC_Tool"        , "Collision_Tool"       , "Whitehole" };




        public static int  File_Executor(short exenum ,string arg) {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string ErrorCMD;
            string OutputCMD;
            string lowname;
            
            string CDpath = Path.Combine(User_Root,Tool_F_Names[exenum]);
            string exepath = Path.Combine(CDpath,Tool_Names[exenum]);
            tssl2.Text = "";
            Form1.Form1Instance.Height = 384;

            if (exenum != 5)
            {

                if (File.Exists(exepath) == false)
                {
                    lowname = Path.GetFileName(exepath);
                    exepath = CDpath + @"\" + lowname;
                    if (File.Exists(exepath) == false)
                    {


                        txt3.Text = "実行ファイル名が間違っています" + env.NewLine + Tool_Names[exenum] + "←の名前である必要があります" + env.NewLine + ".batファイルは未対応です";
                        return 1;
                    }
                }
            }
            else {
                exepath = Tool_Names[exenum];


            }

            //ステータスバーの設定とフォームの非アクティブ化
            tssl2.Text = Tool_Names[exenum]+"を開いています";
            Form1.Form1Instance.Enabled = false;


            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = exepath;
            psi.WorkingDirectory = CDpath;
            psi.Arguments = arg;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            ErrorCMD = p.StandardError.ReadToEnd();
            OutputCMD = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            
            if (p.ExitCode != 0)
            {
                //ステータスバー
                tssl2.Text = "";
                Form1.Form1Instance.Enabled = true;
                tssl2.Text = Tool_Names[exenum] + "でエラーが発生しました";
                tssl2.ForeColor = Color.Red;
                txt3.Text = "下記アウトプット" + env.NewLine + OutputCMD;
                txt3.AppendText(env.NewLine + "下記エラーログ" + env.NewLine + ErrorCMD);
                //textBox2.Text = InputCMD;
                Form1.Form1Instance.Height = 590;
                
            }



            //ステータスバー
            tssl2.Text = "";
            Form1.Form1Instance.Enabled = true;
            return p.ExitCode;
        }
    }
}
