using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
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
        public bool DetailVisible { get; set; } = false;


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
                var tickets = await _service.GetAllAsync();

                var _flightService = new FlightService();
                var flight = await _flightService.GetAllAsync();
                foreach (var ticket in tickets)
                {
                    ticket.Flight = flight.SingleOrDefault(x => x.Id == ticket.FlightId);
                }
                flight.Clear();

                Tickets = tickets;
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
                DetailVisible = true;

                RaisePropertyChanged(() => DetailVisible);
                RaisePropertyChanged(() => SelectedTicket);
            }
        }

        public ICommand AddCommand { get; set; }

        private async void AddTicket()
        {
            try
            {
                var result = await _service.AddAsync(SelectedTicket);
                result.Flight = await new FlightService().GetAsync(result.FlightId);
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
                var result = await _service.UpdateAsync(SelectedTicket);
                result.Flight = await new FlightService().GetAsync(result.FlightId);

                Tickets.Remove(SelectedTicket);
                Tickets.Insert(0, result);
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