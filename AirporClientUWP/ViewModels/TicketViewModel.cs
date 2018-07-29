using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using AirporClientUWP.Models;
using AirporClientUWP.Services;

namespace AirporClientUWP.ViewModels
{
    public class TicketViewModel : ViewModelBase
    {
        private TicketService _service;
        private Ticket _selectedTicket;
        private ObservableCollection<Ticket> _Tickets;


        public TicketViewModel()
        {
            _service = new TicketService();

            AddCommand = new RelayCommand(AddTicket);
            UpdateCommand = new RelayCommand(UpdateTicket);
            DeleteCommand = new RelayCommand(DeleteTicket);

            DownloadData();
        }

        public ObservableCollection<Ticket> Tickets
        {
            get { return _Tickets; }
            set
            {
                _Tickets = value;
                RaisePropertyChanged(() => Tickets);
            }
        }

        private async Task DownloadData()
        {
            try
            {
                Tickets = await _service.GetAllAsync();
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }


        public Ticket SelectedTicket
        {
            get { return _selectedTicket; }
            set
            {
                _selectedTicket = value;
                RaisePropertyChanged(() => SelectedTicket);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddTicket()
        {
            try
            {
                var result = await _service.AddAsync(SelectedTicket);
                Tickets.Insert(0, result);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand UpdateCommand { get; set; }

        private async void UpdateTicket()
        {
            try
            {
                var resultItem = await _service.UpdateAsync(SelectedTicket);
                Tickets.Remove(SelectedTicket);
                Tickets.Insert(0, resultItem);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }

        public ICommand DeleteCommand { get; set; }

        private async void DeleteTicket()
        {
            try
            {
                await _service.DeleteAsync(SelectedTicket.Id);
                Tickets.Remove(SelectedTicket);
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}