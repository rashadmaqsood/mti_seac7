using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using comm;

namespace SmartEyeControl_7.Common
{
    class Serializer
    {
        public static void Serialize(string filename, object obj)
        {
            string path="";
            FolderBrowserDialog  folder=new FolderBrowserDialog();
            folder.RootFolder=Environment.SpecialFolder.Desktop;
            folder.ShowDialog();
            if (folder.SelectedPath != "")
            {
                path = folder.SelectedPath + "\\" + filename;

            }
            else
            {
                MessageBox.Show("Invalid path");
                return;
            }
             XmlSerializer x = new XmlSerializer(obj.GetType());
             FileStream new_File = new FileStream(path, FileMode.Create);
             x.Serialize(new_File, obj);
             new_File.Close();
        }
        public static object DeSerialize(object obj)
        {
           
          
            string path = "";
            OpenFileDialog folder = new OpenFileDialog();
            folder.InitialDirectory = Environment.CurrentDirectory;
            folder.ShowDialog();
            if (folder.FileName != "")
            {
                path = folder.FileName;

            }
            else
            {
                MessageBox.Show("Invalid path");
                return null;
            }
            XmlSerializer x = new XmlSerializer(obj.GetType());
            FileStream new_File = new FileStream(path, FileMode.Open);
            obj =(x.Deserialize(new_File));
            new_File.Close();
            return obj;
        }
    }
}
