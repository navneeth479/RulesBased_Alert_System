using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;
using DataModelsLib;
using PulseCheckerInteraceLib;
using Spo2CheckerInterfaceLib;
using TempCheckerInterfaceLib;
using PulseCheckerLib;
using Spo2CheckerLib;
using TemperatureCheckerLib;
namespace CaseStudyPart2.Controllers
{
    public class AlertStationController : ApiController
    {
        ISpo2Checker _spo2Checker;
        IPulseChecker _pulseChecker;
        ITempChecker _tempChecker;
        ICUDBMySQLRepoInterfaceLib.IICUDBRepo _icu;
        readonly UnityContainer _con = new UnityContainer();
        public AlertStationController()
        {
            _con.RegisterType<ISpo2Checker, Spo2Checker>();
            _con.RegisterType<IPulseChecker, PulseChecker>();
            _con.RegisterType<ITempChecker, TempChecker>();
            _con.RegisterType<ICUDBMySQLRepoInterfaceLib.IICUDBRepo, ICUDBMySQLRepository.IcuDbMySqlRepo>();
        }
        
        
        [Route("api/PatientCondition/{id}")]
        [HttpGet]
        public List<string> GeneratePatientCondition(string id)
        {
            _pulseChecker = _con.Resolve<IPulseChecker>();
            _spo2Checker = _con.Resolve<ISpo2Checker>();
            _tempChecker = _con.Resolve<ITempChecker>();
            _icu = _con.Resolve<ICUDBMySQLRepoInterfaceLib.IICUDBRepo>();
            List<string> localList = new List<string>();
            List<int> vitalsList;
            int spo2 , pulseRate,temp;
            vitalsList=_icu.GetVitals(id);
            spo2 = vitalsList[0];
            pulseRate = vitalsList[1];
            temp = vitalsList[2];
            if (_spo2Checker.IsAlertSpo2(spo2))
            {
                _icu.AlertStatus(id);
            }

            if (_pulseChecker.IsAlertPulse(pulseRate))
            {
                _icu.AlertStatus(id);
            }

            if (_tempChecker.IsAlertTemp(temp))
            {
                _icu.AlertStatus(id);
            }
            localList.Add(_icu.GetSPO2Description((int)_spo2Checker.CheckSpo2(spo2)));
            localList.Add(_icu.GetPulseDescription((int)_pulseChecker.CheckPulse(pulseRate)));
            localList.Add(_icu.GetTempDescription((int)_tempChecker.CheckTemp(temp)));
            
            return localList;
        }

        
    }
}
