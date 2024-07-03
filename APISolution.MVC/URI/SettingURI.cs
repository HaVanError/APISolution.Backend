using System.Net.Http;

namespace APISolution.MVC.URI
{
    public class SettingURI
    {
        private  readonly string _Uri;
        public SettingURI(string uri)
        {
            _Uri = uri;
        }
        public string GetFullUri(string endpoint)
        {
            return $"{_Uri}/{endpoint}";
        }
    }
}
