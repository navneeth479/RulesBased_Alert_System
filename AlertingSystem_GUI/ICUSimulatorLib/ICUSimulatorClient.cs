using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Mail;

namespace ICUSimulatorLib
{
    public class ICUSimulatorClient
    {
        private readonly string localurl = "http://localhost:56294/";


        public void UpdateSpo2(string id, int spo2)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(localurl+"api/NurseStation/spo2/");
            var myContent = JsonConvert.SerializeObject(spo2);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync(id.ToString(), byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

        }
        public void UpdatePulse(string id, int pulse)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(localurl+"api/NurseStation/pulse/");
            var myContent = JsonConvert.SerializeObject(pulse);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync(id.ToString(), byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

        }
        public void UpdateTemp(string id, int temp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(localurl+"api/NurseStation/tempt/");
            var myContent = JsonConvert.SerializeObject(temp);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync(id.ToString(), byteContent).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

        }

    }
}
