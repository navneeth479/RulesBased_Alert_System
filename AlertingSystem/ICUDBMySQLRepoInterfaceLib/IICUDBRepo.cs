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
       
        void ReadRecord(ref string id, ref int spo2, ref int pulse, ref double temp);
        void AdmitPatient(string id,int bedno);
        void DischargePatient(string id,int bedno);
        List<ICUStatu> GetPatient();
        ICUStatu GetSpecificPatient(string id);
        ICUStatu GetPatientBasedOnBed(int id);
        ICUStatu AddPatient(ICUStatu record);
        void UpdateVitals(string id, Patient vitals);
        ICUStatu UpdatePatientStatus(string id, ICUStatu updatestatus);


    }
}
