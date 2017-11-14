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

namespace Xamarin.EX05FIAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContatoDetalheView : ContentPage
    {
        public ContatoViewModel contatosVM = new ContatoViewModel();

        public ContatoDetalheView()
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
            // injeção de dependência (Xamarin.Forms)
            ICoordenadas geolocation = DependencyService.Get<ICoordenadas>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ICoordenadas, Coordenada>
                (this, "coordenada", (objeto, geo) =>
                {
                    //lblLongitude.Text = geo.Longitude;
                    //lblLatitude.Text = geo.Latitude;
                });
        }

        private void btnFoto_Clicked(object sender, EventArgs e)
        {

        }
    }
}