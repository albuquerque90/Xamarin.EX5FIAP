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
//using System.Runtime.CompilerServices;
using Xamarin.EX05FIAP.API;
using Xamarin.EX05FIAP.Droid;
using Xamarin.EX05FIAP.Model;
using Xamarin.Contacts;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Contatos_Android))]
namespace Xamarin.EX05FIAP.Droid
{
    public class Contatos_Android : IContatos
    {
        public IEnumerable<Contato> GetContato()
        {
            var context = Xamarin.Forms.Forms.Context;
            if (context == null) return null;

            var book = new Xamarin.Contacts.AddressBook(context);

            List<Contato> lstContato = new List<Contato>();

            book.RequestPermission().ContinueWith(t =>
            {
                if (!t.Result)
                {
                    return;
                }

                foreach (Contact contact in book)
                {
                    Contato contato = new Contato { Id = contact.Id, Nome = contact.DisplayName, Telefone = contact.Phones.First().Number };
                    lstContato.Add(contato);
                }
            });
            return lstContato;
        }
    }
}