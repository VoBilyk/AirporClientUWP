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
    public class CrewViewModel : ViewModelBase
    {
        private CrewService _service;
        private readonly IDialogService _dialogService;
        
        private Crew _selectedCrew;
        private ObservableCollection<Crew> _Crews;
        public bool DetailVisible { get; set; } = false;


        public CrewViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new CrewService();

            AddCommand = new RelayCommand(AddCrew);
            UpdateCommand = new RelayCommand(UpdateCrew);
            DeleteCommand = new RelayCommand(DeleteCrew);

            DownloadData();
        }

        public ObservableCollection<Crew> Crews
        {
            get { return _Crews; }
            set
            {
                _Crews = value;
                RaisePropertyChanged(() => Crews);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Crews = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public Crew SelectedCrew
        {
            get { return _selectedCrew; }
            set
            {
                _selectedCrew = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}