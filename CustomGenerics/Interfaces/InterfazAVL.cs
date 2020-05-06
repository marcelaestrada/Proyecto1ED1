using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomGenerics.Structures;

namespace CustomGenerics.Interfaces
{
    public interface InterfazAVL<T> 
    {
        void Insert(string name, string lastname, long cui, T value);
        void Delete(long value);
        List<T> Busqueda(string parametro, string buscado);
    }
}
