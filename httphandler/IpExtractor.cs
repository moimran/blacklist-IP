using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace BlackListFeed
{
    class IpExtractor
    {
        List<string> FList=new List<string>();
        public static ConcurrentBag<string> BlackList = new ConcurrentBag<string>();
        public void ExtractIP()
        {
            string path = Common.GetPath();
            Common.FileList().ForEach(Console.WriteLine);

            Parallel.ForEach(Common.FileList(), file =>
                {
                    using (var rd = new StreamReader(path+file.ToString()))
                    {
                        while (!rd.EndOfStream)
                        {
                            var match = Regex.Match(rd.ReadLine(), @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                            if (match.ToString() != "")
                            {
                                BlackList.Add(match.ToString());
                            }

                        }
                    }
                });
            Console.WriteLine(BlackList.Count);

            List<string> distinct = BlackList.Distinct().ToList();
            Console.WriteLine(distinct.Count);
        }
        

    }
}
