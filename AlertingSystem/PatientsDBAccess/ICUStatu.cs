//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientsDBAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ICUStatu
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientDob { get; set; }
        public Nullable<int> PatientAge { get; set; }
        public string PatientGender { get; set; }
        public Nullable<double> PatientHeight { get; set; }
        public Nullable<double> PatientWeight { get; set; }
        public string DoctorAssigned { get; set; }
        public string ReasonAdmitted { get; set; }
        public string AdmissionDate { get; set; }
        public string OtherMedications { get; set; }
        public Nullable<int> SPO2 { get; set; }
        public Nullable<int> PulseRate { get; set; }
        public Nullable<int> Temperature { get; set; }
        public string PatientStatus { get; set; }
        public int bedNo { get; set; }
    }
}