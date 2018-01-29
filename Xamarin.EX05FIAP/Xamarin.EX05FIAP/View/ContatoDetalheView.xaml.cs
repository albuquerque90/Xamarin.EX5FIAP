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
        }
    }
}