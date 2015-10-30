using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlacklistNew
{
    class BlockList_de : Extractor
    {
        
        private Dictionary<string, string> FileUrl = new Dictionary<string, string>();

        public override Dictionary<string, List<KeyValuePair<string, List<string>>>> ExtractIP()
        {
            foreach (var file in Common.FileList())
            {
                if (file.ToString().Contains("all.txt") || file.ToString().Contains("ssh.txt") || file.ToString().Contains("bots.txt") || file.ToString().Contains("bruteforcelogin.txt"))
                {
                    blocklistDe(file);
                }

            }
            Console.WriteLine("Extracting BlockList.de list...");
            return BlackListDB;
        }

        internal void blocklistDe(FileInfo mfile)
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
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();
                    IP = line.Trim();
                    IpDate = "Provider updates every 48hrs";
                    if (mfile.ToString() == "all.txt")
                    {
                    IpDescription = "All IP addresses that have attacked one of our customers/servers in the last 48 hours. ";
                    category = "attacks";
                    }
                    if (mfile.ToString() == "ssh.txt")
                    {
                        IpDescription = "All IP addresses which have been reported within the last 48 hours as having run attacks on the service";
                        category = "ssh attacks";
                    }
                    if (mfile.ToString() == "bots.txt")
                    {
                        IpDescription = "All IP addresses which have been reported within the last 48 hours as having run attacks attacks on the RFI-Attacks, REG-Bots, IRC-Bots or BadBots (BadBots = he has posted a Spam-Comment on a open Forum or Wiki). ";
                        category = "bots";
                    }
                    if (mfile.ToString() == "ircbot.txt")
                    {
                        IpDescription = "Irc bots found in last 48 hours. ";
                        category = "ircbot";
                    }
                    if (mfile.ToString() == "bruteforcelogin.txt")
                    {
                        IpDescription = "All IPs which attacks Joomlas, Wordpress and other Web-Logins with Brute-Force Logins. ";
                        category = "bruteforcelogin";
                    }
                    URL = "http://www.blocklist.de/en/export.html";
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
                        totalip.Add(IP);
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

        public override void Register(IExtractor obj)
        {
            Feeder reg = new Feeder();
            BlockList_de feed = new BlockList_de();
            reg.FeedList.Add(feed);
        }
    }
}

