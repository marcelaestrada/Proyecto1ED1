using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures
{
    public class Node<T>
    {
        public T value { get; set; }
        public long priority { get; set; }
    }
}
