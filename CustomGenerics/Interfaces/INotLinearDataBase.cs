using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomGenerics.Structures;

namespace CustomGenerics.Interfaces
{
    public interface INotLinearDataBase<T>
    {
        void Insert(int priority, string data);
        string Delete();
    }
}
