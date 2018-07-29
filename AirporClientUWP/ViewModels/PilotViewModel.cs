using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
            DownloadData();

            AddCommand = new RelayCommand(AddPilot);
            UpdateCommand = new RelayCommand(UpdatePilot);
            DeleteCommand = new RelayCommand(DeletePilot);
        }

        private async Task DownloadData()
        {
            Pilots = await _service.GetAllAsync();
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

        private async void AddPilot()
        {
            var result = await _service.AddAsync(SelectedPilot);
            Pilots.Insert(0, result);
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdatePilot()
        {
            var resultItem = await _service.UpdateAsync(SelectedPilot);
            Pilots.Remove(SelectedPilot);
            Pilots.Insert(0, resultItem);
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeletePilot()
        {
            await _service.DeleteAsync(SelectedPilot.Id);
            Pilots.Remove(SelectedPilot);
        }
    }
}