using Android.Content;
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
        }

        private void btnLigar_Clicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblTelefone.Text))
            {
                    var phone = DependencyService.Get<ILigar>();
                    if (phone != null) phone.Discar(lblTelefone.Text);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Aviso", "Selecione um contato na lista", "OK");
            }
        }

        private void btnTrocarFoto_Clicked(object sender, System.EventArgs e)
        {

            ICamera capturar = DependencyService.Get<ICamera>();
            capturar.CapturarFoto();

            MessagingCenter.Subscribe<ICamera, string>(this, "cameraFoto",
                (objeto, image) =>
                {

                    imgFoto.Source = ImageSource.FromFile(image);
                });            
        }

        private void btnCoordenadas_Clicked(object sender, System.EventArgs e)
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