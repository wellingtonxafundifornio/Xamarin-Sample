using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloXamarin
{
	public partial class MainPage : ContentPage
	{
        private double Latitude = 0;
        private double Longitude = 0;

		public MainPage()
		{
            GetPosition();
            InitializeComponent();
        }

        private async void TirarFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo"
                });

            if (file == null)
                return;

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }

        private async void EscolherFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }

        private async void GravarVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakeVideoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo",
                    Quality = VideoQuality.Medium
                });

            if (file == null)
                return;

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }

        private async void EscolherVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Ops", "Galeria de videos não suportada.", "OK");

                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });
        }

        private async void GetPosition()
        {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                this.Latitude = position.Latitude;
                this.Longitude = position.Longitude;
        }

        private async void OpenMaps(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Mapa(this.Latitude, this.Longitude), true);
        }
    }
}
