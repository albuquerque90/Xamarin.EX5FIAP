using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.EX05FIAP.API;
using Xamarin.EX05FIAP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.EX05FIAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContatoDetalheView : ContentPage
    {
        public ContatoDetalheView()
        {
            InitializeComponent();
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblTelefone.Text))
            {
                if (await this.DisplayAlert("Ligando...", "Ligar para " + lblTelefone.Text + "?", "Sim", "Não"))
                {
                    var phone = DependencyService.Get<ILigar>();
                    if (phone != null) phone.Discar(lblTelefone.Text);
                }
            }
            else
            {
                await this.DisplayAlert("Aviso", "Selecione um contato na lista", "OK");
            }
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