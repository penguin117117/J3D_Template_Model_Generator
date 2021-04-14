using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using J3D_Template_Model_Generator.FileSys;
using mes = J3D_Template_Model_Generator.FileSys.Message;

namespace J3D_Template_Model_Generator.FileSys
{
    class FolderCreate{
        public static void Set_New_Folder(string newsub) 
        {
            //宣言
            string mainfilePath = Properties.Settings.Default.設定 + "\\" + "J3D_Template_Model_Generator";

            //処理
            if (Directory.Exists(mainfilePath + "\\" + newsub)==false)
            {
                Directory.CreateDirectory(mainfilePath +  "\\" + newsub);
            }
            
        }
        public static void Set_Folder()
        {
            //宣言配列を増やすとフォルダを増やせます
            string mainfilePath = Properties.Settings.Default.設定 + "\\" + "J3D_Template_Model_Generator";
            string[] sub_Tools = { "SuperBMD", "J3D_View" , "ARC_Tool" , "Collision_Tool", "Whitehole" };
            string[] sub_Models = {"FBX","OBJ","BDL_BMD"};
            string[] sub_Anm = { "BTK" , "BRK" };
            string[] sub_dir = {"ARC","Collision" , "User_json"};
            short fcount = 0;

            //リスト化
            List<string> list = new List<string>(sub_Tools.Length + sub_Models.Length + sub_Anm.Length + sub_dir.Length);
            list.AddRange(sub_Tools);
            list.AddRange(sub_Models);
            list.AddRange(sub_Anm);
            list.AddRange(sub_dir);

            //リストから配列へ変換
            string[] dst = list.ToArray();
            Console.WriteLine("[{0}]", string.Join(", ", dst));

            //メインディレクトリのチェック&配置
            if (Directory.Exists(mainfilePath) == false)
            {
                Directory.CreateDirectory(mainfilePath);

                //サブディレクトリのチェック&配置
                foreach (string a in list)
                {
                    if (Directory.Exists(mainfilePath + a) == false)
                    {
                        Directory.CreateDirectory(mainfilePath + "\\" + a);
                        Console.WriteLine(a);

                    }
                    
                }

                //システムメッセージ
                mes.sysmes(1);
            }
            else
            {
                //メインディレクトリが存在する際も
                //サブディレクトリのチェック&配置
                //アップデートなどで増える可能性があるため
                foreach (string a in list)
                {
                    if (Directory.Exists(mainfilePath + "\\" + a) == false)
                    {
                        Directory.CreateDirectory(mainfilePath + "\\" + a);
                        Console.WriteLine(a);
                        fcount++;
                    }

                }

                //システムメッセージ
                //ディレクトリを作成した時だけ表示
                if (fcount>0) {
                    mes.sysmes(1);
                }
                else
                {
                    MessageBox.Show("作業に必要なディレクトリが" + "\n\r" + "全てありました", "作業ディレクトリチェック", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            

            


        }
    }
}
