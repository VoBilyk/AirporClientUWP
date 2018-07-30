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
    public class AeroplaneViewModel : ViewModelBase
    {
        private AeroplaneService _service;
        private readonly IDialogService _dialogService;

        private Aeroplane _selectedAeroplane;
        private ObservableCollection<Aeroplane> _Aeroplanes;

        public bool DetailVisible { get; set; } = false;
        
        public AeroplaneViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new AeroplaneService();

            AddCommand = new RelayCommand(AddAeroplane);
            UpdateCommand = new RelayCommand(UpdateAeroplane);
            DeleteCommand = new RelayCommand(DeleteAeroplane);

            DownloadData();
        }

        public ObservableCollection<Aeroplane> Aeroplanes
        {
            get { return _Aeroplanes; }
            set
            {
                _Aeroplanes = value;
                RaisePropertyChanged(() => Aeroplanes);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Aeroplanes = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public Aeroplane SelectedAeroplane
        {
            get { return _selectedAeroplane; }
            set
            {
                _selectedAeroplane = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}