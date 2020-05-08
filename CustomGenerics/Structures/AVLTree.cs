using System;
using System.Collections.Generic;
using CustomGenerics.Interfaces;
using System.Linq;
using System.IO;

namespace CustomGenerics.Structures
{
    public class AVLTree<T> : InterfazAVL<T> where T:IComparable
    {
        public NodeAVL<T> root;

        public AVLTree()
        {
            root = null;
        }

        //metodos de insercion 
        private NodeAVL<T> Insert(NodeAVL<T> tree, NodeAVL<T> sub)
        {
            NodeAVL<T> newRoot = sub;
            if (tree.dpi.CompareTo(sub.dpi) == -1)
            {
                if (sub.Left == null)
                {
                    sub.Left = tree;
                }
                else
                {
                    sub.Left = Insert(tree, sub.Left);
                    if (ObtainFe(sub.Left) - ObtainFe(sub.Right) == 2)
                    {
                        if (tree.dpi.CompareTo(sub.Left.dpi) == -1)
                        {
                            newRoot = RotationLeft(sub);
                        }
                        else
                        {
                            newRoot = DoubleRotationLeft(sub);
                        }
                    }
                }
            }
            else if (tree.dpi.CompareTo(sub.dpi) == 1) 
            {
                if (sub.Right == null)
                {
                    sub.Right = tree;
                }
                else
                {
                    sub.Right = Insert(tree, sub.Right);
                    if (ObtainFe(sub.Right) - ObtainFe(sub.Left) == 2)
                    {
                        if (tree.dpi.CompareTo(sub.Right.dpi) == 1)
                        {
                            newRoot = RotationRight(sub);
                        }
                        else
                        {
                            newRoot = DoubleRotationRight(sub);
                        }
                    }
                }
            }

            if ((sub.Left == null) && (sub.Right != null))
            {
                sub.Fe = sub.Right.Fe + 1;
            }
            else if ((sub.Right == null) && (sub.Left != null))
            {
                sub.Fe = sub.Left.Fe + 1;
            }
            else
            {
                sub.Fe = Math.Max(ObtainFe(sub.Left), ObtainFe(sub.Right));
            }
            return newRoot;
        }
        public void Insert(string name, string lastname, long cui, T data)
        {
            NodeAVL<T> tree = new NodeAVL<T>();
            tree.nombre = name;
            tree.apellido = lastname;
            tree.dpi = cui;
            tree.value = data;

            if (root == null)
            {
                root = tree;
            }
            else
            {
                root = Insert(tree, root);
            }
        }

        //metodo de factor de equilibrio
        public int ObtainFe(NodeAVL<T> tree)
        {
            if (tree == null)
            {
                return -1;
            }
            else
            {
                return tree.Fe;
            }
        }

        //metodo de busqueda 
        private NodeAVL<T> Search (long Value2, NodeAVL<T> tree)
        {
            if (root == null)
            {
                return null;
            }
            else if (tree.dpi.CompareTo(Value2) == 0)
            {
                return tree;
            }
            else if (Value2.CompareTo(tree.dpi) == -1)
            {
                
                return Search(Value2, tree.Left);
            }
            else
            {
               return Search(Value2, tree.Right);
            }
        }

        public List<T> Busqueda(string parametro, string buscado)
        {

            List<T> usuario = new List<T>();
            return preOrder(root, parametro, buscado, usuario);
        }

        public bool Search(long value)
        {
            return Search(value, root) != null ? true : false;
        }

        /// <summary>
        /// Regresa el elemento especifico buscado.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public NodeAVL<T> SearchOneValue(long value)
        {
            return Search(value, root);
        }
       
        
        //metodos de rotacion
        public NodeAVL<T> RotationLeft(NodeAVL<T> tree)
        {
            NodeAVL<T> aux = tree.Left;
            tree.Left = aux.Right;
            aux.Right = tree;
            tree.Fe = Math.Max(ObtainFe(tree.Left), ObtainFe(tree.Right)+1);
            aux.Fe = Math.Max(ObtainFe(aux.Left), ObtainFe(aux.Right));
            return aux;
        }
        public NodeAVL<T> RotationRight(NodeAVL<T> tree)
        {
            NodeAVL<T> aux = tree.Right;
            tree.Right = aux.Left;
            aux.Left = tree;
            tree.Fe = Math.Max(ObtainFe(tree.Left), ObtainFe(tree.Right) + 1);
            aux.Fe = Math.Max(ObtainFe(aux.Left), ObtainFe(aux.Right));
            return aux;
        }
        public NodeAVL<T> DoubleRotationLeft(NodeAVL<T> tree)
        {
            NodeAVL<T> aux;
            tree.Left = RotationRight(tree.Left);
            aux = RotationLeft(tree);
            return aux;
        }
        public NodeAVL<T> DoubleRotationRight(NodeAVL<T> tree)
        {
            NodeAVL<T> aux;
            tree.Right = RotationLeft(tree.Right);
            aux = RotationRight(tree);
            return aux;
        }

