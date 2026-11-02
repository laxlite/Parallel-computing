// Вариант 20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async_delegates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] SYMBOLS = { "-", "\\", "|", "/" };
            int SIZE = 1_000_000;
            Func<int, List<int>> dl = DoHardWork;
            IAsyncResult ar = dl.BeginInvoke(SIZE, null, null);
            Console.WriteLine("Вычисления запущены...");
            for (int i = 0; ; i++)
            {
                Console.Write('\b');
                Console.Write(SYMBOLS[i % 4]);
                if(ar.AsyncWaitHandle.WaitOne(100, false))
                {
                    Console.WriteLine("\nВычисления выполнены.");
                    break;
                }
            }
            List<int> result = dl.EndInvoke(ar);
            Console.WriteLine("Длина полученного массива: {0}", result.Count());
            Console.WriteLine("Длина входного массива: {0}", SIZE);
            Console.ReadKey();
        }
        // 
        static List<int> DoHardWork(int size)
        {
            int MAX_VALUE = 20_000_000;
            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(random.Next(MAX_VALUE));
            }
            List<int> newList = new List<int>();
            for (int i = 0; i < size; i++)
            {
                if (list[i] % 6 == 0)
                {
                    newList.Add(list[i]);
                }
            }
            // делаем остановку на 3000ms, чтобы симулировать сложную работу
            Thread.Sleep(3000);
            return newList;
        }
    }
}

/*
Вычисления запущены...
-
Вычисления выполнены.
Длина полученного массива: 166798
Длина входного массива: 1000000
*/