using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlacklistNew
{
    class Program
    {
        static void Main(string[] args)
        {
            //============================================
            //Ipset Ipsetdb = new Ipset();
            //Feeder.Feeds(Ipsetdb);
            //Ipsetdb.ExtractIP();
            //DbShow(Ipsetdb);
            //============================================
            //ShunList shunDb = new ShunList();
            //shunDb.ExtractIP();
            //DbShow(shunDb);
            //=============================================
            //BlockList_de BlockDb = new BlockList_de();
            //BlockDb.ExtractIP();
            //DbShow(BlockDb);
            //Console.WriteLine(Ipsetdb.BlackListDB.Count());
            //Console.WriteLine(Ipsetdb.totalip.Count());
            //Console.WriteLine(Ipsetdb.totalip.Distinct().Count());
            //SSLIpBlackList ccList = new SSLIpBlackList();
            //ccList.ExtractIP();
            //DbShow(ccList);
            Feeder run = new Feeder();
            run.Runner();
            Console.ReadKey();
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
