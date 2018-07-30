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
    public class PilotViewModel : ViewModelBase
    {
        private PilotService _service;
        private readonly IDialogService _dialogService;

        private Pilot _selectedPilot;
        private ObservableCollection<Pilot> _Pilots;
        public bool DetailVisible { get; set; } = false;


        public PilotViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new PilotService();

            AddCommand = new RelayCommand(AddPilot);
            UpdateCommand = new RelayCommand(UpdatePilot);
            DeleteCommand = new RelayCommand(DeletePilot);

            DownloadData();
        }

        public ObservableCollection<Pilot> Pilots
        {
            get { return _Pilots; }
            set
            {
                _Pilots = value;
                RaisePropertyChanged(() => Pilots);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Pilots = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public Pilot SelectedPilot
        {
            get { return _selectedPilot; }
            set
            {
                _selectedPilot = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
                RaisePropertyChanged(() => SelectedPilot);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddPilot()
        {
            try
            {
                var result = await _service.AddAsync(SelectedPilot);
                Pilots.Insert(0, result);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdatePilot()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedPilot);
                Pilots.Remove(SelectedPilot);
                Pilots.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeletePilot()
        {
            try
            {
                await _service.DeleteAsync(SelectedPilot.Id);
                Pilots.Remove(SelectedPilot);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}