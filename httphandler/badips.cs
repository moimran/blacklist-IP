using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackListFeed
{
    class badips
    {
        public static List<string> Get(List<string> url)
        {
            List<string> CompleteIPs = new List<string>();
            List<string> DupIPs = new List<string>();

            foreach (string webadd in url)
            {
                WebRequest req = WebRequest.Create(webadd);
                WebResponse res = req.GetResponse();
                StreamReader rd = new StreamReader(res.GetResponseStream(), Encoding.ASCII);
                while (!rd.EndOfStream)
                {
                    CompleteIPs.Add(rd.ReadLine().Trim());
                }

            }

            DupIPs = CompleteIPs.Distinct().ToList();
            Console.WriteLine(DupIPs.Count);
            return CompleteIPs;

        }
    }
}
