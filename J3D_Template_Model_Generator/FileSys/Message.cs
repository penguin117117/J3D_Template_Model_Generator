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
        public static DialogResult sysmes(short Message_Type =0) {
            DialogResult result = DialogResult.No;
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
                        MessageBox.Show("任意の場所に" + "\n\r" + "作業フォルダを作成しますか？" + "\n\r" + "「いいえ」を推奨します"+"\n\r"+"以前の作業フォルダがある場合は「はい」を選択して"+"\n\r"+"以前選択したディレクトリを選択", "作成許可", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                default:
                    return result;
            }
            
        }
    }
}