        //metodos de recorrido 
        public List<T> preOrder(NodeAVL<T> tree, string parametro, string buscado, List<T> usuario)
        {
            
            if (tree == null)
            {
                return null;
            }
            else
            {
                if (parametro == "nombre")
                {
                    if (tree.nombre.CompareTo(buscado) == 0)
                    {
                        usuario.Add(tree.value);
                    }
                }
                else if(parametro == "apellido")
                {
                    if (tree.apellido.CompareTo(buscado) == 0)
                    {
                        usuario.Add(tree.value);
                    }
                }
                else if (parametro == "dpi")
                {
                    if (tree.dpi.CompareTo(long.Parse(buscado)) == 0)
                    {
                        usuario.Add(tree.value);
                    }
                }
                preOrder(tree.Left, parametro, buscado, usuario);
                preOrder(tree.Right, parametro, buscado, usuario);
            }
            return usuario;
        }

        public void inOrder(NodeAVL<T> tree, string _Path)
        {
           
            if (tree == null)
            {
                return;
            }
            else
            {
                inOrder(tree.Left,_Path);
                inOrder(tree.Right, _Path);
            }
        }

        public void postOrder(NodeAVL<T> tree)
        {
            if (tree == null)
            {
                return;
            }
            else
            {
                postOrder(tree.Left);
                postOrder(tree.Right);
                Console.Write(tree.nombre + " - ");
            }
        }


        
        /// <summary>
        /// Eliminar un nodo
        /// </summary>
        /// <param name="value"></param>
        public void Delete(long value)
        {
            if (Search(value, root) != null)
            {
                root = Delete(root, value);
                
            }
        }

        private NodeAVL<T> Delete(NodeAVL<T> actualRoot, long value)
        {
            if (actualRoot == null)
                return null;

            //A hijo izquierdo
            if (value.CompareTo(actualRoot.dpi) == -1)
            {
                actualRoot.Left = Delete(actualRoot.Left, value);
            }

            //A hijo derecho
            else if (value.CompareTo(actualRoot.dpi) == 1)
            {
                actualRoot.Right = Delete(actualRoot.Right, value);
            }
            //Se encuentra el nodo
            else
            {
                //solo tiene sub arbol derecho. 
                if (actualRoot.Left == null) return actualRoot.Right;
                //solo tiene sub arbol izquierdo. 
                else if (actualRoot.Right == null) return actualRoot.Left;

                else
                {
                    //Elimina del hijo izquierdo
                    if (actualRoot.Left.Height > actualRoot.Right.Height)
                    {
                        //Encontrar el nodo mas a la derecha del hijo izquierdo
                        //Intercambiar valores
                        long sonValue = MostRight(actualRoot.Left);
                        actualRoot.dpi = sonValue;
                        //Encuentra el ultimo nodo del izquierdo. 
                        actualRoot.Left = Delete(actualRoot.Left, sonValue);
                    }

                    else
                    {
                        //find the smallest left node from the right
                        //swap values
                        long sonValue = MostLeft(actualRoot.Right);
                        actualRoot.dpi = sonValue;

                        //int the right sub tree, remove the most left node.
                        actualRoot.Right = Delete(actualRoot.Right, sonValue);
                    }
                }
            }

            
            OverWriteBF(actualRoot);

            return Balance(actualRoot);

        }

        //Encontrar el nodo mas a la izquierda
        private long MostLeft(NodeAVL<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.dpi;
        }
        //Encontrar el nodo mas a la derecha
        private long MostRight(NodeAVL<T> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node.dpi; ;
        }

       

        /// <summary>
        /// Sobre escribe el factor de equilibrio
        /// </summary>
        /// <param name="node"></param>
        private void OverWriteBF(NodeAVL<T> node)
        {
            int leftHeight, rightHeight;

            if (node.Left == null) leftHeight = -1;
            else leftHeight = node.Left.Height;

            if (node.Right == null) rightHeight = -1;
            else rightHeight = node.Right.Height;


            node.Height = 1 + Math.Max(leftHeight, rightHeight);

            node.Fe = rightHeight - leftHeight;

        }

        /// <summary>
        /// metodo que re balancea la estructura
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private NodeAVL<T> Balance(NodeAVL<T> node)
        {
            if (node.Fe == -2)
            {
                if (node.Left.Fe <= 0) return RotationRight(node);
                else return DoubleRotationRight(node);
            }
            else if (node.Fe == 2)
            {
                if (node.Right.Fe >= 0) return RotationLeft(node);
                else return DoubleRotationLeft(node); 
            }

            return node;
        }
      

 




    }
}
