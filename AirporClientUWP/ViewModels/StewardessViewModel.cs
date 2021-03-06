﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class StewardessViewModel : ViewModelBase
    {
        private StewardessService _service;
        private readonly IDialogService _dialogService;

        private Stewardess _selectedStewardess;
        private ObservableCollection<Stewardess> _stewardesses;
        public bool DetailVisible { get; set; } = false;


        public StewardessViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new StewardessService();

            AddCommand = new RelayCommand(AddStewardess);
            UpdateCommand = new RelayCommand(UpdateStewardess);
            DeleteCommand = new RelayCommand(DeleteStewardess);

            DownloadData();
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public Stewardess SelectedStewardess
        {
            get { return _selectedStewardess; }
            set
            {
                _selectedStewardess = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
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
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}