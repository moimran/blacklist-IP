using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BlackListFeed
{
    class APiHadler
    {

        public static void ApiHandler()
        {
            List<string> Categories = new List<string>();
            List<string> allurls = new List<string>();
            List<string> AllIP = new List<string>();
            List<string> lstofdict = new List<string>();
            Categories.Add("https://www.badips.com/get/categories");
            lstofdict = badips.Get(Categories);
            Dictionary<string, List<Dictionary<string, string>>> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(lstofdict[0]);
            foreach (List<Dictionary<string, string>> keyvalue in htmlAttributes.Values)
            {
                foreach (Dictionary<string, string> dict in keyvalue)
                {
                    foreach (KeyValuePair<string, string> item in dict)
                    {
                        if (item.Key == "Name")
                        {
                            var category = item.Value;
                            allurls.Add("https://www.badips.com/get/list/" + category + "/0");
                        }
                    }

                }
            }
            AllIP = badips.Get(allurls);

            Console.WriteLine(AllIP.Count);
            foreach (string Ip in AllIP)
            {
                //Console.WriteLine(Ip);
                IpExtractor.BlackList.Add(Ip);

            }
            Console.WriteLine(IpExtractor.BlackList.Count());
            //List<string> nDistinct = new List<string>();
            List<string> nDistinct = IpExtractor.BlackList.Distinct().ToList();

        }
    }
}
