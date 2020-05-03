using System;
using System.Collections.Generic;
using CustomGenerics.Interfaces;

namespace CustomGenerics.Structures
{
    public class PriorityQueue<T> : INotLinearDataBase<T>  where T:IComparable
    {
        List<Node<T>> priorityQueue = new List<Node<T>>();
        public int size = -1;

        int reverseEquation(int x)
        {
            return (x - 1) / 2;
        }

        void change(int x, int y)
        {

            var aux = priorityQueue[x];
            priorityQueue[x] = priorityQueue[y];
            priorityQueue[y] = aux;

        }

        int left(int x)
        {
            return (x * 2) + 1;
        }

        int right(int x)
        {
            return (x * 2) + 2;
        }

        void minHeap(int x)
        {
            int nleft = left(x);
            int nright = right(x);
            int h = x;

            if ((nleft <= size) && (priorityQueue[h].priority > priorityQueue[nleft].priority))
            {
                h = nleft;
            }
            if ((nright <= size) && priorityQueue[h].priority > priorityQueue[nright].priority)
            {
                h = nright;
            }
            if (h != x)
            {
                change(h, x);
                minHeap(h);
            }
        }

        void heapMin(int x)
        {
            while ((x >= 0) && (priorityQueue[reverseEquation(x)].priority > priorityQueue[x].priority))
            {
                change(x, reverseEquation(x));
                x = reverseEquation(x);
            }
        }

        void Inserting(long p, T data)
        {
            if (size == 1)
            {
                size = -1;
            }

            Node<T> newNode = new Node<T>();
            newNode.priority = p;
            newNode.value = data;

            priorityQueue.Add(newNode);
            size++;
            heapMin(size);
        }

        
        T Deleting()
        {
            if (size > -1)
            {
                T data = priorityQueue[0].value;
                priorityQueue[0] = priorityQueue[size];
                priorityQueue.RemoveAt(size);
                size--;
                minHeap(0);
                return data;
            }
            else
            {
                return priorityQueue[0].value;
            }
        }

        public void Insert(long value, T data)
        {
            Inserting(value, data);
        }

        public T Delete()
        {
            return Deleting();
        }

        public T Peek()
        {
            
            return priorityQueue[0].value;

        }

        public List<Node<T>> CopyOfData()
        {
            var CopyDataList = this.priorityQueue;

            return CopyDataList;
        }
    }
}
