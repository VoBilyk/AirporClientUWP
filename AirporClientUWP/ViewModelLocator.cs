using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using CommonServiceLocator;
//using MvvmDialogs;
using AirporClientUWP.ViewModels;


namespace AirporClientUWP
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<PilotViewModel>();
            SimpleIoc.Default.Register<StewardessViewModel>();
            SimpleIoc.Default.Register<CrewViewModel>();
            SimpleIoc.Default.Register<DepartureViewModel>();
            SimpleIoc.Default.Register<FlightViewModel>();
            SimpleIoc.Default.Register<AeroplaneTypeViewModel>();
            SimpleIoc.Default.Register<AeroplaneViewModel>();
            SimpleIoc.Default.Register<TicketViewModel>();

        }

        public PilotViewModel PilotVMInstance => ServiceLocator.Current.GetInstance<PilotViewModel>();

        public StewardessViewModel StewardessVMInstance => ServiceLocator.Current.GetInstance<StewardessViewModel>();

        public CrewViewModel CrewVMInstance => ServiceLocator.Current.GetInstance<CrewViewModel>();

        public AeroplaneViewModel AeroplaneVMInstance => ServiceLocator.Current.GetInstance<AeroplaneViewModel>();

        public AeroplaneTypeViewModel AeroplaneTypeVMInstance => ServiceLocator.Current.GetInstance<AeroplaneTypeViewModel>();

        public FlightViewModel FlightVMInstance => ServiceLocator.Current.GetInstance<FlightViewModel>();

        public TicketViewModel TickettVMInstance => ServiceLocator.Current.GetInstance<TicketViewModel>();

        public DepartureViewModel DepartureVMInstance => ServiceLocator.Current.GetInstance<DepartureViewModel>();
    }
}