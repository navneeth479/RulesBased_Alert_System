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
        private ObservableCollection<Patient> _newpatients=new ObservableCollection<Patient>();
        private ICommand _submitCommand;
        private ICommand _closeCommand;
        private ICommand _resetCommand;
        private Client clt = new Client();


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
