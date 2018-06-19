using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace HelloXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Mapa : ContentPage
	{
		public Mapa (double aLatitude, double aLogitude)
		{
			InitializeComponent();

            MapaTela.MoveToRegion(MapSpan.FromCenterAndRadius(
                              new Position(aLatitude, aLogitude),
                              Distance.FromMiles(0.5)));

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(aLatitude, aLogitude),
                Label = "Minha Localização",
                Address = "http://www.centi.com.br/",
            };

            MapaTela.Pins.Add(pin1);

        }
	}
}