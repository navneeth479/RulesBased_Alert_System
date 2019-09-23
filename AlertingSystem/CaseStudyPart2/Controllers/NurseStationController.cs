using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ICUDBMySQLRepoInterfaceLib;
using DataModelsLib;
using PatientsDBAccess;
using Spo2CheckerInterfaceLib;
using PulseCheckerInteraceLib;
using TempCheckerInterfaceLib;
using Unity;
using Spo2CheckerLib;
using PulseCheckerLib;
using TemperatureCheckerLib;
namespace CaseStudyPart2.Controllers
{
    public class NurseStationController : ApiController
    {
        ISpo2Checker _spo2Checker;
        IPulseChecker _pulseChecker;
        ITempChecker _tempChecker;
        ICUDBMySQLRepoInterfaceLib.IICUDBRepo _db;
        UnityContainer _con = new UnityContainer();
        public NurseStationController()
        {
            _con.RegisterType<ISpo2Checker, Spo2Checker>();
            _con.RegisterType<IPulseChecker, PulseChecker>();
            _con.RegisterType<ITempChecker, TempChecker>();
            _con.RegisterType<ICUDBMySQLRepoInterfaceLib.IICUDBRepo, ICUDBMySQLRepository.IcuDbMySqlRepo>();
        }

        
        [Route("api/PatientAdmitter/{id}/{bedno}")]
        [HttpGet]
        public void AdmitPatient(string id,int bedno)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();
            _db.AdmitPatient(id,bedno);
        }
        [Route("api/PatientDischarger/{id}/{bedno}")]
        [HttpGet]
        public void DischargePatient(string id,int bedno)
        { 
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();
            
            _db.DischargePatient(id,bedno);
        }

        [Route("api/NurseStation/GetPatient")]
        [HttpGet]
        public HttpResponseMessage GetPatient()
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _db.GetPatient());
            }



            catch (Exception ex)
            { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }
        }

        [Route("api/NurseStation/GetSpecificPatient/{id}")]
        [HttpGet]
        public HttpResponseMessage GetSpecificPatient(string id)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _db.GetSpecificPatient(id));
            }



            catch (Exception ex)
            { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }
        }

        [Route("api/NurseStation/register")]
        [HttpPost]//Adding customer details

        public HttpResponseMessage Post([FromBody] ICUStatu registerpatient)

        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            var entity = _db.AddPatient(registerpatient);

            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not admitted ");
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, "Patient admitted  ");

            }
        }

        [Route("api/NurseStation/UpdateVitals/{id}")]
        [HttpPut]
        public void UpdateVitals(string id, Patient vitals)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            _db.UpdateVitals(id, vitals);
        }



    }
}
