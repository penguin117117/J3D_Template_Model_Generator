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
        public static string[] Tool_Names =   { "SuperBMD.exe", "j3dview.exe", @"res\arcPack.exe", @"res\yaz0enc.exe", "collision_creator.exe", "javaw" };
        public static string[] Tool_F_Names = { "SuperBMD"    , "J3D_View"   , "ARC_Tool"        , "ARC_Tool"        , "Collision_Tool"       , "Whitehole" };




        /// <summary>
        /// 外部ファイル実行クラス
        /// <remarks>File_Executor(実行ファイル番号、処理コマンド)</remarks>
        /// <br/>
        /// 実行ﾌｧｲﾙ番号：0 SuperBMD :1 J3DView :3 ARCTool 以降:Tool_Names[]参照
        /// </summary>
        /// 
        public static int  File_Executor(short exenum ,string argpath) {
            string User_Root = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
            string ErrorCMD;
            string OutputCMD;
            string lowname;
            
            string CDpath = Path.Combine(User_Root,Tool_F_Names[exenum]);
            string exepath = Path.Combine(CDpath,Tool_Names[exenum]);
            tssl2.Text = "";
            Form1.Form1Instance.Height = 384;

            //if (exenum != 5)
            //{

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
            //}
            //else {
            //    exepath = Tool_Names[exenum];


            //}
            //ステータスバーの設定とフォームの非アクティブ化
            tssl2.Text = Tool_Names[exenum]+"を開いています";
            Form1.Form1Instance.Enabled = false;

            //クラスインスタンス
            ProcessStartInfo psi = new ProcessStartInfo();

            //プロセス実行
            psi.FileName = exepath;
            psi.WorkingDirectory = CDpath;
            psi.Arguments = argpath;
            psi.UseShellExecute = false ;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            ErrorCMD = p.StandardError.ReadToEnd();
            OutputCMD = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            
            //エラーが出たかを確認
            if (p.ExitCode != 0)
            {
                //ステータスバーの設定とフォームのアクティブ化
                tssl2.Text = "";
                Form1.Form1Instance.Enabled = true;
                tssl2.Text = Tool_Names[exenum] + "でエラーが発生しました";
                tssl2.ForeColor = Color.Red;

                //CMDのエラーメッセージを表示する
                txt3.Text = "下記アウトプット" + env.NewLine + OutputCMD;
                txt3.AppendText(env.NewLine + "下記エラーログ" + env.NewLine + ErrorCMD);

                //フォームの長さを伸ばす
                Form1.Form1Instance.Height = 590;
                
            }



            //ステータスバーの設定とフォームのアクティブ化
            tssl2.Text = Path.GetFileName(Tool_Names[exenum]) + "が正常終了";
            tssl2.ForeColor = Color.Green;
            Form1.Form1Instance.Enabled = true;
            return p.ExitCode;
        }
    }
}
