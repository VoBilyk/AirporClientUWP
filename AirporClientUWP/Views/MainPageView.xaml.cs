using Windows.UI.Xaml.Controls;

namespace AirporClientUWP.Views
{
    public sealed partial class MainPageView : Page
    {
        public MainPageView()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(FlightsView));
        }

        private void SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag)
            {
                case "Pilots":
                    Menu.Header = "Pilots";
                    contentFrame.Navigate(typeof(PilotsView));
                    break;

                case "Stewardesses":
                    Menu.Header = "Stewardesses";
                    contentFrame.Navigate(typeof(StewardessView));
                    break;

                case "Crews":
                    Menu.Header = "Crews";
                    contentFrame.Navigate(typeof(CrewView));
                    break;

                case "Tickets":
                    Menu.Header = "Tickets";
                    contentFrame.Navigate(typeof(TicketView));
                    break;

                case "Flights":
                    Menu.Header = "Flights";
                    contentFrame.Navigate(typeof(FlightsView));
                    break;

                case "Aeroplanes":
                    Menu.Header = "Aeroplanes";
                    contentFrame.Navigate(typeof(AeroplaneView));
                    break;

                case "AeroplaneTypes":
                    Menu.Header = "Aeroplane Types";
                    contentFrame.Navigate(typeof(AeroplaneTypeView));
                    break;

                case "Departures":
                    Menu.Header = "Departures";
                    contentFrame.Navigate(typeof(DepartureView));
                    break;
            }
        }
    }
}
