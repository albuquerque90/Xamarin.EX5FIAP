using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.EX05FIAP.API;
using Xamarin.EX05FIAP.Model;
using Xamarin.Forms;

namespace Xamarin.EX05FIAP.ViewModel
{
    public class ContatoViewModel : INotifyPropertyChanged
    {
        private Contato contato;
        public Contato Contato
        {
            get { return contato; }
            set
            {
                contato = value as Contato;
                OnLigarCMD.LigarCanExecuteChanged();
                EventPropertyChanged();
            }
        }

        public ContatoViewModel()
        {
            OnLigarCMD = new OnLigarCMD(this);
            OnDetalheCMD = new OnDetalheCMD(this);
        }

        public ObservableCollection<Contato> Contatos { get; set; } = new ObservableCollection<Contato>();
        public OnLigarCMD OnLigarCMD { get; }
        public OnDetalheCMD OnDetalheCMD { get; }

        public async void Ligar(Contato paramContato)
        {
            if (!string.IsNullOrWhiteSpace(paramContato.Telefone))
            {
                if (await App.Current.MainPage.DisplayAlert(
                    "Ligando...", "Ligar para " + paramContato.Telefone + "?", "Sim", "Não"))
                {
                    var phone = DependencyService.Get<ILigar>();
                    if (phone != null) phone.Discar(paramContato.Telefone);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso", "Selecione um contato na lista", "OK");
            }
        }

        public void GetDetalhe(Contato paramContato)
        {
            if (paramContato != null)
            {
                App.Current.MainPage.Navigation.PushAsync(
                    new View.ContatoDetalheView() { BindingContext = paramContato });
            }
        }

        public async void GetCoordenada()
        {
            string longitude = null;
            string latitude = null;

            ICoordenadas geolocation = DependencyService.Get<ICoordenadas>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ICoordenadas, Coordenada>
                (this, "coordenada", (objeto, geo) =>
                {
                    longitude = geo.Longitude;
                    latitude = geo.Latitude;
                });

            await App.Current.MainPage.DisplayAlert(
                    "Coordenada...", $"lat {latitude} long {longitude}", "Sim", "Não");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public class OnLigarCMD : ICommand
    {
        private ContatoViewModel contatoVM;
        public OnLigarCMD(ContatoViewModel paramVM)
        {
            contatoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void LigarCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter)
        {
            if (parameter != null) return true;

            return false;
        }
        public void Execute(object parameter)
        {
            try
            {
                contatoVM.Ligar(parameter as Contato);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public class OnDetalheCMD : ICommand
    {
        private ContatoViewModel contatoVM;
        public OnDetalheCMD(ContatoViewModel paramVM)
        {
            contatoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void DetalheCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter)
        {
            if (parameter != null) return true;

            return false;
        }
        public void Execute(object parameter)
        {
            contatoVM.GetDetalhe(parameter as Contato);
        }
    }

    public class OnGetCoordenadaCMD : ICommand
    {
        private ContatoViewModel contatoVM;
        public OnGetCoordenadaCMD(ContatoViewModel paramVM)
        {
            contatoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void CoordenadaCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter)
        {
            if (parameter != null) return true;

            return false;
        }
        public void Execute(object parameter)
        {
            contatoVM.GetCoordenada();
        }
    }
}
