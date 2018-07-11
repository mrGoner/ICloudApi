using System;
using System.Net;

namespace IcloudApi
{
    internal class RequestExecutor : WebClient
    {
        public CookieContainer CurrentCookies { get; }

        public RequestExecutor(IContainerManager _containerManager)
        {
            if (_containerManager == null)
                throw new ArgumentNullException(nameof(_containerManager));

            CurrentCookies = _containerManager.GetCookie();

            Headers.Add("Origin", "https://www.icloud.com");
            Headers.Add("Referer", "https://setup.icloud.com/setup/ws/1/login");
            Headers.Add("Content-Type", "text/plain");
            Encoding = System.Text.Encoding.UTF8;
        }

        public string GET(string _url)
        {
            return DownloadString(_url);
        }

        public string POST(string _url, string _data)
        {
            return UploadString(_url, _data);
        }

        protected override WebRequest GetWebRequest(Uri _address)
        {
            var request = base.GetWebRequest(_address);

            if (request is HttpWebRequest webRequset)
                webRequset.CookieContainer = CurrentCookies;

            return request;
        }
    }
}
