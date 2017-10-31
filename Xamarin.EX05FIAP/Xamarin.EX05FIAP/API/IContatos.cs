using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.EX05FIAP.Model;

namespace Xamarin.EX05FIAP.API
{
    public interface IContatos
    {
        IEnumerable<Contato> GetContato();
    }
}
