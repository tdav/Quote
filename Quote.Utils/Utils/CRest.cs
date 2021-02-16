using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Utils
{
    public static class CRest
    {
        public static async Task<TReturn> Post<TParam, TReturn>(this string ServerUrl, string Url, TParam param, string ContentType= "application/json") where TParam : class
        {
            try
            {
                var c = new HttpClient();
                c.BaseAddress = new Uri(ServerUrl);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                var cd = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, ContentType);
                var response = await c.PostAsync(Url, cd);

                if (response.IsSuccessStatusCode)
                {
                    var searchResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TReturn>(searchResult);
                }
                else
                {                    
                    throw new Exception($"Алоқада хато => {response.StatusCode} {response.ReasonPhrase}");
                }                
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }


        public static async Task<T> Get<T>(this string ServerUrl, string Url, string ContentType = "application/json") where T : class
        {
            try
            {
                var c = new HttpClient();
                c.BaseAddress = new Uri(ServerUrl);
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
                                
                var response = await c.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    var searchResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(searchResult);
                }
                else
                {
                    throw new Exception($"{ServerUrl}/{Url}  {response.StatusCode} {response.ReasonPhrase}");
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public static async Task<T> DownloadFile<T>(this string ServerUrl, string Url, string ContentType = "application/json") where T : class
        {
            try
            {
                var c = new HttpClient();
                c.BaseAddress = new Uri(ServerUrl);
                c.DefaultRequestHeaders.Accept.Clear();
                var response = await c.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    var searchResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(searchResult);
                }
                else
                {
                    throw new Exception($"Алоқада хато => {response.StatusCode} {response.ReasonPhrase}");
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }

        }
    }
}
