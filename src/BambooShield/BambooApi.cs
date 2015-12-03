using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace BambooShield
{
    public static class BambooApi
    {
        private static readonly string BaseUrl;
        private static readonly string Authorization;

        static BambooApi()
        {
            BaseUrl = Environment.GetEnvironmentVariable("BAMBOOSHIELD_API_BASE_URL")?.TrimEnd('/');
            var login = Environment.GetEnvironmentVariable("BAMBOOSHIELD_API_LOGIN");
            var password = Environment.GetEnvironmentVariable("BAMBOOSHIELD_API_PASSWORD");
            Authorization = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"))}";
        }

        public static BuildStatus GetBuildStatus(string projectKey, string buildKey, out string subject, out string status)
        {
            var buildStatus = BuildStatus.Unknown;
            subject = null;
            status = null;
            try
            {
                var escapedProjectKey = Uri.EscapeDataString(projectKey);
                var escapedBuildKey = Uri.EscapeDataString(buildKey);
                var planUri = $"{BaseUrl}/plan/{escapedProjectKey}-{escapedBuildKey}?os_authType=basic";
               
                var planDocument = DoRestRequest(planUri);

                subject =
                    planDocument.Root.GetElementString("description")
                    ?? (
                        planDocument.Root.GetElementString("buildName")
                            ?.Insert(0,planDocument.Root.GetAttributeString("shortName")?.Append(" - ") ?? string.Empty)
                        );

                var isBuilding = planDocument.Root.GetElementBool("isBuilding");
                if (isBuilding == true)
                {
                    return buildStatus = BuildStatus.Building;
                }

                var resultUri = $"{BaseUrl}/result/{escapedProjectKey}-{escapedBuildKey}-latest?os_authType=basic";
                var resultDocument = DoRestRequest(resultUri);
                var success = resultDocument.Root.GetAttributeBool("successful");
                return (success == true)
                    ? buildStatus = BuildStatus.Passing
                    : buildStatus = BuildStatus.Failing;
            }
            catch (Exception ex)
            {
                status = ex.Message;
                buildStatus =  BuildStatus.Error;
            }
            finally
            {
                subject = subject ?? $"{projectKey} - ${buildKey}";
                status = status ?? buildStatus.ToString().ToLowerInvariant();
            }
            return buildStatus;
        }

        private static XDocument DoRestRequest(string uri)
        {
            XDocument document;
            var webRequest = (HttpWebRequest) WebRequest.Create(uri);
            webRequest.Headers.Add("Authorization", Authorization);
            webRequest.Method = "GET";
            var response = (HttpWebResponse) webRequest.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                document = XDocument.Load(responseStream);
            }
            return document;
        }

        private static string Append(this string source, string value)
        {
            return string.Concat(source, value);
        }


        private static string GetElementString(this XContainer container, string key)
        {
            return container?.Elements(key)
                .Select(element => element.Value)
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        }

        private static bool? GetElementBool(this XContainer container, string key)
        {
            return container?.Elements(key)
                .Select(value => StringComparer.OrdinalIgnoreCase.Equals("true", value.Value) as bool?)
                .FirstOrDefault();
        }

        private static bool? GetAttributeBool(this XElement element, string key)
        {
            return element?.Attributes(key)
                .Select(attribute => StringComparer.OrdinalIgnoreCase.Equals("true", attribute.Value) as bool?)
                .FirstOrDefault();
        }

        private static string GetAttributeString(this XElement element, string key)
        {
            return element?.Attributes(key)
                .Select(attribute => attribute.Value)
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        }
    }
}
