using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class PilotViewModel : ViewModelBase
    {
        private PilotService _service;
        private Pilot _selectedPilot;

        public ObservableCollection<Pilot> Pilots { get; private set; }
        
        public PilotViewModel()
        {
            _service = new PilotService();

            AddCommand = new RelayCommand(AddPilot);
            Pilots = _service.GetAllAsync().Result;

            //search();
        }

        
        public Pilot SelectedPilot
        {
            get { return _selectedPilot; }
            set
            {
                _selectedPilot = value;
                RaisePropertyChanged(() => SelectedPilot);
            }
        }

        public ICommand AddCommand { get; set; }

        private void AddPilot()
        {
            var pilot = this.SelectedPilot;
        }

        //private void search()
        //{
        //    Students.Clear();
        //    if (string.IsNullOrWhiteSpace(SearchFilter))
        //    {
        //        foreach (var student in _academy.GetAllStudents())
        //        {
        //            Students.Add(student);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var student in _academy.GetByStudentName(SearchFilter))
        //        {
        //            Students.Add(student);
        //        }
        //        foreach (var student in _academy.GetByStudentCity(SearchFilter))
        //        {
        //            Students.Add(student);
        //        }
        //    }
        //}
    }
}