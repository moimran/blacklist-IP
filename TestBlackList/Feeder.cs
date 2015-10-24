using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace BlacklistNew
{
    internal class Feeder
    {
        internal List<IExtractor> FeedList = new List<IExtractor>();
        Ipset Ipsetdb = new Ipset();
        ShunList shunDb = new ShunList();
        BlockList_de BlockDb = new BlockList_de();
        SSLIpBlackList ccList = new SSLIpBlackList();

        public void Runner()
        {
            FeedList.Add(Ipsetdb);
            FeedList.Add(shunDb);
            FeedList.Add(BlockDb);
            FeedList.Add(ccList);

            //Parallel.ForEach(IExtractor item in FeedList)
            //{
            //    feeds(item);
            //}

            foreach(IExtractor item in FeedList)
            {
                feeds(item);
                DbShow(item);
            }
            

        }
        public void feeds(IExtractor obj)
        {
            obj.ExtractIP();
        }
        private static void DbShow(IExtractor shunDb)
        {
            Console.WriteLine(shunDb.BlackListDB.Count());
            Console.WriteLine(shunDb.totalip.Count());
            Console.WriteLine(shunDb.totalip.Distinct().Count());
            //foreach (KeyValuePair<string, List<KeyValuePair<string, List<string>>>> item in shunDb.BlackListDB)
            //{
            //    Console.WriteLine("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
            //    string ip = item.Key;
            //    Console.WriteLine("IP = {0}", ip);
            //    for (int i = 0; i < item.Value.Count; i++)
            //    {
            //        Console.WriteLine("{0}", item.Value.ElementAt(i).Key);
            //        foreach (var item2 in item.Value.ElementAt(i).Value)
            //        {
            //            Console.WriteLine(item2);
            //        }
            //    }
            //}
        }


    }
}
