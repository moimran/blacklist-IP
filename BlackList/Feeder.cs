using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using System.IO;

namespace BlacklistNew
{
    internal class Feeder
    {
        List<Dictionary<string, List<KeyValuePair<string, List<string>>>>> BlackListFull = new List<Dictionary<string, List<KeyValuePair<string, List<string>>>>>();
        Dictionary<string, List<KeyValuePair<string, List<string>>>> BlackListUnique = new Dictionary<string, List<KeyValuePair<string, List<string>>>>();
        internal List<IExtractor> FeedList = new List<IExtractor>();
        List<string> dup = new List<string>();
        Ipset Ipsetdb = new Ipset();
        ShunList shunDb = new ShunList();
        BlockList_de BlockDb = new BlockList_de();
        SSLIpBlackList ccList = new SSLIpBlackList();
        BadIp badip = new BadIp();
        public void Runner()
        {
            FeedList.Add(Ipsetdb);
            FeedList.Add(shunDb);
            FeedList.Add(BlockDb);
            FeedList.Add(ccList);
            FeedList.Add(badip);
            foreach (IExtractor item in FeedList)
            {
                feeds(item);
                //DbShow(item);
            }
            Analysis();
        }

        public void Analysis()
        {
            foreach (var blacklistDB in BlackListFull)
            {
                foreach (KeyValuePair<string, List<KeyValuePair<string, List<string>>>> item in blacklistDB)
                {
                    string ip = item.Key;
                    if (!BlackListUnique.ContainsKey(ip))
                    {
                        //BlackListUnique[ip] = item.Value;
                        BlackListUnique.Add(ip, item.Value);
                    }
                    else
                    {
                        dup.Add(ip);
                        for (int i = 0; i < item.Value.Count; i++)
                        {
                            foreach (var item2 in item.Value.ElementAt(i).Value)
                            {
                                List<KeyValuePair<string, List<string>>> data = BlackListUnique[ip];
                                foreach (KeyValuePair<string, List<string>> item3 in data)
                                {
                                    if (item.Value.ElementAt(i).Key == item3.Key)
                                    {
                                        if (!item3.Value.Contains(item2))
                                        {
                                            item3.Value.Add(item2);
                                        }

                                    }
                                }
                            }
                        }
                    }


                }

            }
            Console.WriteLine("ToTal Number of BlackList IP's = {0}", BlackListUnique.Count());
            Console.WriteLine("ToTal Number of duplicates IP's Found = {0}", dup.Count());
            using (StreamWriter sw = File.CreateText("blacklistDB.csv"))
            {
                Console.WriteLine("Writing to File....");
                foreach (KeyValuePair<string, List<KeyValuePair<string, List<string>>>> item in BlackListUnique)
                {
                    string ip = item.Key;
                    //Console.WriteLine("IP = {0}", ip);
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        //Console.WriteLine("{0}", item.Value.ElementAt(i).Key);
                        foreach (var item2 in item.Value.ElementAt(i).Value)
                        {
                            //Console.WriteLine(item2);
                            sw.WriteLine("{0},{1},{2}", ip, item.Value.ElementAt(i).Key, item2);
                        }
                    }
                }

            }
        }


        public void feeds(IExtractor obj)
        {
            Dictionary<string, List<KeyValuePair<string, List<string>>>> temp = obj.ExtractIP();
            BlackListFull.Add(temp);

        }
        private static void DbShow(IExtractor shunDb)
        {
            
            using (StreamWriter sw = File.CreateText("blacklistDB.csv"))
            {

           
            foreach (KeyValuePair<string, List<KeyValuePair<string, List<string>>>> item in shunDb.BlackListDB)
            {
                string ip = item.Key;
                //Console.WriteLine("IP = {0}", ip);
                for (int i = 0; i < item.Value.Count; i++)
                {
                    //Console.WriteLine("{0}", item.Value.ElementAt(i).Key);
                    foreach (var item2 in item.Value.ElementAt(i).Value)
                    {
                        //Console.WriteLine(item2);
                        sw.WriteLine("{0},{1},{2}",ip, item.Value.ElementAt(i).Key, item2);
                    }
                }
            }

            }
        }


    }
}

