using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reverse_async_callback
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Главный поток начал работу. ");
            const int SIZE = 1_000_000;
            const double TARGET_VALUE = 234.5;
            Func<int, double, List<double> > dl = GetSubset;
            dl.BeginInvoke(SIZE, TARGET_VALUE, (ar) =>
            {
                if (ar == null) throw new ArgumentNullException("\'ar\' takes the value null");
                Func<int, double, List<double>> getted_dl = ar.AsyncState as Func<int, double, List<double>>;
                // it`s equal to: dl (4'th argument in BeginInvoke)

                List<double> result = getted_dl.EndInvoke(ar);
                Console.WriteLine("Ассинхронные вычисления выполнены.");
                Console.WriteLine("Длина сгенерированного массива: {0}", SIZE);
                Console.WriteLine("Длина полученного массива: {0}", result.Count);
            }, dl);
            Console.WriteLine("Главный поток завершил работу. Ожидание ассинхронного делегата...");
            Console.ReadKey(); // Waiting any key for wait the delegate
        }

        static List<double> GetSubset(int size, double n)
        {
            Console.WriteLine("Ассинхронные вычисления запущены.");

            const double MULTIPLIER = 1_000;
            Random random = new Random();
            List<double> list = new List<double>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(random.NextDouble() * MULTIPLIER);
            }

            List<double> newList = new List<double>();
            for (int i = 0; i < size; i++)
            {
                if (Math.Abs(list[i] - n) <= 4) newList.Add(list[i]);
            }

            Thread.Sleep(3000);
            return newList;
        }
    }
}

/*
Главный поток начал работу.
Главный поток завершил работу. Ожидание ассинхронного делегата...
Ассинхронные вычисления запущены.
Ассинхронные вычисления выполнены.
Длина сгенерированного массива: 1000000
Длина полученного массива: 7895
/*
