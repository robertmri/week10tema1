using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> intGenericList = new GenericList<int>();
             
            intGenericList.Add(1);
            intGenericList.Add(2);
            intGenericList.Add(3);
            intGenericList.Add(4);
            intGenericList.Add(5);
            intGenericList.Add(6);

            
            intGenericList.RemoveAt(1);
            intGenericList.RemoveAt(3);

            intGenericList.InsertAt(2, 999);

            Console.WriteLine(intGenericList);

            Console.WriteLine(intGenericList.Min());

            Console.WriteLine(intGenericList.Max());
            
            intGenericList.Clear();
            
            Console.WriteLine(intGenericList);
        }

    }
    public class GenericList<T> : IEnumerable<T>
    {
        public GenericList(int capacity)
        {
            genericList = new T[capacity];
            Capacity = capacity;
        }

        public GenericList()
        {
            genericList = new T[1];
            Capacity = 1;
        }

        private T[] genericList { get; set; }
        private T[] auxList { get; set; }
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public T this[int index]
        {
            get { return genericList[index]; }
            set { genericList[index] = value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return genericList[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AutoGrow()
        {
            auxList = new T[Capacity];
            for (int i = 0; i < Count; i++)
                auxList[i] = genericList[i];

            genericList = new T[Capacity *= 2];
            for (int i = 0; i < auxList.Length; i++)
                genericList[i] = auxList[i];

            auxList = new T[0];
        }

        public void Add(T value)
        {
            if (Count == Capacity - 1)
                AutoGrow();
            genericList[Count++] = value;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            for (int i = index; i < Count - 1; i++)
                genericList[i] = genericList[i + 1];

            genericList[Count--] = default;
        }

        public void Clear()
        {
            genericList = new T[0];
            auxList = new T[0];
            Count = 0;
            Capacity = 0;
        }

        public void InsertAt(int index, T value)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();

            AutoGrow();
            for (int i = Count - 1; i >= index; i--)
                genericList[i + 1] = genericList[i];

            genericList[index] = value;
            Count++;
        }

        public T Min<T>() where T : struct
        {
            dynamic min = genericList[0];
            for (int i = 0; i < Count; i++)
                if (genericList[i] < min)
                    min = genericList[i];

            return min;
        }

        public T Max<T>() where T : struct
        {
            dynamic max = genericList[0];
            for (int i = 0; i < Count; i++)
                if (genericList[i] < max)
                    max = genericList[i];

            return max;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in this)
                sb.Append($"{element}\n");

            return sb.ToString();
        }
    }
}
