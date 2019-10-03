using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICUDBMySQLRepository;
using PatientsDBAccess;

namespace ICUDBMySqlRepoTestsLib
{
    [TestClass]
    public class ICUDBMySQLRepoTest
    {
        public ICUDBMySQLRepoTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetPatient()
        {
            PatientsDBAccess.ICUStatu obj = new ICUStatu();
            List<ICUStatu> patients1 = new List<ICUStatu>();
            List<ICUStatu> patients2 = new List<ICUStatu>();
            obj.FirstName = "Pranay";
            obj.LastName = "Kumar";
            obj.PatientDob = "07/11/1996";
            obj.PatientGender = "Male";
            obj.PatientHeight = 179;
            obj.DoctorAssigned = "Navaneeth";
            obj.bedNo = 5;
            obj.PatientWeight = 63;
            obj.ReasonAdmitted = "yo";
            obj.OtherMedications = "yo";
            obj.PatientId = "1";
            obj.AdmissionDate = "yo";
            patients1.Add(obj);
            IcuDbMySqlRepo obj2 = new IcuDbMySqlRepo();
            patients2 = obj2.GetPatient();
            Assert.AreEqual(patients1[0].FirstName, patients2[0].FirstName);
            Assert.AreEqual(patients1[0].PatientGender, patients2[0].PatientGender);
            Assert.AreEqual(patients1[0].PatientHeight, patients2[0].PatientHeight);
            Assert.AreEqual(patients1[0].LastName, patients2[0].LastName);
            Assert.AreEqual(patients1[0].PatientDob, patients2[0].PatientDob);
            Assert.AreEqual(patients1[0].PatientId, patients2[0].PatientId);
            Assert.AreEqual(patients1[0].ReasonAdmitted, patients2[0].ReasonAdmitted);
            Assert.AreEqual(patients1[0].PatientWeight, patients2[0].PatientWeight);

        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        [TestMethod]
        public void TestAddPatient()
        {
            PatientsDBAccess.ICUStatu obj = new ICUStatu();
            List<ICUStatu> patients1 = new List<ICUStatu>();
            List<ICUStatu> patients2 = new List<ICUStatu>();
            Random r = new Random();
            obj.FirstName = RandomString(5, true);
            obj.LastName = RandomString(5, true); ;
            obj.PatientDob = "07/11/96";
            obj.PatientGender = "Male";
            obj.PatientHeight = r.Next();
            obj.DoctorAssigned = RandomString(5, true); ;
            obj.bedNo = 5;
            obj.PatientWeight = 63;
            obj.ReasonAdmitted = "yo";
            obj.OtherMedications = "yo";
            obj.PatientId = RandomString(5, true); ;
            obj.AdmissionDate = "yo";

            IcuDbMySqlRepo obj2 = new IcuDbMySqlRepo();
            patients2 = obj2.GetPatient();
            int initialcount = patients2.Count;
            obj2.AddPatient(obj);
            patients2 = obj2.GetPatient();
            int finalcount = patients2.Count;
            Assert.AreEqual(initialcount + 1, finalcount);

        }

        [TestMethod]
        public void TestGetSpecificPatient()
        {
            PatientsDBAccess.ICUStatu obj = new ICUStatu();
            List<ICUStatu> patients1 = new List<ICUStatu>();
            List<ICUStatu> patients2 = new List<ICUStatu>();
            obj.FirstName = "Pranay";
            obj.LastName = "Kumar";
            obj.PatientDob = "07/11/1996";
            obj.PatientGender = "Male";
            obj.PatientHeight = 179;
            obj.DoctorAssigned = "Navaneeth";
            obj.bedNo = 5;
            obj.PatientWeight = 63;
            obj.ReasonAdmitted = "yo";
            obj.OtherMedications= "yo";
            obj.PatientId = "1";
            obj.AdmissionDate = "yo";
            IcuDbMySqlRepo obj2 = new IcuDbMySqlRepo();
            PatientsDBAccess.ICUStatu obj1 = new ICUStatu();
            obj1 = obj2.GetSpecificPatient("1");
            Assert.AreEqual(obj.FirstName, obj1.FirstName);
            Assert.AreEqual(obj.PatientGender, obj1.PatientGender);
            Assert.AreEqual(obj.PatientHeight, obj1.PatientHeight);
            Assert.AreEqual(obj.LastName, obj1.LastName);
            Assert.AreEqual(obj.PatientDob, obj1.PatientDob);
            Assert.AreEqual(obj.PatientId, obj1.PatientId);
            Assert.AreEqual(obj.ReasonAdmitted, obj1.ReasonAdmitted);
            Assert.AreEqual(obj.PatientWeight, obj1.PatientWeight);
        }

    }
}
