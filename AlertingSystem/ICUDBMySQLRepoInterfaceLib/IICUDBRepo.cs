using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using PatientsDBAccess;
namespace ICUDBMySQLRepoInterfaceLib
{
    public interface IICUDBRepo
    {
        void AdmitPatient(string id,int bedno);
        void DischargePatient(string id,int bedno);
        List<ICUStatu> GetPatient();
        List<int> GetVitals(string id);
        ICUStatu GetSpecificPatient(string id);
        ICUStatu GetPatientBasedOnBed(int id);
        ICUStatu AddPatient(ICUStatu record);
        ICUStatu UpdateSpo2(string id, int spo2);
        ICUStatu UpdatePulse(string id, int pulse);
        ICUStatu UpdateTemp(string id, int temp);
        ICUStatu UpdatePatientStatus(string id, ICUStatu updatestatus);


    }
}
