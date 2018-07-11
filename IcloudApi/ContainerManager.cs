using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace IcloudApi
{
    public class ContainerManager : IContainerManager
    {
        private readonly BinaryFormatter m_binaryFormatter;
        private readonly string m_pathToSetings;

        public ContainerManager(string _pathToSettings)
        {
            if(string.IsNullOrWhiteSpace(_pathToSettings))
                throw new ArgumentException("Path can not be null or empty!");

            m_binaryFormatter = new BinaryFormatter();
            m_pathToSetings = Path.Combine(_pathToSettings, "cookies.bin");
        }

        public CookieContainer GetCookie()
        {
            if(!File.Exists(m_pathToSetings))
                return new CookieContainer();

            using (Stream stream = new FileStream(m_pathToSetings, FileMode.Open))
            {
                return (CookieContainer) m_binaryFormatter.Deserialize(stream);
            }
        }

        public void SaveCookie(CookieContainer _cookieContainer)
        {
            if (_cookieContainer == null)
                throw new ArgumentNullException(nameof(_cookieContainer));

            using (Stream stream = new FileStream(m_pathToSetings, FileMode.Create))
            {
                m_binaryFormatter.Serialize(stream, _cookieContainer);
            }
        }
    }
}
