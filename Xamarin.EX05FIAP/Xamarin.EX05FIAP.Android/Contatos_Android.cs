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
            try
            {
               var book = new AddressBook(Xamarin.Forms.Forms.Context);

                book.RequestPermission().Wait();

                var contatos = book.ToList().Select(contact =>
                {
                    string phone = null;

                    if (contact.Phones.Count() > 0)
                    {
                        phone = contact.Phones.FirstOrDefault().Number;
                    }

                    return new Contato { Id = contact.Id, Nome = contact.DisplayName, Telefone = phone };

                }).ToList();

                return contatos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}