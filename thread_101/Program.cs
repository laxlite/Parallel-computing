using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_101
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int NUMBER_OF_THREADS = 3;
            Thread[] threads = new Thread[NUMBER_OF_THREADS];
            for (int i = 0; i < NUMBER_OF_THREADS; i++)
            {
                threads[i] = new Thread(CalcStatistics);
                threads[i].Start();
            }
            Console.ReadKey();
        }
        static void CalcStatistics()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Поток № {0}, Инициализация переменных...", threadId);
            const int MATRIX_SIZE = 1_000;
            const int MAX_NUMBER = 20;
            Random random = new Random();
            List<List<int>> matrix = new List<List<int>>();
            Console.WriteLine("Поток № {0}, Создание матрицы...", threadId);
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                matrix.Add(new List<int>(MATRIX_SIZE));
            }
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                for (int j = 0; j < MATRIX_SIZE; j++)
                {
                    matrix[i].Add(random.Next(MAX_NUMBER));
                }
            }

            Dictionary<String, int> statistics = new Dictionary<string, int>();
            Console.WriteLine("Поток № {0}, Подсчет статистики...", threadId);

            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                for(int j = 0; j < MATRIX_SIZE; j++)
                {
                    string key = matrix[i][j].ToString();
                    if (!statistics.ContainsKey(key))
                    {
                        statistics.Add(key, 0);
                    }
                    statistics[key]++;
                }
            }
            Console.WriteLine("Поток № {0}, Завершение процесса. Результаты:", threadId);
            foreach (KeyValuePair<string, int> item in statistics)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
/*
Поток № 5, Инициализация переменных...
Поток № 4, Инициализация переменных...
Поток № 4, Создание матрицы...
Поток № 5, Создание матрицы...
Поток № 3, Инициализация переменных...
Поток № 3, Создание матрицы...
Поток № 4, Подсчет статистики...
Поток № 3, Подсчет статистики...
Поток № 5, Подсчет статистики...
Поток № 4, Завершение процесса. Результаты:
[10, 50077]
[8, 50313]
[1, 50270]
[6, 50447]
[11, 50139]
[7, 50016]
[19, 50045]
[16, 49984]
[15, 50165]
[13, 50230]
[4, 49490]
[18, 49710]
[17, 50190]
[12, 49996]
[3, 49597]
[0, 49810]
[5, 49741]
[9, 49488]
[14, 49992]
[2, 50300]
Поток № 3, Завершение процесса. Результаты:
[10, 50077]
[8, 50313]
[1, 50270]
[6, 50447]
[11, 50139]
[7, 50016]
[19, 50045]
[16, 49984]
[15, 50165]
[13, 50230]
[4, 49490]
[18, 49710]
[17, 50190]
[12, 49996]
[3, 49597]
[0, 49810]
[5, 49741]
[9, 49488]
[14, 49992]
[2, 50300]
Поток № 5, Завершение процесса. Результаты:
[10, 50077]
[8, 50313]
[1, 50270]
[6, 50447]
[11, 50139]
[7, 50016]
[19, 50045]
[16, 49984]
[15, 50165]
[13, 50230]
[4, 49490]
[18, 49710]
[17, 50190]
[12, 49996]
[3, 49597]
[0, 49810]
[5, 49741]
[9, 49488]
[14, 49992]
[2, 50300]
*/