using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AirporClientUWP.Services;
using AirporClientUWP.Models;


namespace AirporClientUWP.Views
{
    public sealed partial class PilotsView : Page
    {
        //private PilotService _service;

        //private Pilot _selectedPilot;
        //public ObservableCollection<Pilot> pilots;
        
        public PilotsView()
        {
            this.InitializeComponent();
            //_service = new PilotService();
            //pilots = _service.GetAllAsync().Result;
            //_selectedPilot = pilots[1];
        }

        //public async Task<ObservableCollection<Pilot>> GetPilots()
        //{
        //    var service = new PilotService();
        //    pilots = await service.GetAllAsync();
        //    return pilots;
        //}

        //private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var list = (ListView)sender;
        //    _selectedPilot = (Pilot)list.SelectedValue;
        //}
    }
}
