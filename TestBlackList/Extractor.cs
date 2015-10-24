using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacklistNew
{
    abstract class Extractor : IExtractor
    {
        public Dictionary<string, List<KeyValuePair<string, List<string>>>> BlackListDB
        {
            get { return _BlackListDB; }
            set { _BlackListDB = value; }
        }
        public List<string> totalip
        {
            get { return _totalip; }
            set { _totalip = value; }
        }
        public List<string> DescriptionList
        {
            get { return _DescriptionList; }
            set { _DescriptionList = value; }
        }
        public List<string> UrlList
        {
            get { return _UrlList; }
            set { _UrlList = value; }
        }
        public List<string> CategoryList
        {
            get { return _CategoryList; }
            set { _CategoryList = value; }
        }
        public List<string> IpDateList
        {
            get { return _IpDateList; }
            set { _IpDateList = value; }
        }

        List<string> _DescriptionList = new List<string>();
        Dictionary<string, List<KeyValuePair<string, List<string>>>> _BlackListDB = new Dictionary<string, List<KeyValuePair<string, List<string>>>>();
        List<string> _IpDateList = new List<string>();
        List<string> _CategoryList = new List<string>();
        List<string> _UrlList = new List<string>();
        List<string> _totalip = new List<string>();

        public abstract void ExtractIP();
        public abstract void Register(IExtractor obj);

    }
}
