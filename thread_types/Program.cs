using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_types
{
    public class PairVec
    {
        public int id;
        public double MULTIPLIER = 100_000;
        public int n;
        public List<double> v1;
        public List<double> v2;
        public List<double> result;
        public PairVec(int id, int n)
        {
            this.id = id;
            this.n = n;
        }
    }

    public class Array
    {
        List<PairVec> list;
        public Array()
        {
            this.list = new List<PairVec>();
        }
        public void Add(PairVec pv)
        {
            list.Add(pv);
            Console.WriteLine("Добавлен элемент по индексу {0}", list.Count - 1);
            ThreadPool.QueueUserWorkItem(ThreadCalc, pv);
        }
        private static void ThreadCalc(object o)
        {
            PairVec pv = (PairVec)o;
            Console.WriteLine("Поток: {1}. В пул потоков попал вектор id = {0}...", pv.id,
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Поток: {1}. Генерируем пару векторов id = {0}...", pv.id,
                Thread.CurrentThread.ManagedThreadId);
            Random random = new Random();
            pv.v1 = new List<double>(pv.n);
            pv.v2 = new List<double>(pv.n);
            pv.result = new List<double>(pv.n);
            for (int i = 0; i<pv.n; i++)
            {
                pv.v1.Add(random.NextDouble()* pv.MULTIPLIER);
                pv.v2.Add(random.NextDouble()* pv.MULTIPLIER);
            }
            for (int i = 0; i<pv.n; i++)
            {
                pv.result.Add(pv.v1[i] + pv.v2[i]);
            }
            Thread.Sleep(1000);
            Console.WriteLine("Поток: {1}. Сумма векторов элемента вычисленна id = {0}", pv.id,
                Thread.CurrentThread.ManagedThreadId);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            const int ARRSIZE = 5;
            const int VECSIZE = 100_000;
            Array array = new Array();
            for (int i = 0; i < ARRSIZE; i++)
            {
                array.Add(new PairVec(id: i, n: VECSIZE));
            }
            Console.ReadKey();
        }
    }
}

/*
Добавлен элемент по индексу 0
Добавлен элемент по индексу 1
Добавлен элемент по индексу 2
Добавлен элемент по индексу 3
Добавлен элемент по индексу 4
Поток: 4. В пул потоков попал вектор id = 0...
Поток: 4. Генерируем пару векторов id = 0...
Поток: 8. В пул потоков попал вектор id = 4...
Поток: 8. Генерируем пару векторов id = 4...
Поток: 3. В пул потоков попал вектор id = 2...
Поток: 7. В пул потоков попал вектор id = 3...
Поток: 5. В пул потоков попал вектор id = 1...
Поток: 5. Генерируем пару векторов id = 1...
Поток: 7. Генерируем пару векторов id = 3...
Поток: 3. Генерируем пару векторов id = 2...
Поток: 5. Сумма векторов элемента вычисленна id = 1
Поток: 8. Сумма векторов элемента вычисленна id = 4
Поток: 3. Сумма векторов элемента вычисленна id = 2
Поток: 7. Сумма векторов элемента вычисленна id = 3
Поток: 4. Сумма векторов элемента вычисленна id = 0
*/

/*
    id - id элемента коллекции
    Поток - номер потока в пуле, который выполнят преобразования с элементом id
*/