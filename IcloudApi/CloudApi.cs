using System;

namespace IcloudApi
{
    public class CloudApi
    {
        private readonly RequestExecutor m_requestExecutor = new RequestExecutor(new ContainerManager(@"C:\temp"));
        private readonly CredentialSerializer m_credentialSerializer = new CredentialSerializer();
        private AuthResponse m_authResponse;

        public void Authorization(Credentials _credentials)
        {
            if(_credentials == null)
                throw new ArgumentNullException(nameof(_credentials));

            var credentialsData = m_credentialSerializer.Serialize(_credentials);

            var result = m_requestExecutor.POST("https://setup.icloud.com/setup/ws/1/login", credentialsData);

            var authResponseParser = new AuthResponseParser();
            m_authResponse = authResponseParser.Parse(result);

            m_requestExecutor.Headers.Set("Cookie", m_requestExecutor.ResponseHeaders["Set-Cookie"]);
        }

        public void GetReminders()
        {
            var result =
                m_requestExecutor.GET(
                    $"{m_authResponse.RemindersUrl}/rd/startup?clientBuildNumber=1812&dsid={m_authResponse.Dsid}&lang=ru-ru&usertz=US/Pacific");
        }
    }
}