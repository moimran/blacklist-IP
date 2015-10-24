using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackListFeed
{
    class Program
    {
        static void Main(string[] args)
        {
            //------------------File download-------------------------
            List<string> UrlList = new List<string>();
            List<string> Honeypot=new List<string>();
            using (StreamReader reader = new StreamReader("blackList.conf"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        UrlList.Add(line.Trim());
                        //Console.WriteLine(line); // Use line.
                    }
                }

            }
            //UrlList.ForEach(Console.WriteLine);
            //List<string> UrlList = new List<string> { "http://autoshun.org/files/shunlist.csv", "https://www.packetmail.net/iprep.txt", "http://atrack.h3x.eu/api/asprox_active_csv.php", "http://malc0de.com/bl/IP_Blacklist.txt" };
            //List<string> UrlList = new List<string> { "https://raw.githubusercontent.com/ktsaou/blocklist-ipsets/master/alienvault_reputation.ipset" };
            MultipleFeeds feeds = new MultipleFeeds(UrlList);
            feeds.StartTasks();
            //string path = Common.GetPath();

            //Common.FileList().ForEach(Console.WriteLine);

            IpExtractor ex = new IpExtractor();
            ex.ExtractIP();
            APiHadler.ApiHandler();
            List<string> hit = new List<string>();

            using (StreamReader reader = new StreamReader("honeypot.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        Honeypot.Add(line.Trim());
                        Console.WriteLine(line); // Use line.
                    }
                }
            }
            List<string> nDistinct = IpExtractor.BlackList.Distinct().ToList();
            Console.WriteLine("cout----{0}", nDistinct);
            Console.WriteLine("=============================================================");
            foreach (var item in nDistinct)
            {
                if (Honeypot.Contains(item))
                {
                    Console.WriteLine(item);
                    hit.Add(item);
                }
                
            }




            //---------------------File watcher----------------------
            //FileWatcher watch = new FileWatcher(path);
            //watch.CreateFileWatcher();

            //Thread.Sleep(3000);
            //feeds.StartTasks();
            //Console.ReadKey();


        }
    }
}
