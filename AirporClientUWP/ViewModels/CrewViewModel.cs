using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class CrewViewModel : ViewModelBase
    {
        private CrewService _service;
        private Crew _selectedCrew;

        public ObservableCollection<Crew> Crews { get; private set; }

        public CrewViewModel()
        {
            _service = new CrewService();
            DownloadData();

            AddCommand = new RelayCommand(AddCrew);
            UpdateCommand = new RelayCommand(UpdateCrew);
            DeleteCommand = new RelayCommand(DeleteCrew);
        }

        private async Task DownloadData()
        {
            try
            {
                Crews = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }


        public Crew SelectedCrew
        {
            get { return _selectedCrew; }
            set
            {
                _selectedCrew = value;
                RaisePropertyChanged(() => SelectedCrew);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddCrew()
        {
            try
            {
                var result = await _service.AddAsync(SelectedCrew);
                Crews.Insert(0, result);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateCrew()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedCrew);
                Crews.Remove(SelectedCrew);
                Crews.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteCrew()
        {
            try
            {
                await _service.DeleteAsync(SelectedCrew.Id);
                Crews.Remove(SelectedCrew);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}