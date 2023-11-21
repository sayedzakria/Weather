using Newtonsoft.Json;
using System.Web;

namespace WeatherApp.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "ChangeTabHome", (sender) =>
            {
                Shell.Current.CurrentItem = this;
            });
        }

        
    }
}
