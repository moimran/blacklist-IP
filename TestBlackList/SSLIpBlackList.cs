using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacklistNew
{
    class SSLIpBlackList : Extractor
    {
        private Dictionary<string, string> FileUrl = new Dictionary<string, string>();

        public override void ExtractIP()
        {
            foreach (var file in Common.FileList())
            {
                if (file.ToString().Contains("sslipblacklist.csv"))
                {
                    Console.WriteLine(file.ToString());
                    SslBlackList(file);
                }
            }
        }

        internal void SslBlackList(FileInfo mfile)
        {
            string path = Common.GetPath();
            //FileUrl = Common.GetURL();// Creates a dictionary of Filename and URL from which it got downloaded
            using (var rd = new StreamReader(path + mfile.ToString()))
            {
                //Console.WriteLine(mfile.ToString());
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
                string line1 = rd.ReadLine();
                string line2 = rd.ReadLine();
                string line3 = rd.ReadLine();
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();
                    string[] info = line.Split(',');
                    IpDate = line3.Split(':')[1];
                    if (info.Count() == 3)
                    {
                        IP = info[0];
                        IpDescription = info[2];
                        category = "C & C";
                        URL = "https://sslbl.abuse.ch";
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
        }

        public override void Register(IExtractor obj)
        {
            Feeder reg = new Feeder();
            SSLIpBlackList feed = new SSLIpBlackList();
            reg.FeedList.Add(feed);
        }
    }
}
