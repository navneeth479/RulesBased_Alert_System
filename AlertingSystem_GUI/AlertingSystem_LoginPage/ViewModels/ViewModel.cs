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
//using NewtonSoft.Json;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Controls;
using AlertingSystem_LoginPage.Models;

namespace AlertingSystem_LoginPage.ViewModels
{
    class ViewModel:ViewModelBase
    {
        private Patient _patient;
        private Patient _selectedPatient;
        private ObservableCollection<Patient> _patients =new ObservableCollection<Patient>();
        private ICommand _submitCommand;
        private ICommand _closeCommand;
        private ICommand _resetCommand;
        private ICommand _dischargeCommand;
        private ICommand _startexamCommand;

        private Client clt = new Client();

        //public Patient GetPatientBasedOnBed(int id)
        //{
        //    Patients = new ObservableCollection<Patient>();
        //    Patients.CollectionChanged += new NotifyCollectionChangedEventHandler(Patients_CollectionChanged);
        //    _patients = clt.GetAllPatients();
        //    return _patients.FirstOrDefault(e => e.bedNo == id);
        //}

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
            get => _patients.FirstOrDefault(e => e.bedNo == 6);
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
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => this.CloseWindow(),
                        null);
                }
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
                if (_dischargeCommand == null)
                {
                    _dischargeCommand = new RelayCommand(param => DischargePatient(param),
                        null);
                }
                return _dischargeCommand;
            }

        }

        private void DischargePatient(object param)
        {
            var patient = param as Patient;
            patient.PatientStatus = "Discharged";
            Patients.Remove(patient);
            clt.UpdatePatient(patient.PatientId, patient);
            NotifyPropertyChanged("SelectedPatient");
        }

        private void StartExam(object param)
        {

            var patient = param as Patient;
            patient.PatientStatus = "Active";
            patient.PulseRate = 1;
            patient.SPO2 = 1;
            patient.Temperature = 1;
            NotifyPropertyChanged("SelectedPatient");
            clt.UpdatePatient(patient.PatientId, patient);
        }

        public ViewModel()
        {
            Patient=new Patient();
            Patients=new ObservableCollection<Patient>();
            Patients.CollectionChanged +=new NotifyCollectionChangedEventHandler(Patients_CollectionChanged);
            _patients = clt.GetAllPatients();
            
        }
        void Patients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Patients");
            NotifyPropertyChanged("BedNo6");
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

        
        
    }
}
