using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

namespace J3D_Template_Model_Generator.FileSys
{
    public class testes 
    {
        public string a;
        public string b;
    }
    /// <summary>
    /// J3D_Template_Model_Generatorの作業フォルダにある<br/>
    /// Systemフォルダ内のシステムファイルをロードするクラス。
    /// </summary>
    public sealed class SystemFileLoader
    {
        private static readonly string _currentPath = Properties.Settings.Default.設定 + @"J3D_Template_Model_Generator\";
        private static readonly string _xmlFile = _currentPath + "System\\ComboBoxSettings.xml";

        private static List<string> _tempNames;
        private static List<string> _comboxItem_JP;
        private static List<string> _comboxItem_EN;
        private static List<string> _needMats;

        public static List<string> TempNames
        {
            get
            {
                if (_tempNames == default || _tempNames.Count < 0)
                {
                    return _tempNames = new List<string> { "None" };
                }
                return _tempNames;
            }
            private set => _tempNames = value;
        }

        public static List<string> ComboxItem_JP
        {
            get
            {
                if (_comboxItem_JP == default || _comboxItem_JP.Count < 0)
                {
                    return _comboxItem_JP = new List<string> { "なし" };
                }
                return _comboxItem_JP;
            }
            private set => _comboxItem_JP = value;
        }

        public static List<string> ComboxItem_EN
        {
            get
            {
                if (_comboxItem_EN == default || _comboxItem_EN.Count < 0)
                {
                    return _comboxItem_EN = new List<string> { "None" };
                }
                return _comboxItem_EN;
            }
            private set => _comboxItem_EN = value;
        }

        public static List<string> NeedMats
        {
            get
            {
                if (_needMats == default || _needMats.Count < 0)
                {
                    return _needMats = new List<string> { string.Empty };
                }
                return _needMats;
            }
            private set => _needMats = value;
        }

        private static void SystemFileCreate(string path, byte[] fileBytes)
        {

            FileInfo fi = new FileInfo(path);
            var fic = fi.Create();
            fic.Write(fileBytes, 0, fileBytes.Length);
            fic.Close();

        }

        /// <summary>
        /// システムフォルダのXMLファイルをロードします。<br/>
        /// 対象のXmlがない場合は,Properties.Resources.ComboBoxSettingsからxmlファイルを作成
        /// </summary>
        public static void Xml()
        {
            if (File.Exists(_xmlFile) == false)
            {
                SystemFileCreate(_xmlFile, Properties.Resources.ComboBoxSettings);
            }
            TempNames = new List<string>();
            ComboxItem_JP = new List<string>();
            ComboxItem_EN = new List<string>();
            NeedMats = new List<string>();

            XDocument xdoc = XDocument.Load(_xmlFile);
            XElement xel = xdoc.Element("BTK");
            IEnumerable<XElement> xml = xel.Elements("Temp");
            //testes ts = new testes();
            //var t = typeof(testes);
            //FieldInfo[] mia = t.GetFields();
            //foreach(var mi in mia)
            //Console.WriteLine(mi);
            //Console.WriteLine("Tempエレメント数  " + xml.ElementAt(0).Elements().Count());

            foreach (XElement temp in xml)
            {
                TempNames.Add(temp.Attribute("Name").Value);
                ComboxItem_JP.Add(temp.Element("JP").Value);
                ComboxItem_EN.Add(temp.Element("EN").Value);
                NeedMats.Add(temp.Element("NeedMat").Value);
            }

            //Console.WriteLine("TempName".PadRight(30) + "日本語".PadRight(15, '　') + "EN".PadRight(30) + "NeedMat".PadRight(40));
            //for (int i = 0; i < xml.Count(); i++)
            //{
            //    Console.Write(TempNames[i].PadRight(30));
            //    Console.Write(ComboxItem_JP[i].PadRight(15, '　'));
            //    Console.Write(ComboxItem_EN[i].PadRight(30));
            //    Console.WriteLine(NeedMats[i].PadRight(40));
            //}
        }
    }

}
