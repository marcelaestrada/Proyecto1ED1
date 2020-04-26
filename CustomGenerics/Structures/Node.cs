using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures
{
    public class Node<T>
    {
        public string value { get; set; }
        public int priority { get; set; }
    }
}
