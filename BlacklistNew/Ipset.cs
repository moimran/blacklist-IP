using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;


namespace BlacklistNew
{
    class Ipset : IExtractor
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
            get {return _DescriptionList;}
            set {_DescriptionList=value; } 
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

            foreach (var mfile in Common.FileList())
            {
                if (mfile.ToString().Contains("ipset"))
                {
                    IPsetDB(mfile);
                }

            }
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
                Console.WriteLine(mfile.ToString());
                while (!rd.EndOfStream)
                {
                    string x = rd.ReadLine();
                    IpDescription = x.Contains("#") ? IpDescription + x + "\n" : IpDescription;
                    category = x.Contains("Category") ? x.Split(':')[1].ToString().Trim() : category;
                    IpDate = x.Contains("Source File Date") ? x.Split(':')[1].ToString().Trim() : IpDate;
                    var match = Regex.Match(x, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                    if (match.ToString() != "")
                    {
                        IP = match.ToString().Trim();
                        DescriptionList.Add(IpDescription);
                        CategoryList.Add(category);
                        UrlList.Add(FileUrl[mfile.ToString()]);
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


    }
}
