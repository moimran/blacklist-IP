using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackListFeed
{
    class MultipleFeeds
    {
        public List<string> UrlList { set; get; }
        public MultipleFeeds(List<string> list)
        {
            UrlList = list;
        }
        public void StartTasks()
        {
            Task[] tasks = new Task[UrlList.Count()];
            DownloadFiles feeds = new DownloadFiles();
       
            for (int i = 0; i < UrlList.Count(); i++)
            {
                int j = i;
                tasks[i] = Task.Factory.StartNew(() => { feeds.Download(UrlList[j], j); });
                
            }
            //Waits until all tasks to get completed
            Task.WaitAll(tasks);
        }
    }
}
