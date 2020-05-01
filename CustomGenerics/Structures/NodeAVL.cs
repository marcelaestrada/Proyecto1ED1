using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures
{
    public class NodeAVL<T> where T : IComparable
    {
       public int Fe { get; set; }
       public int Height { get; set; }
       public T value { get; set; }
       public string nombre { get; set; }
       public string apellido { get; set; }
       public long dpi { get; set; }
       public NodeAVL<T> Right { get; set; }
       public NodeAVL<T> Left { get; set; }

        public NodeAVL(string nombre, string apellido, long dpi)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dpi = dpi;
            this.Fe = 0;
            this.Left = null;
            this.Right = null;
            this.Height = 0;
        }

        public NodeAVL()
        {
        }
    }
}
