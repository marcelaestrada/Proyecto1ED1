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
                //return Search(Value2, tree.Right);
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


        
        //Delete Functions
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

            //To left sub tree
            if (value.CompareTo(actualRoot.dpi) == -1)
            {
                actualRoot.Left = Delete(actualRoot.Left, value);
            }

            //To Right sub tree
            else if (value.CompareTo(actualRoot.dpi) == 1)
            {
                actualRoot.Right = Delete(actualRoot.Right, value);
            }
            //The node is found
            else
            {
                //just a right sub tree
                if (actualRoot.Left == null) return actualRoot.Right;
                //just a right sub tree 
                else if (actualRoot.Right == null) return actualRoot.Left;

                else
                {
                    //Remove from left sub tree.
                    if (actualRoot.Left.Height > actualRoot.Right.Height)
                    {
                        //find the biggest right node from the left. 
                        //sap values
                        long sonValue = MostRight(actualRoot.Left);
                        actualRoot.dpi = sonValue;
                        //find the last node of the left sub tree
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

            // Re balance the tree
            OverWriteBF(actualRoot);

            return Balance(actualRoot);

        }

        //find the most left node
        private long MostLeft(NodeAVL<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.dpi;
        }
        //find the most right node
        private long MostRight(NodeAVL<T> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node.dpi; ;
        }

        #region Avl 

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

        private NodeAVL<T> Balance(NodeAVL<T> node)
        {
            if (node.Fe == -2)
            {
                if (node.Left.Fe <= 0) return LeftToLeft(node);
                else return LeftToRight(node);
            }
            else if (node.Fe == 2)
            {
                if (node.Right.Fe >= 0) return RightToRight(node);
                else return RightToLeft(node);
            }

            return node;
        }
        private NodeAVL<T> LeftToLeft(NodeAVL<T> node)
        {
            return RotationToRight(node);
        }
        private NodeAVL<T> LeftToRight(NodeAVL<T> node)
        {
            node.Left = RotationToLeft(node.Left);
            return LeftToLeft(node);
        }
        private NodeAVL<T> RightToRight(NodeAVL<T> node)
        {
            return RotationToLeft(node);
        }
        private NodeAVL<T> RightToLeft(NodeAVL<T> node)
        {
            node.Right = RotationToRight(node.Right);
            return RightToRight(node);
        }
        private NodeAVL<T> RotationToLeft(NodeAVL<T> node)
        {
            NodeAVL<T> AuxParent = node.Right;
            node.Right = AuxParent.Left;
            AuxParent.Left = node;

            OverWriteBF(node);
            OverWriteBF(AuxParent);
            return AuxParent;
        }
        private NodeAVL<T> RotationToRight(NodeAVL<T> node)
        {
            NodeAVL<T> AuxParent = node.Left;
            node.Left = AuxParent.Right;
            AuxParent.Right = node;

            OverWriteBF(node);
            OverWriteBF(AuxParent);
            return AuxParent;
        }

       







        #endregion




    }
}
