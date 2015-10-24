using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlacklistNew
{
    class Common
    {
        private string appPath { get; set; }
        
        //Gets list of files
        internal static List<FileInfo> FileList()
        {
            string appPath = GetPath();
            DirectoryInfo dr = new DirectoryInfo(appPath);
            List<FileInfo> mFile=new List<FileInfo>();
            List<string> sFile=new List<string>();
            mFile = dr.GetFiles().ToList();
            return mFile;
        }

        internal static string GetPath()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //appPath= appPath+"/files/";
            appPath = @"C:\Users\moimran\Documents\Visual Studio 2013\Projects\httphandler\httphandler\bin\Debug\files\";
            return appPath;
        }

        public static Dictionary<string,string> GetURL()
        {
            List<FileInfo> urllist = Common.FileList();
            Dictionary<string, string> FileUrl = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader("blackList.conf"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        foreach (FileInfo item in urllist)
                        {
                            string filename = item.ToString();
                            if (line.Contains(filename.Trim()))
                            {
                                FileUrl.Add(filename, line.Trim());
                                continue;
                            }
                        }
                    }
                }
            }
            return FileUrl;
        }

    }
}
