using Windows.UI.Xaml.Controls;

using AirporClientUWP.Views;


namespace AirporClientUWP
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            StewardessesFrame.Navigate(typeof(StewardessView));
            PilotsFrame.Navigate(typeof(PilotsView));
        }
    }
}
