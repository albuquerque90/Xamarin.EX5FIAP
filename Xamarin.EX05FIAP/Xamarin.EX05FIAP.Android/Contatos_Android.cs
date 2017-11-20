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
using Xamarin.EX05FIAP.ViewModel;
using System.IO;
using Android.Graphics;

[assembly: Dependency(typeof(Contatos_Android))]
namespace Xamarin.EX05FIAP.Droid
{
    public class Contatos_Android : IContatos
    {
        public async void GetContato(ContatoViewModel vm)
        {
            var context = Xamarin.Forms.Forms.Context as Activity;
            var book = new Xamarin.Contacts.AddressBook(context);
            if (await book.RequestPermission())
            {
                foreach (Contact contact in book)
                {
                    SetContato(contact, vm);
                }
            }
            else
            {
                AlertDialog.Builder messageUI = new AlertDialog.Builder(context);
                messageUI.SetMessage("Permissão negada. Habite acesso a lista de contatos");
                messageUI.SetTitle("Autorização");
                messageUI.Create().Show();
            }
        }

        void SetContato(Contact paramContato, ContatoViewModel vm)
        {
            try
            {
                var image = paramContato.GetThumbnail();
                ImageSource imgSource = null;
                if (image != null)
                {
                    byte[] imgFile = new byte[image.Width * image.Height * 4];
                    MemoryStream stream = new MemoryStream(imgFile);
                    image.Compress(Bitmap.CompressFormat.Png, 100, stream);
                    stream.Flush();
                    imgSource = ImageSource.FromStream(()
                        => new MemoryStream(imgFile));
                }
                else
                    imgSource = ImageSource.FromFile("contacts.png");

                var contato = new Contato()
                {
                    Nome = paramContato.FirstName,
                    Foto = imgSource
                };

                if (paramContato.Phones != null)
                    contato.Telefone = paramContato.Phones.Select(p => p.Number).FirstOrDefault();

                vm.Contatos.Add(contato);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}