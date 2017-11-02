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
    public partial class ContatosView : ContentPage
    {
        public string Telefone { get; set; }

        public ContatosView()
        {
            InitializeComponent();

            IContatos contatos = DependencyService.Get<IContatos>();
            lstContatos.ItemsSource = contatos.GetContato();
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Telefone))
            {
                if (await this.DisplayAlert("Ligando...", "Ligar para " + Telefone + "?", "Sim", "Não"))
                {
                    var phone = DependencyService.Get<ILigar>();
                    if (phone != null) phone.Discar(Telefone);
                }
            }
            else
            {
                await this.DisplayAlert("Aviso", "Selecione um contato na lista", "OK");
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemSelecionado = e.Item as Contato;

            Telefone = itemSelecionado.Telefone;
        }
    }
}