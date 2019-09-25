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

        [Route("api/NurseStation/GetPatientBasedOnBed/{id}")]
        [HttpGet]
        public HttpResponseMessage GetPatientBasedOnBed(int id)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _db.GetPatientBasedOnBed(id));
            }



            catch (Exception ex)
            { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }
        }

        [Route("api/NurseStation/Getvitals/{id}")]
        [HttpGet]
        public HttpResponseMessage GetVitals(string id)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _db.GetVitals(id));
            }



            catch (Exception ex)
            { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); }
        }

        [Route("api/NurseStation/UpdatePatientRecord/{id}")]
        [HttpPut]
        public HttpResponseMessage Put(string id, [FromBody] ICUStatu patientstatus)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();
            var entity = _db.UpdatePatientStatus(id, patientstatus);

            if (entity == null)
            { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "patient with id" + id + "Is not found"); }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "models details successfully updated ");
            }

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

        [Route("api/NurseStation/spo2/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateSpo2(string id,[FromBody] int spo2)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            var entity= _db.UpdateSpo2(id, spo2);

            if (entity == null)
            { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "patient with id" + id + "Is not found"); }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "models details successfully updated ");
            }
        }

        [Route("api/NurseStation/pulse/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdatePulse(string id,[FromBody] int pulse)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            var entity= _db.UpdatePulse(id, pulse);
            if (entity == null)
            { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "patient with id" + id + "Is not found"); }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "models details successfully updated ");
            }
        }

        [Route("api/NurseStation/tempt/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateTemp(string id,[FromBody] int temp)
        {
            _db = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();

            var entity=_db.UpdateTemp(id, temp);
            if (entity == null)
            { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "patient with id" + id + "Is not found"); }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "models details successfully updated ");
            }
        }



    }
}
