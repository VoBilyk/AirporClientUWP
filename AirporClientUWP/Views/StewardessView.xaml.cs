using AirporClientUWP.Models;
using AirporClientUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirporClientUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StewardessView : Page
    {

        private StewardessService _service;

        private Stewardess _selectedItem;
        public ObservableCollection<Stewardess> stewardesses;

        public StewardessView()
        {
            this.InitializeComponent();
            _service = new StewardessService();
            //stewardesses = _service.GetAllAsync().Result;
            //_selectedItem = stewardesses[1];
        }

        public async Task<ObservableCollection<Stewardess>> GetPilots()
        {
            stewardesses = await _service.GetAllAsync();
            return stewardesses;
        }

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListView)sender;
            _selectedItem = (Stewardess)list.SelectedValue;
        }
    }
}
