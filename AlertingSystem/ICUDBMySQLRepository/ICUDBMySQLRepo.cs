using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using ICUDBMySQLRepoInterfaceLib;
using Newtonsoft.Json;
using PatientsDBAccess;

namespace ICUDBMySQLRepository
{
    public class IcuDbMySqlRepo : IICUDBRepo
    {
        
        public void AdmitPatient(string id,int bedno)
        {
           
            SqlConnection con = new SqlConnection(@"Data Source=YY175268\SQLEXPRESS;Initial Catalog=AlarmingSystemDB;Integrated Security=True");
            con.Open();

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM ICUSTATE", con);
            var reader2 = cmd2.ExecuteReader();
            int bedsFree=10, bedsOccupied=0;
            while (reader2.Read())
            {
                bedsFree = (int)reader2[5];
                bedsOccupied = (int)reader2[4];
            }
            reader2.Close();
            
            //Admit Patient 

            List<Patient> patients;

            var reader1 = new StreamReader("PatientData.json");
            string content = reader1.ReadToEnd();

            var data = JsonConvert.DeserializeObject(content, typeof(List<Patient>));
            if (data is List<Patient>)
                patients = data as List<Patient>;
                
            else
                throw new ArgumentException("Failed to load JSON file");
            con.Close();
            foreach (var patient in patients)
            {
                if (patient.PatientId == id)
                {
                   
                    con.Open();
                    SqlCommand cmd = new SqlCommand($@"INSERT INTO ICUSTATE VALUES ('"+ id+"', "+patient.Spo2+", "+patient.PulseRate+", "+patient.Temperature+
                                                    ", " + bedsOccupied+ ", " + bedsFree+ ", " + bedno+");",con);
                    
                    string query = @"UPDATE ICUSTATE SET BEDSOCCUPIED=BEDSOCCUPIED+1 
                            ";
                    bedsOccupied++;
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    
                    SqlCommand cmd3 = new SqlCommand(@"UPDATE ICUSTATE SET BEDSFREE=BEDSFREE-1
                            ", con);
                    bedsFree--;
                    
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    
                }
            }
            
            if (ICU.BedOccupancy[bedno])
                return;
            ICU.BedOccupancy[bedno] = true;


            SqlCommand cmd4 = new SqlCommand($"UPDATE BEDS SET OCCUPIED='Y' WHERE BEDNO={bedno}", con);
            cmd4.ExecuteNonQuery();
            reader1.Dispose();
            con.Close();
        }

        public void DischargePatient(string id ,int bedno)
        { 
                if ( ICU.BedOccupancy[bedno] )
                    return;
                ICU.BedOccupancy[bedno] = false;
                SqlConnection con =
                    new SqlConnection(@"Data Source=YY175268\SQLEXPRESS;Initial Catalog=AlarmingSystemDB;Integrated Security=True");
                string query = @"UPDATE ICUSTATE SET BEDSOCCUPIED=BEDSOCCUPIED-1 
                            ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand cmd1 = new SqlCommand($"UPDATE BEDS SET OCCUPIED='N' WHERE BEDNO={bedno}", con);
                SqlCommand cmd3 = new SqlCommand($@"DELETE FROM ICUSTATE WHERE ICUSTATE.PATIENTID='{id}'",
                    con);
                SqlCommand cmd2 = new SqlCommand(@"UPDATE ICUSTATE SET BEDSFREE=BEDSFREE+1
                            ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            
        }
        
        public List<ICUStatu> GetPatient()
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            { return entities.ICUStatus.Where(e=>e.PatientStatus.ToLower()!="discharged").ToList(); }
        }

        
        
        public List<int> GetVitals(string id)
        {
            List<int> vitalsList = new List<int>();
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.FirstOrDefault(e => e.PatientId == id);
                vitalsList.Add((int)entity.SPO2);
                vitalsList.Add((int)entity.PulseRate);
                vitalsList.Add((int)entity.Temperature);

            }
            return vitalsList;


        }
        public ICUStatu UpdateSpo2(string id, int spo2)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.FirstOrDefault(e => e.PatientId == id);
                entity.SPO2 = spo2;
                entities.SaveChanges();

                return entity;
            }
        }
        public ICUStatu UpdatePulse(string id, int pulse)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.FirstOrDefault(e => e.PatientId == id);
                entity.PulseRate = pulse;
                entities.SaveChanges();

                return entity;
            }
        }
        public ICUStatu UpdateTemp(string id, int temp)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.FirstOrDefault(e => e.PatientId == id);
                entity.Temperature = temp;
                entities.SaveChanges();
                return entity;
            }
        }

        public ICUStatu GetPatientBasedOnBed(int id)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.Where(e => e.bedNo == id);
                return entity.FirstOrDefault();
            }
        }
        public ICUStatu UpdatePatientStatus(string id, ICUStatu updatestatus)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {

                var entity = entities.ICUStatus.FirstOrDefault(e => e.PatientId == id);
                entity.FirstName = updatestatus.FirstName;
                entity.LastName = updatestatus.LastName;
                entity.AdmissionDate = updatestatus.AdmissionDate;
                entity.DoctorAssigned = updatestatus.DoctorAssigned;
                entity.PatientAge = updatestatus.PatientAge;
                entity.PatientGender = updatestatus.PatientGender;
                entity.PatientHeight = updatestatus.PatientHeight;
                entity.PatientWeight = updatestatus.PatientWeight;
                entity.PatientStatus = updatestatus.PatientStatus;
                entity.SPO2 = updatestatus.SPO2;
                entity.Temperature = updatestatus.Temperature;
                entity.PulseRate = updatestatus.PulseRate;
                entity.bedNo = updatestatus.bedNo;
                entity.OtherMedications = updatestatus.OtherMedications;
                entity.PatientDob = updatestatus.PatientDob;
                entities.SaveChanges();
                return entity;
            }
        }

        public ICUStatu AddPatient(ICUStatu record)
        {
            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity = entities.ICUStatus.Add(record);
                entities.SaveChanges();
                return entity;

            }
        }
        public ICUStatu GetSpecificPatient(string id)
        {


            using (ICUDBEntities1 entities = new ICUDBEntities1())
            {
                var entity=entities.ICUStatus.Where(e => e.PatientId.ToLower() == id.ToLower());
                return entity.FirstOrDefault();
            }
        }
    }
}
