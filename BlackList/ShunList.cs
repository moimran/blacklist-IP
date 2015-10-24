using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlacklistNew
{
    class ShunList : Extractor
    {
        private Dictionary<string, string> FileUrl = new Dictionary<string, string>();

        public override Dictionary<string, List<KeyValuePair<string, List<string>>>> ExtractIP()
        {
            foreach (var file in Common.FileList())
            {
                if (file.ToString().Contains("shunlist.csv"))
                {
                    shunIps(file);
                }
            }
            return BlackListDB;
        }

        internal void shunIps(FileInfo mfile)
        {
            string path = Common.GetPath();
            //FileUrl = Common.GetURL();// Creates a dictionary of Filename and URL from which it got downloaded
            using (var rd = new StreamReader(path + mfile.ToString()))
            {
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
                    IpDate=info[1];
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
                    if (!BlackListDB.ContainsKey(IP))
                    {
                        BlackListDB.Add(IP, InfoList);
                        totalip.Add(IP);
                    }
                    CategoryList = new List<string>();
                    DescriptionList = new List<string>();
                    UrlList = new List<string>();
                    InfoList = new List<KeyValuePair<string, List<string>>>();
                    IpDateList = new List<string>();
                }
            }
        }

        public override void Register(IExtractor obj)
        {
            Feeder reg = new Feeder();
            ShunList feed = new ShunList();
            reg.FeedList.Add(feed);
        }
    }
}
