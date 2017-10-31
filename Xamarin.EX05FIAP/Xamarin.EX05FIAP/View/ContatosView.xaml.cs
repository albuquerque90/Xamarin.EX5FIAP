using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.EX05FIAP.API;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.EX05FIAP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContatosView : ContentPage
    {
        public ContatosView()
        {
            InitializeComponent();

            IContatos contatos = DependencyService.Get<IContatos>();
            lstContatos.ItemsSource = contatos.GetContato();
        }
    }
}