using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace J3D_Template_Model_Generator.FileSys
{
    class Message
    {
        public static TextBox tx1 = Form1.Form1Instance.textBox1;
        public static DialogResult sysmes(short Message_Type =0) {
            DialogResult result ;
            Console.WriteLine(Properties.Settings.Default.LangageType);
            if (Properties.Settings.Default.LangageType == "日本語")
            {
                switch (Message_Type)
                {
                    case 0:
                        return
                            MessageBox.Show("ファイルを作成しますか？" + "\n\r" + "しない場合はプログラムを終了します", "ファイル作成許可", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    case 1:
                        return
                            MessageBox.Show("ReadMeに記載された必要な" + "\n\r" + "ファイルを準備してください", "作成しました", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 2:
                        return
                            MessageBox.Show("任意の場所に" + "\n\r" + "作業フォルダを作成しますか？"  + "\n\r" + "以前の作業フォルダがある場合は「はい」を選択して" + "\n\r" + "以前選択したディレクトリを選択", "作成許可", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    case 3:
                        return
                            MessageBox.Show("以前のフォルダが存在したので" + "\n\r" + "そのディレクトリを使用します" + "\n\r" + "次にアップデートなどで" + "\n\r" + "足りないフォルダを作成します", "作成しませんでした", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 4:
                        return
                            MessageBox.Show("J3Dviewで各テクスチャの" + "\n\r" + "Unknown2の項目を" + "\n\r" + "「255」→「0」にしてください", "作業説明", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 5:
                        return
                            MessageBox.Show("BTKファイルなどを使用しませんでした" + "\n\r" + "モデルが正しく表示されているか確認してください" + "\n\r" + "必要ない場合j3dviewを閉じてください", "作業説明", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 6:
                        return
                            MessageBox.Show(tx1.Text + ".objまたは" + "\n\r" + tx1.Text + ".mtl" + "がありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    case 7:
                        return
                            MessageBox.Show(tx1.Text + ".kclファイルがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    case 8:
                        return
                            MessageBox.Show(tx1.Text + ".paファイルがありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    default:
                        return result　= DialogResult.No;
                }
            }
            else if(Properties.Settings.Default.LangageType == "EN")
            {
                switch (Message_Type)
                {
                    case 0:
                        return
                            MessageBox.Show("Do you want to create a folder?" + "\n\r" + "If not, exit the program", "Create Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    case 1:
                        return
                            MessageBox.Show("Please prepare the necessary software.", "Created Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 2:
                        return
                            MessageBox.Show("Do you want to create a working folder in any location ? \n\r"+ "No is recommended.\n\r"+ "If you have a previous working directory, \r\n"+ "select Yes and select the previously selected directory.", "Create Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    case 3:
                        return
                            MessageBox.Show("Since the previous folder existed, use that directory." + "\n\r" + "Next, create the missing folder for updates etc.", "Did not create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 4:
                        return
                            MessageBox.Show("In J3Dview, set the Unknown2 item" + "\n\r" + "of each image to 255 → 0.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 5:
                        return
                            MessageBox.Show("I didn't use BTK files etc. " + "\n\r" + "Please check if the model is displayed correctly. " + "\n\r" + "If you don't need it, please close j3dview.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    case 6:
                        return
                            MessageBox.Show("Missing"+tx1.Text + ".obj or" + "\n\r" + tx1.Text + ".mtl", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    case 7:
                        return
                            MessageBox.Show(tx1.Text + ".kcl there is not", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    case 8:
                        return
                            MessageBox.Show(tx1.Text + ".pa there is not", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    default:
                        return result = DialogResult.No;
                }
            }
            return result = DialogResult.No;
        }
    }
}
