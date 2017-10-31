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
using System.Runtime.CompilerServices;
using Xamarin.EX05FIAP.API;
using Xamarin.EX05FIAP.Droid;
using Xamarin.EX05FIAP.Model;

//[assembly: Dependency(typeof(Contatos_Android))]
namespace Xamarin.EX05FIAP.Droid
{
    public class Contatos_Android : IContatos
    {
        public IEnumerable<Contato> GetContato()
        {
            List<Contato> lstContato = new List<Contato>();
            Contato c = new Contato() { Nome = "Fernando", Telefone = "1111111111" };
            Contato c1 = new Contato() { Nome = "João", Telefone = "1111111111" };
            Contato c2 = new Contato() { Nome = "Alberto", Telefone = "1111111111" };
            Contato c3 = new Contato() { Nome = "Maria", Telefone = "1111111111" };

            lstContato.Add(c);
            lstContato.Add(c1);
            lstContato.Add(c2);
            lstContato.Add(c3);

            return lstContato;
        }
    }
}