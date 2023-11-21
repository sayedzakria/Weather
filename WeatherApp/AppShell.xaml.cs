using WeatherApp.Views;

namespace WeatherApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // register routs
            Routing.RegisterRoute(nameof(Home),
              typeof(Home));
                Routing.RegisterRoute(nameof(Search),
              typeof(Search));
            Routing.RegisterRoute(nameof(Forecast),
              typeof(Forecast));
        }
    }
}
