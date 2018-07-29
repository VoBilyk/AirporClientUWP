using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class StewardessViewModel : ViewModelBase
    {
        private StewardessService _service;
        private Stewardess _selectedStewardess;
        private ObservableCollection<Stewardess> _stewardesses;
        

        public StewardessViewModel()
        {
            _service = new StewardessService();

            DownloadData();

            AddCommand = new RelayCommand(AddStewardess);
            UpdateCommand = new RelayCommand(UpdateStewardess);
            DeleteCommand = new RelayCommand(DeleteStewardess);
        }

        public ObservableCollection<Stewardess> Stewardesses
        {
            get { return _stewardesses; }
            set
            {
                _stewardesses = value;
                RaisePropertyChanged(() => Stewardesses);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Stewardesses = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }


        public Stewardess SelectedStewardess
        {
            get { return _selectedStewardess; }
            set
            {
                _selectedStewardess = value;
                RaisePropertyChanged(() => SelectedStewardess);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddStewardess()
        {
            try
            {
                var result = await _service.AddAsync(SelectedStewardess);
                Stewardesses.Insert(0, result);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateStewardess()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedStewardess);
                Stewardesses.Remove(SelectedStewardess);
                Stewardesses.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteStewardess()
        {
            try
            {
                await _service.DeleteAsync(SelectedStewardess.Id);
                Stewardesses.Remove(SelectedStewardess);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}