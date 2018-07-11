using System;
using Newtonsoft.Json.Linq;

namespace IcloudApi
{
    public class AuthResponseParser
    {
        public AuthResponse Parse(string _data)
        {
            var jResponse = JObject.Parse(_data);
            
            var authResponse = new AuthResponse();

            if (jResponse.TryGetValue("dsInfo", StringComparison.InvariantCultureIgnoreCase, out var dsJToken))
            {
                if (dsJToken is JObject jDsInfo)
                {
                    authResponse.FullName = jDsInfo.GetValue("fullName").Value<string>();
                    authResponse.Dsid = jDsInfo.GetValue("dsid").Value<string>();
                }
                else
                    throw new AuthResponseParserException($"Failed to parse dsToken expected jObject actual <{dsJToken.GetType()}> ");
            }
            else
                throw new AuthResponseParserException("Property dsInfo not found");

            if (jResponse.TryGetValue("webservices", StringComparison.InvariantCultureIgnoreCase,
                out var webServicesToken))
            {
                if (webServicesToken is JObject jWebServicesToken)
                {
                    if (jWebServicesToken.TryGetValue("reminders", out var remindersToken) &&
                        remindersToken is JObject jRemindersToken)
                    {
                        var reminderUrl = jRemindersToken.GetValue("url").Value<string>();

                        if (reminderUrl.Contains(":"))
                            reminderUrl = reminderUrl.Substring(0, reminderUrl.LastIndexOf(':'));

                        authResponse.RemindersUrl = reminderUrl;
                    }

                    if (jWebServicesToken.TryGetValue("reminders", out var notesToken) &&
                        notesToken is JObject jNotesToken)
                    {
                        var notesUrl = jNotesToken.GetValue("url").Value<string>();

                        if (notesUrl.Contains(":"))
                            notesUrl = notesUrl.Substring(0, notesUrl.LastIndexOf(':'));

                        authResponse.RemindersUrl = notesUrl;
                    }
                }
                else
                    throw new AuthResponseParserException(
                        $"Failed to parse webServicesToken expected jObject actual <{dsJToken.GetType()}> ");
            }
            else
                throw new AuthResponseParserException("Property webservices not found");

            return authResponse;
        }

        public class AuthResponseParserException : Exception
        {
            public AuthResponseParserException(string _message, Exception _exception) : base(_message, _exception) { }
            public AuthResponseParserException(string _message): base(_message) { }
        }
    }
}
