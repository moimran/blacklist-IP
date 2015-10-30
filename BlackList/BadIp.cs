using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using Newtonsoft.Json;

namespace BlacklistNew
{
    class BadIp : Extractor
    {

        public override Dictionary<string, List<KeyValuePair<string, List<string>>>> ExtractIP()
        {
            return BadIpMain();
        }
        public static List<string> Get(string url)
        {
            List<string> CompleteIPs = new List<string>();
            List<KeyValuePair<string, List<string>>> InfoList = new List<KeyValuePair<string, List<string>>>();
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            StreamReader rd = new StreamReader(res.GetResponseStream(), Encoding.ASCII);
            while (!rd.EndOfStream)
            {
                CompleteIPs.Add(rd.ReadLine().Trim());

            }
            CompleteIPs = CompleteIPs.Distinct().ToList();
            return CompleteIPs;

        }
        public static Dictionary<string, List<Dictionary<string, string>>> BadIpdict()
        {
            string Categories = "https://www.badips.com/get/categories";
            List<string> CompleteCat = new List<string>();
            WebRequest req = WebRequest.Create(Categories);
            WebResponse res = req.GetResponse();
            StreamReader rd = new StreamReader(res.GetResponseStream(), Encoding.ASCII);
            while (!rd.EndOfStream)
            {
                CompleteCat.Add(rd.ReadLine().Trim());

            }

            Dictionary<string, string> CatDesc = new Dictionary<string, string>();
            Dictionary<string, List<Dictionary<string, string>>> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(CompleteCat[0]);
            return htmlAttributes;
        }

        public Dictionary<string, List<KeyValuePair<string, List<string>>>> BadIpMain()
        {
            List<string> AllIP = new List<string>();
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
            Dictionary<string, string> CatDesc = new Dictionary<string, string>();
            CatDesc = GetCatDesc();
            foreach (string cat in CatDesc.Keys)
            {
                AllIP = Get("https://www.badips.com/get/list/" + cat + "/0");
                foreach (string ip in AllIP)
                {
                    IP = ip;
                    category = cat;
                    IpDescription = CatDesc[cat];
                    URL = "https://www.badips.com/get/list/" + cat + "/0";
                    IpDate = "No Date Provided";
                    DescriptionList.Add(IpDescription);
                    CategoryList.Add(category);
                    UrlList.Add(URL);
                    IpDateList.Add(IpDate);
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldC, CategoryList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldD, DescriptionList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldU, UrlList));
                    InfoList.Add(new KeyValuePair<string, List<string>>(fieldT, IpDateList));
                    if (IP != "") { 
                    if (!BlackListDB.ContainsKey(IP))
                    {
                        totalip.Add(IP);
                        BlackListDB.Add(IP, InfoList);
                    }
                    }
                    CategoryList = new List<string>();
                    DescriptionList = new List<string>();
                    UrlList = new List<string>();
                    InfoList = new List<KeyValuePair<string, List<string>>>();
                    IpDateList = new List<string>();

                }
            }
            return BlackListDB;
        }

        public static Dictionary<string, string> GetCatDesc()
        {
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            Dictionary<string, List<Dictionary<string, string>>> htmlAttributes = new Dictionary<string, List<Dictionary<string, string>>>();
            htmlAttributes = BadIpdict();
            Dictionary<string, string> CatDesc = new Dictionary<string, string>();
            foreach (List<Dictionary<string, string>> keyvalue in htmlAttributes.Values)
            {
                foreach (Dictionary<string, string> dict in keyvalue)
                {
                    foreach (KeyValuePair<string, string> item in dict)
                    {
                        if (item.Key == "Name")
                        {
                            temp1.Add(item.Value);
                        }
                        if (item.Key == "Desc")
                        {

                            if (item.Value == "")
                            {
                                string x = "No Desc";
                                temp2.Add(x);
                            }
                            else
                            {
                                temp2.Add(item.Value);
                            }
                        }
                        CatDesc = temp1.Zip(temp2, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                    }
                }
            }
            return CatDesc;
        }
        public override void Register(IExtractor obj)
        {
            Feeder reg = new Feeder();
            BlockList_de feed = new BlockList_de();
            reg.FeedList.Add(feed);
        }
    }

}
