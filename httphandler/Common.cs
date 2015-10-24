using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlackListFeed
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
            appPath= appPath+"/files/";
            return appPath;
        }

    }
}
