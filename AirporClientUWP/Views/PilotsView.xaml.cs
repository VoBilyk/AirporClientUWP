using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AirporClientUWP.Services;
using AirporClientUWP.Models;

namespace AirporClientUWP.Views
{
    public sealed partial class PilotsView : Page
    {
        private PilotService _service;

        public PilotsView()
        {
            this.InitializeComponent();
            _service = new PilotService();
            pilots = _service.GetAllAsync().Result;
        }

        public ObservableCollection<Pilot> pilots;


        public async Task<ObservableCollection<Pilot>> GetPilots()
        {
            var service = new PilotService();
            pilots = await service.GetAllAsync();
            return pilots;
        }
    }
}
