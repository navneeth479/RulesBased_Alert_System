using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Controls;
using AlertingSystem_LoginPage.Models;
using System.Windows.Threading;
using VitalSimulatorMangerLib;

namespace AlertingSystem_LoginPage.ViewModels
{
    class ViewModel:ViewModelBase
    {
        private Patient _patient;
        private Patient _selectedPatient;
        private ObservableCollection<Patient> _patients;
        private ICommand _resetAlarmCommand;
        private ICommand _submitCommand;
        private ICommand _closeCommand;
        private ICommand _resetCommand;
        private ICommand _dischargeCommand;
        private ICommand _startexamCommand;

        private readonly Client clt = new Client();

        private ObservableCollection<int> _spo2data;
        private ObservableCollection<int> _tempdata;
        private ObservableCollection<int> _pulsedata;
        private ObservableCollection<DateTime> _timedata;

        public ObservableCollection<DateTime> TimeData
        {
            get => _timedata;
            set
            {
                _timedata = value;
                NotifyPropertyChanged("TimeData");
            }
        }

        public ObservableCollection<int> SPO2Data
        {
            get => _spo2data;
            set
            {
                _spo2data = value;
                NotifyPropertyChanged("SPO2Data");
            }
        }
        public ObservableCollection<int> PulseData
        {
            get => _pulsedata;
            set
            {
                _pulsedata = value;
                NotifyPropertyChanged("SPO2Data");
            }
        }
        public ObservableCollection<int> TempData
        {
            get => _tempdata;
            set
            {
                _tempdata = value;
                NotifyPropertyChanged("SPO2Data");
            }
        }
        public Patient BedNo1
        {
            get => clt.GetPatientBasedOnBed(1);
            set
            {
               _patient = value;
                NotifyPropertyChanged("BedNo1");


            }
        }
        public Patient BedNo2
        {
            get => clt.GetPatientBasedOnBed(2);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo2");

            }
        }
        public Patient BedNo3
        {
            get => clt.GetPatientBasedOnBed(3);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo3");

            }
        }
        public Patient BedNo4
        {
            get => clt.GetPatientBasedOnBed(4);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo4");

            }
        }
        public Patient BedNo5
        {
            get => clt.GetPatientBasedOnBed(5);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo5");

            }
        }
        public Patient BedNo6
        {
            get => clt.GetPatientBasedOnBed(6);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo6");
                NotifyPropertyChanged("Patients");

            }
        }
        public Patient BedNo7
        {
            get => clt.GetPatientBasedOnBed(7);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo7");

            }
        }
        public Patient BedNo8
        {
            get => clt.GetPatientBasedOnBed(8);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo8");

            }
        }
        public Patient BedNo9
        {
            get => clt.GetPatientBasedOnBed(9);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo9");

            }
        }
        public Patient BedNo10
        {
            get => clt.GetPatientBasedOnBed(10);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo10");

            }
        }
        public Patient BedNo11
        {
            get => clt.GetPatientBasedOnBed(11);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo11");

            }
        }
        public Patient BedNo12
        {
            get => clt.GetPatientBasedOnBed(12);
            set
            {
                _patient = value;
                NotifyPropertyChanged("BedNo12");

            }
        }
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                NotifyPropertyChanged("SelectedPatient");
            }
        }
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                NotifyPropertyChanged("Patient");
                NotifyPropertyChanged("Patients");
            }
        }

       

        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                NotifyPropertyChanged("Patients");
            }
        }



        public ICommand ResetAlarmCommand
        {
            get
            {
                if (_resetAlarmCommand == null)
                {
                    _resetAlarmCommand = new RelayCommand(param => this.ResetAlarm(param),
                        null);
                }
                return _resetAlarmCommand;
            }
        }


        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(param => this.Reset(),
                        null);
                }
                return _resetCommand;
            }
        }
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new RelayCommand(param => this.Submit(),
                        null);
                    
                }
                return _submitCommand;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                    _closeCommand = new RelayCommand(param => this.CloseWindow(),
                        null);
                
                return _closeCommand;
            }
            
        }

        public ICommand StartExamCommand
        {
            get
            {
                if (_startexamCommand == null)
                {
                    _startexamCommand = new RelayCommand(param => StartExam(param),
                        null);
                }
                return _startexamCommand;
            }
        }

        public ICommand DischargeCommand
        {
            get
            {
                return _dischargeCommand ?? (_dischargeCommand = new RelayCommand(param => DischargePatient(param),
                           null));
            }

        }

        private void DischargePatient(object param)
        {
            var patient = param as Patient;
            patient.PatientStatus = "Discharged";
            patient.BedNum = 0;
            Patients.Remove(patient);
            clt.UpdatePatient(patient.PatientId, patient);
            NotifyPropertyChanged("SelectedPatient");

        }
        

        private void VitalSimulator(Patient p)
        {
            string id = p.PatientId;
            VitalSimulatorManager vitalSimulator = new VitalSimulatorManager();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (obj, e) => vitalSimulator.StartSimulation(id);
            worker.RunWorkerAsync();
            p.PatientStatus = "Active";
            List<string> conditionList;
            List<int> vitalsList;
            for (int i = 0; i < 100; i++)
            {
                vitalsList = clt.GetVitals(id);
                conditionList = clt.GetPatientCondition(id);
                p.SPO2 = vitalsList[0];
                p.PulseRate = vitalsList[1];
                p.Temperature = vitalsList[2];
                p.SPO2Status = conditionList[0];
                p.PulseRateStatus = conditionList[1];
                p.TemperatureStatus = conditionList[2];
                Thread.Sleep(5000);
            }
        }

        private void StartExam(object param)
        {
            
            _patient = param as Patient;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (obj, e) => VitalSimulator(_patient);
            
            worker.RunWorkerAsync();
        }

        

        public ViewModel()
        {
            Patient=new Patient();
            Patients=new ObservableCollection<Patient>();
            Patients.CollectionChanged +=new NotifyCollectionChangedEventHandler(Patients_CollectionChanged);
            _patients = clt.GetAllPatients();
            SPO2Data = new ObservableCollection<int>();
            PulseData = new ObservableCollection<int>();
            TempData = new ObservableCollection<int>();
            TimeData = new ObservableCollection<DateTime>();
            PulseData.CollectionChanged += new NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            TempData.CollectionChanged += new NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            PulseData.CollectionChanged += new NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            TimeData.CollectionChanged += new NotifyCollectionChangedEventHandler(Data_CollectionChanged);

            

        }

        void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("SelectedPatient");
        }

        void Patients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Patients");
        }

        private void Submit()
        {
            Patient.PatientStatus = "Admitted";
            Patient.SPO2 = 0;
            Patient.Temperature = 0;
            Patient.PulseRate = 0;
            clt.RegisterPatient(Patient);
            Patients.Insert(0, clt.GetPatient(Patient.PatientId));
            Patient =new Patient();
        }
        private void ResetAlarm(object param)
        {
            _patient = param as Patient;
            _patient.PatientStatus = "Active";
        }

        private void Reset()
        {
            Patient = new Patient();
        }

        private void CloseWindow()
        {
            if (MessageBox.Show("Are you sure you want to close this Application?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Close();
            }
             
        }

        //Task<string> DoWork()
        //{
        //    return Task.Run(() =>
        //    {

        //        return GetRandomNumberAsString();
        //    });
        //}



    }
}
