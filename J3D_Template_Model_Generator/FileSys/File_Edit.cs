using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace J3D_Template_Model_Generator.FileSys
{
    class File_Edit
    {
        public static void Directry_Files_Copy(string path1 , string path2) 
        {
            //コピー元のディレクトリにあるファイルをコピー
            string[] files = Directory.GetFiles(path1);
            foreach (string file in files)
            {
                if (Path.GetExtension(file) != ".json")
                {
                    File.Copy(file, path2 + @"\" + Path.GetFileName(file), true);
                }
            }
        }
    }
}
