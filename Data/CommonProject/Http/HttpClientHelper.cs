using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace CommonProject.Http
{
    public class HttpClientHelper
    {
        public static HttpResponseMessage RequestAsJson(HttpMethod method, string url, object model, Dictionary<string, string> headers = null)
        {

            var httpResponse = new HttpResponseMessage();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new System.Net.Http.HttpClient())
            {
                if (headers != null && headers.Any())
                {
                    foreach (var item in headers)
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                if (method == HttpMethod.Get)
                {
                    httpResponse = client.GetAsync(url).Result;
                }
                else if (method == HttpMethod.Post)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpResponse = client.PostAsJsonAsync(url, model).Result;
                }
                else if (method == HttpMethod.Put)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpResponse = client.PutAsJsonAsync(url, model).Result;
                }
                else if (method == HttpMethod.Delete)
                {
                    httpResponse = client.DeleteAsync(url).Result;
                }
                else
                {
                    throw new NotImplementedException("Currently this HTTP method is not supported...");
                }
            }
            return httpResponse;
        }
    }

}
