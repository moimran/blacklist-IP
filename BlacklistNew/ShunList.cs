using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlacklistNew
{
    class ShunList : IExtractor
    {
        public Dictionary<string, List<KeyValuePair<string, List<string>>>> BlackListDB
        {
            get { return _BlackListDB; }
            set { _BlackListDB = value; }
        }
        public List<string> totalip
        {
            get { return _totalip; }
            set { _totalip = value; }
        }
        public List<string> DescriptionList
        {
            get { return _DescriptionList; }
            set { _DescriptionList = value; }
        }
        public List<string> UrlList
        {
            get { return _UrlList; }
            set { _UrlList = value; }
        }
        public List<string> CategoryList
        {
            get { return _CategoryList; }
            set { _CategoryList = value; }
        }
        public List<string> IpDateList
        {
            get { return _IpDateList; }
            set { _IpDateList = value; }
        }

        List<string> _DescriptionList = new List<string>();
        Dictionary<string, List<KeyValuePair<string, List<string>>>> _BlackListDB = new Dictionary<string, List<KeyValuePair<string, List<string>>>>();
        List<string> _IpDateList = new List<string>();
        List<string> _CategoryList = new List<string>();
        List<string> _UrlList = new List<string>();
        List<string> _totalip = new List<string>();
        private Dictionary<string, string> FileUrl = new Dictionary<string, string>();

        public void ExtractIP()
        {
            foreach (var file in Common.FileList())
            {
                if (file.ToString().Contains("shunlist.csv"))
                {
                    shunIps(file);
                }

            }
        }

        internal void shunIps(FileInfo mfile)
        {
            string path = Common.GetPath();
            //FileUrl = Common.GetURL();// Creates a dictionary of Filename and URL from which it got downloaded
            using (var rd = new StreamReader(path + mfile.ToString()))
            {
                Console.WriteLine(mfile.ToString());
                List<KeyValuePair<string, List<string>>> InfoList = new List<KeyValuePair<string, List<string>>>();
                string fieldC = "category";
                string fieldD = "description";
                string fieldU = "url";
                string fieldT = "date";
                string IP = "";
                string IpDescription = "";
                string category = "";
                string IpDate = "";
                string URL = "";
                string header = rd.ReadLine();
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();
                    string[] info = line.Split(',');
                    IP = info[0];
                    IpDate = info[1];
                    IpDescription = info[2];
                    category = "shunlist";
                    URL = "http://autoshun.org/";
                    DescriptionList.Add(IpDescription);
                    CategoryList.Add(category);
                    UrlList.Add(URL);
                    IpDateList.Add(IpDate);
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldC, CategoryList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldD, DescriptionList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldU, UrlList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldT, IpDateList));
                    totalip.Add(IP);
                    if (!BlackListDB.ContainsKey(IP))
                    {
                        BlackListDB.Add(IP, InfoList);
                    }
                    CategoryList = new List<string>();
                    DescriptionList = new List<string>();
                    UrlList = new List<string>();
                    InfoList = new List<KeyValuePair<string, List<string>>>();
                    IpDateList = new List<string>();
               
                }
            }
        }
    }
}
