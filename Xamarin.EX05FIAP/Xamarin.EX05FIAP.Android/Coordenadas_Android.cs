using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.EX05FIAP.Droid;
using Xamarin.EX05FIAP.API;
using Xamarin.Geolocation;
using Xamarin.EX05FIAP.Model;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Coordenadas_Android))]
namespace Xamarin.EX05FIAP.Droid
{
    public class Coordenadas_Android : ICoordenadas
    {
        public void GetCoordenada()
        {
            var context = Xamarin.Forms.Forms.Context as Activity;
            var locator = new Geolocator(context) { DesiredAccuracy = 50 };

            locator.GetPositionAsync(timeout: 10000).ContinueWith(t => {
                SetCoordenada(t.Result.Latitude, t.Result.Longitude);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void SetCoordenada(double paramLatitude, double paramLongitude)
        {
            var coordenada = new Coordenada()
            {
                Latitude = paramLatitude.ToString(),
                Longitude = paramLongitude.ToString()
            };

            MessagingCenter.Send<ICoordenadas, Coordenada>
                (this, "coordenada", coordenada);
        }
    }
}