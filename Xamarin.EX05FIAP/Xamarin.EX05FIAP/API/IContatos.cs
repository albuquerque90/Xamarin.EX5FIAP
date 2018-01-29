using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.EX05FIAP.Model;
using Xamarin.EX05FIAP.ViewModel;


namespace Xamarin.EX05FIAP.API
{
    public interface IContatos
    {
        void GetContato(ContatoViewModel vm);
        //void SetContato(Xamarin.Contacts.Contact paramContato, ContatoViewModel vm);
    }
}
