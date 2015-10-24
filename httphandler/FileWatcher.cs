using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackListFeed
{
    class FileWatcher
    {
        public string path { get; set; }
        public FileWatcher(string appPath)
        {
            path = appPath;
        }
        public void CreateFileWatcher()
        {
            Console.WriteLine("Watching Files......");
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcherTxt = new FileSystemWatcher();
            FileSystemWatcher watcherCsv = new FileSystemWatcher();
            watcherTxt.Path = path;
            watcherCsv.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcherTxt.NotifyFilter = NotifyFilters.LastWrite;
            watcherCsv.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            watcherTxt.Filter = "*.txt";
            watcherCsv.Filter = "shunlist.csv";
            // Add event handlers.
            watcherTxt.Changed += new FileSystemEventHandler(OnChangedTxt);
            watcherCsv.Changed += new FileSystemEventHandler(OnChangedCsv);
            // Begin watching.
            watcherTxt.EnableRaisingEvents = true;
            watcherCsv.EnableRaisingEvents = true;
        }


        // Define the event handlers.
        private static void OnChangedTxt(object source, FileSystemEventArgs e)
        {
            try
            {
                using (var fs = File.OpenWrite(e.FullPath))
                {
                }
                // Specify what is done when a file is changed, created, or deleted.
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            }
            catch (Exception)
            {
                //no write access, other app not done
            }
            

        }

        private static void OnChangedCsv(object source, FileSystemEventArgs e)
        {
            try
            {
                using (var fs = File.OpenWrite(e.FullPath))
                {
                }
                // Specify what is done when a file is changed, created, or deleted.
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            }
            catch (Exception)
            {
                //no write access, other app not done
            }
        }

    }
}
