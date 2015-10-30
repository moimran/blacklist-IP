using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;


namespace BlacklistNew
{
    class Ipset : Extractor
    {


        private Dictionary<string, string> FileUrl = new Dictionary<string, string>();
        public override Dictionary<string, List<KeyValuePair<string, List<string>>>> ExtractIP()
        {

            foreach (var mfile in Common.FileList())
            {
                if (mfile.ToString().Contains("ipset"))
                {
                    IPsetDB(mfile);
                }

            }
            Console.WriteLine("Extracting Ipset Database...");
            return BlackListDB;
        }
        internal void IPsetDB(FileInfo mfile)
        {

            string path = Common.GetPath();
            FileUrl = Common.GetURL();// Creates a dictionary of Filename and URL from which it got downloaded
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
                string url = "";
                List<string> tempDesc = new List<string>();
                int i = 1;
                string t = "";
                string l = "";
                int k = 0;
                while (!rd.EndOfStream)
                {
                    string x=rd.ReadLine();
                    if (i != k && k==0)
                    {
                        if (i >= 6)
                        {
                            if (!x.Contains("Maintainer"))
                            {
                                tempDesc.Add(x);                              
                            }
                            else
                            {
                                k = i;
                                k++;
                                l = string.Join(" ", tempDesc);
                                l = l.Replace("#", " ");
                                IpDescription=l;
                            }
                        }
                    }
                    i++;
                    if (i==k)
                    {
                        k = 1;
                    }
                    category = x.Contains("Category") ? x.Split(':')[1].ToString().Trim() : category;
                    IpDate = x.Contains("Source File Date") ? x.Split(':')[1].ToString().Trim() : IpDate;

                    url = x.Contains("List source URL") ? x.Split(new char[] {':'}, 2, StringSplitOptions.RemoveEmptyEntries)[1].ToString().Trim() : url;
                    var match = Regex.Match(x, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                    if (match.ToString() != "")
                    {
                        IP = match.ToString().Trim();
                        DescriptionList.Add(IpDescription);
                        CategoryList.Add(category);
                        UrlList.Add(url);
                        IpDateList.Add(IpDate);
                        InfoList.Add(new KeyValuePair<string, List<string>>(fieldC, CategoryList));
                        InfoList.Add(new KeyValuePair<string, List<string>>(fieldD, DescriptionList));
                        InfoList.Add(new KeyValuePair<string, List<string>>(fieldU, UrlList));
                        InfoList.Add(new KeyValuePair<string, List<string>>(fieldT, IpDateList));

                        if (!BlackListDB.ContainsKey(IP))
                        {
                            totalip.Add(IP);
                            BlackListDB.Add(IP, InfoList);
                        }
                        else
                        {
                            InfoList = BlackListDB[IP];
                            foreach (KeyValuePair<string, List<string>> item in InfoList)
                            {
                                if (item.Key == "category")
                                {
                                    if (!item.Value.Contains(category))
                                    {
                                        item.Value.Add(category);
                                    }
                                }
                                if (item.Key == "url")
                                {
                                    if (!item.Value.Contains(FileUrl[mfile.ToString()]))
                                    {
                                        item.Value.Add(FileUrl[mfile.ToString()]);
                                    }
                                }
                            }
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
            Ipset feed = new Ipset();
            reg.FeedList.Add(feed);
        }

    }
}
