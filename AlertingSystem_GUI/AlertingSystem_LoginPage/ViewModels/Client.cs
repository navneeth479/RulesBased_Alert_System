using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AlertingSystem_LoginPage.Models;
using Newtonsoft.Json;

namespace AlertingSystem_LoginPage.ViewModels
{
    public class Client
    {
        

        public ObservableCollection<Patient> GetAllPatients()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56294/");
            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("api/NurseStation/GetPatient/").Result;
            string s = response.Content.ReadAsStringAsync().Result;
            var output = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(s);
            return output;
        }

        public Patient GetPatientBasedOnBed(int bed)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56294/api/NurseStation/GetPatientBasedOnBed/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(bed.ToString()).Result;
            string s = response.Content.ReadAsStringAsync().Result;
            var output = JsonConvert.DeserializeObject<Patient>(s);
            return output;

        }
        public Patient GetPatient(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56294/api/NurseStation/GetSpecificPatient/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(id).Result;
            string s = response.Content.ReadAsStringAsync().Result;
            var output = JsonConvert.DeserializeObject<Patient>(s);
            return output;
        }

        public void RegisterPatient(Patient patient)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56294/");
            var myContent = JsonConvert.SerializeObject(patient);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("api/NurseStation/register", byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
               return;
            }
            
        }

        public void UpdatePatient(string id, Patient patient)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56294/api/NurseStation/UpdatePatientRecord/");
            var myContent = JsonConvert.SerializeObject(patient);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync(id.ToString(), byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                return;
            }

        }
    }
}
