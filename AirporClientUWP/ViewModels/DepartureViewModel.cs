using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using AirporClientUWP.Models;
using AirporClientUWP.Services;


namespace AirporClientUWP.ViewModels
{
    public class DepartureViewModel : ViewModelBase
    {
        private DepartureService _service;
        private readonly IDialogService _dialogService;

        private Departure _selectedDeparture;
        private ObservableCollection<Departure> _Departures;

        public bool DetailVisible { get; set; } = false;

        public DepartureViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new DepartureService();

            AddCommand = new RelayCommand(AddDeparture);
            UpdateCommand = new RelayCommand(UpdateDeparture);
            DeleteCommand = new RelayCommand(DeleteDeparture);

            DownloadData();
        }

        public ObservableCollection<Departure> Departures
        {
            get { return _Departures; }
            set
            {
                _Departures = value;
                RaisePropertyChanged(() => Departures);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Departures = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public Departure SelectedDeparture
        {
            get { return _selectedDeparture; }
            set
            {
                _selectedDeparture = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
                RaisePropertyChanged(() => SelectedDeparture);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddDeparture()
        {
            try
            {
                var result = await _service.AddAsync(SelectedDeparture);
                Departures.Insert(0, result);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateDeparture()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedDeparture);
                Departures.Remove(SelectedDeparture);
                Departures.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteDeparture()
        {
            try
            {
                await _service.DeleteAsync(SelectedDeparture.Id);
                Departures.Remove(SelectedDeparture);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}