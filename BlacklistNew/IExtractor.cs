using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacklistNew
{
    public interface IExtractor
    {
        Dictionary<string, List<KeyValuePair<string, List<string>>>> BlackListDB { get; set; } //BlackListDB
        List<string> totalip { get; set; } 
        List<string> DescriptionList {get;set;} 
        List<string> UrlList {get;set;} 
        List<string> CategoryList {get;set;}
        List<string> IpDateList { get; set; } 

    }
}
