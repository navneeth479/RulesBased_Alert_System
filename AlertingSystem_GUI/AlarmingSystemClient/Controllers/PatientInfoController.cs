using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AlertingSystem_LoginPage.Models;
using Newtonsoft.Json;

namespace AlarmingSystemClient.Controllers
{
    public class PatientInfoController : Controller
    {
        // GET: PatientInfo
        private HttpClient client;
        string url = "http://localhost:56294/api/NurseStation/GetPatient";

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public PatientInfoController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public class Foo
        {
            private ObservableCollection<Patient> _fooPatients = new ObservableCollection<Patient>();
            public ObservableCollection<Patient> FooEmployees
            {
                get { return _fooPatients; }
                set { _fooPatients = value; }
            }
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(responseData);

                return View(Employees);
            }
            return View("Error");
        }

    }
}