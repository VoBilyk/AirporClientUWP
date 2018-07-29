using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class FlightViewModel : ViewModelBase
    {
        private FlightService _service;
        private Flight _selectedFlight;
        private ObservableCollection<Flight> _Flights;


        public FlightViewModel()
        {
            _service = new FlightService();

            AddCommand = new RelayCommand(AddFlight);
            UpdateCommand = new RelayCommand(UpdateFlight);
            DeleteCommand = new RelayCommand(DeleteFlight);

            DownloadData();
        }

        public ObservableCollection<Flight> Flights
        {
            get { return _Flights; }
            set
            {
                _Flights = value;
                RaisePropertyChanged(() => Flights);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Flights = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }


        public Flight SelectedFlight
        {
            get { return _selectedFlight; }
            set
            {
                _selectedFlight = value;
                RaisePropertyChanged(() => SelectedFlight);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddFlight()
        {
            try
            {
                var result = await _service.AddAsync(SelectedFlight);
                Flights.Insert(0, result);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateFlight()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedFlight);
                Flights.Remove(SelectedFlight);
                Flights.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteFlight()
        {
            try
            {
                await _service.DeleteAsync(SelectedFlight.Id);
                Flights.Remove(SelectedFlight);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}