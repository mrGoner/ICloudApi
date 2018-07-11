using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IcloudApi
{
    public class CredentialSerializer
    {
        public string Serialize(Credentials _credentials)
        {
            if(_credentials == null)
                throw new ArgumentNullException(nameof(_credentials));

            var jCredentials = new JObject
            {
                new JProperty("apple_id", _credentials.AppleId),
                new JProperty("password", _credentials.Password),
                new JProperty("extended_login", _credentials.ExtendedLogin)
            };

            return jCredentials.ToString(Formatting.Indented);
        }
    }
}
