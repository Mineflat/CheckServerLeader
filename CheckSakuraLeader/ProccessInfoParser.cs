
using Newtonsoft.Json.Linq;

namespace CheckSakuraLeader
{
    internal static class ProccessInfoParser
    {
        private static HttpClient httpClient = new ();
        /// <summary>
        /// Проверка, является ли сервер с указанным UUID лидером кластера
        /// </summary>
        /// <param name="URI">Адрес, используемый для получения отладочной информации</param>
        /// <param name="targetUUID">UUID предполагаемого лидера кластера</param>
        public static void CheckServerLeader(string URI, string targetUUID)
        {
            string serverResponce = GetRequestBody(URI).Result;
            dynamic data = JObject.Parse(serverResponce);
            for (int i = 0; i < data.nodes.Count; i++)
            {
                if (data.nodes[i].uid == targetUUID)
                {
                    if (data.nodes[i].state == "master" && data.nodes[i].isMaster == true)
                        Environment.Exit(0);
                    else
                        Environment.Exit(1);
                }
            }
        }
        /// <summary>
        /// Выполнение HTTP GET-запроса по указанному URI
        /// </summary>
        /// <param name="URI">URI, на который будет отправлен запрос</param>
        /// <returns></returns>
        private static async Task<string> GetRequestBody(string URI)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient client = new(clientHandler);
            return await client.GetStringAsync(URI);
        }
    }
}
