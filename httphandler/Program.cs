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
                    }
                }

            }
            MultipleFeeds feeds = new MultipleFeeds(UrlList);
            feeds.StartTasks();
            //---------------------File watcher----------------------
            //FileWatcher watch = new FileWatcher(path);
            //watch.CreateFileWatcher();

            //Thread.Sleep(3000);
            //feeds.StartTasks();
            //Console.ReadKey();


        }
    }
}
