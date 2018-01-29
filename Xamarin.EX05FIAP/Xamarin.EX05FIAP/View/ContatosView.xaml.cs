using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.EX05FIAP.API;
using Xamarin.EX05FIAP.Model;
using Xamarin.EX05FIAP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Xamarin.EX05FIAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Activity(Label = "IntentAction", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class ContatosView : ContentPage
    {
        public ContatoViewModel contatosVM = new ContatoViewModel();

        public ContatosView()
        {
            BindingContext = contatosVM;
            InitializeComponent();

            GetContatos(contatosVM);
        }

        private void GetContatos(ContatoViewModel vm)
        {
            IContatos lista = DependencyService.Get<IContatos>();
            lista.GetContato(vm);


        }


        private void btnCoodenadas_Clicked(object sender, EventArgs e)
        {
            ICoordenadas geolocation = DependencyService.Get<ICoordenadas>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ICoordenadas, Coordenada>
                (this, "coordenada", (objeto, geo) =>
                {
                    var geoUri = global::Android.Net.Uri.Parse($"geo:{geo.Latitude},{geo.Longitude}");
                    Intent mapIntent = new Intent(Intent.ActionView, geoUri);
                    mapIntent.AddFlags(ActivityFlags.NewTask);
                    //StartActivity(mapIntent);
                    global::Android.App.Application.Context.StartActivity(mapIntent);
                });
        }

    }
}