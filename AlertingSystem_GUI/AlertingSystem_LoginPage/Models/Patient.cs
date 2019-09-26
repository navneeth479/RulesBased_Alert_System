using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertingSystem_LoginPage.Models
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientId { get; set; }
        public string PatientDob { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }
        public float PatientHeight { get; set; }
        public float PatientWeight { get; set; }
        public string DoctorAssigned { get; set; }
        public string ReasonAdmitted { get; set; }
        public int bedNo { get; set; }
        public string AdmissionDate { get; set; }
        public string OtherMedications { get; set; }
        public string PatientStatus { get; set; }
        public int SPO2 { get; set; }
        public int PulseRate { get; set; }
        public int Temperature { get; set; }
        public string SPO2Status { get; set; }
        public string PulseRateStatus { get; set; }
        public string TemperatureStatus { get; set; }
        public List<int> SPO2List { get; set; }
        public List<int> PulseList { get; set; }
        public List<int> TempList { get; set; }
        public List<DateTime> TimeList { get; set; }

        public Patient()
        {
            SPO2List=new List<int>();
            PulseList=new List<int>();
            TempList=new List<int>();
            TimeList=new List<DateTime>();
        }
    }
}
