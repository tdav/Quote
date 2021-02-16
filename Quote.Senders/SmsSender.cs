using Microsoft.Extensions.Configuration;
using Quote.Senders.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Senders
{   
    public class SmsSender : ISender
    {
        private readonly IConfiguration conf;

        public SmsSender(IConfiguration configuration)
        {
            conf = configuration;
        }

        public async Task<bool> SendAsync(object value)
        {
            SmsModel sm = value as SmsModel; 

            using (var client = new HttpClient())
            {
                var url = conf.GetSection("SmsSender:Url").Value;
                var path = conf.GetSection("SmsSender:Path").Value;
                var login = conf.GetSection("SmsSender:Login").Value;
                var passw = conf.GetSection("SmsSender:Password").Value;
                var text = conf.GetSection("SmsSender:Text").Value;

                sm.mes = string.Format(text, sm.mes);

                var ss = new SmsSenderModel(sm);

                var authInfo = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{passw}")));

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ss), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Authorization = authInfo;
                var res = await client.PostAsync(path, content);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
