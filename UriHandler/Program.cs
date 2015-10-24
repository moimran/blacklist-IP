using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UriHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            String hreflink = "http://atrack.h3x.eu/api/asprox_active_csv.php";
            Uri uri = new Uri(hreflink);

            string filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                Console.WriteLine(filename);
        }
    }
}
