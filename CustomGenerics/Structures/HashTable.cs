using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures
{
    public class HashTable<S, T> where T : IComparable
    {


        //ListArray. 
        List<T>[] dataArray;
        int arrayLength;
        public int datos;

        public HashTable(int arrayLength)
        {
            this.datos = 0;

            this.arrayLength = arrayLength;
            dataArray = new List<T>[arrayLength];

            for (int i = 0; i < dataArray.Length; i++)
            {
                dataArray[i] = new List<T>();
            }
        }


        /// <summary>
        /// Metodo que devuelve el indice en el que se encuentra un elemento, basado en su llave
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int Hash(string key)
        {
            int sumaDeElementos = 0;

            for (int i = 0; i < key.Length; i++)
                sumaDeElementos += Convert.ToInt32(key[i]) * ((i + 1)*(i+1));

            int retorno = sumaDeElementos % arrayLength;
            return retorno;
        }

        /// <summary>
        /// Incerta un valor basado en su key - llave tipo string
        /// y su T value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(S key, T value)
        {
            datos++;
            if (!KeyExist(key))
            {
                int arrayIndex = Hash(key.ToString());
                dataArray[arrayIndex].Add(value);
            }
        }

        //Devuelve un boolean que indica si la llave existe o no
        public bool KeyExist(S key)
            => (Search(key) != null) ? true : false;


        /// <summary>
        /// Elimina un elemento de la tabla pasando como parametro su llave tipo string
        /// </summary>
        /// <param name="key"></param>
        public void Delete(S key)
        {
            datos--;
            int arrayIndex = Hash(key.ToString());

            foreach (var item in dataArray[arrayIndex])
            {
                
                if (item.CompareTo(key.ToString()) == 0)
                {
                    dataArray[arrayIndex].Remove(item);
                    break;
                }
            }
        }

        /// <summary>
        /// Hace la busqueda de un elemento de llave Key tipo String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Search(S key)
        {
            int Index = Hash(key.ToString());

            List<T> element = dataArray[Index];

            T retorno = element.Find((data) => {
                if (data.CompareTo(key.ToString()) == 0)
                    return true;
                else
                    return false;
            });

            return retorno;

        }

        /// <summary>
        /// Recorre toda la tabla y devuelve una lista con la data
        /// </summary>
        /// <returns></returns>
        public List<T> AllDataLikeList()
        {
            List<T> retorno = new List<T>();

            foreach (var item in this.dataArray)
            {
                if (item != null)
                {
                    foreach (var internItem in item)
                    {
                        retorno.Add(internItem);
                    }
                }
                
            }

            return retorno;

        }

    }
}
