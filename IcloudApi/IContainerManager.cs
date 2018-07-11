using System.Net;

namespace IcloudApi
{
    public interface IContainerManager
    {
        CookieContainer GetCookie();
        void SaveCookie(CookieContainer _cookieContainer);
    }
}