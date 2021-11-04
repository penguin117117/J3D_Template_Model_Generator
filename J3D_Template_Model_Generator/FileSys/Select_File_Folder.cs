using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace J3D_Template_Model_Generator.FileSys
{
    class Select_File_Folder
    {
        public static string Folder_Select() 
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "フォルダを指定してください。";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = @"c:\";
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            else 
            { 
                return Properties.Resources.DefaultPath;
            }

        }
    }
}
