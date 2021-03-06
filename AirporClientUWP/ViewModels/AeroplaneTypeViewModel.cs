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
    public class AeroplaneTypeViewModel : ViewModelBase
    {
        private AeroplaneTypeService _service;
        private readonly IDialogService _dialogService;

        private AeroplaneType _selectedAeroplaneType;
        private ObservableCollection<AeroplaneType> _AeroplaneTypes;

        public bool DetailVisible { get; set; } = false;
        

        public AeroplaneTypeViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _service = new AeroplaneTypeService();

            AddCommand = new RelayCommand(AddAeroplaneType);
            UpdateCommand = new RelayCommand(UpdateAeroplaneType);
            DeleteCommand = new RelayCommand(DeleteAeroplaneType);

            DownloadData();
        }

        public ObservableCollection<AeroplaneType> AeroplaneTypes
        {
            get { return _AeroplaneTypes; }
            set
            {
                _AeroplaneTypes = value;
                RaisePropertyChanged(() => AeroplaneTypes);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                AeroplaneTypes = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }


        public AeroplaneType SelectedAeroplaneType
        {
            get { return _selectedAeroplaneType; }
            set
            {
                _selectedAeroplaneType = value;
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
                RaisePropertyChanged(() => SelectedAeroplaneType);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddAeroplaneType()
        {
            try
            {
                var result = await _service.AddAsync(SelectedAeroplaneType);
                AeroplaneTypes.Insert(0, result);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateAeroplaneType()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedAeroplaneType);
                AeroplaneTypes.Remove(SelectedAeroplaneType);
                AeroplaneTypes.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteAeroplaneType()
        {
            try
            {
                await _service.DeleteAsync(SelectedAeroplaneType.Id);
                AeroplaneTypes.Remove(SelectedAeroplaneType);
            }
            catch (System.InvalidOperationException ex)
            {
                await _dialogService.ShowMessage(ex.Message, "Error");
            }
        }
    }
}