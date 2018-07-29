using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class AeroplaneViewModel : ViewModelBase
    {
        private AeroplaneService _service;
        private Aeroplane _selectedAeroplane;

        public ObservableCollection<Aeroplane> Aeroplanes { get; private set; }

        public AeroplaneViewModel()
        {
            _service = new AeroplaneService();
            DownloadData();

            AddCommand = new RelayCommand(AddAeroplane);
            UpdateCommand = new RelayCommand(UpdateAeroplane);
            DeleteCommand = new RelayCommand(DeleteAeroplane);
        }

        private async Task DownloadData()
        {
            try
            {
                Aeroplanes = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }


        public Aeroplane SelectedAeroplane
        {
            get { return _selectedAeroplane; }
            set
            {
                _selectedAeroplane = value;
                RaisePropertyChanged(() => SelectedAeroplane);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddAeroplane()
        {
            try
            {
                var result = await _service.AddAsync(SelectedAeroplane);
                Aeroplanes.Insert(0, result);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateAeroplane()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedAeroplane);
                Aeroplanes.Remove(SelectedAeroplane);
                Aeroplanes.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteAeroplane()
        {
            try
            {
                await _service.DeleteAsync(SelectedAeroplane.Id);
                Aeroplanes.Remove(SelectedAeroplane);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}