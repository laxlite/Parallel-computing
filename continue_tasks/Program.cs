// Вариант 20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace continue_tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            const int MAX_VALUE = 1_000_000;
            const int MAX_SIZE = 10_000;
            int MATRIX_SIZE = random.Next(MAX_SIZE);
            List<List<int>> matrix = new List<List<int>>();
            Task task_base = new Task(() => {
                Console.WriteLine("Основная задача {0} начала выполнение...", Task.CurrentId);
                for (int i = 0; i < MATRIX_SIZE; i++) 
                {
                    matrix.Add(new List<int>());
                    for (int j = 0; j < MATRIX_SIZE; j++)
                    {
                        matrix[i].Add(random.Next(MAX_VALUE));
                    }
                }
                Console.WriteLine("Основная задача {0} завершила выполнение.\nСоздана матрица размера {1} на {1}", Task.CurrentId, MATRIX_SIZE);
                
            });

            Task task_continue_1 = task_base.ContinueWith(SumEvenDiv4, matrix);
            Task task_continue_2 = task_base.ContinueWith(FindMaxDiv3, matrix);
            Task task_continue_3 = task_base.ContinueWith(FindMin, matrix);

            task_base.Start();
            Console.ReadKey();
        }
        static void SumEvenDiv4(Task task, object o)
        {
            Console.WriteLine("Задача {0} начала выполнение...", Task.CurrentId);
            List<List<int>> matrix = (List<List<int>>)o;
            int sum = 0;
            foreach (List<int> list in matrix)
            {
                foreach (int i in list)
                {
                    if (i % 2 == 0 && i % 4 == 0) sum += i;
                }
            }
            Thread.Sleep(2000);
            Console.WriteLine("Задача {0} завершена успешно. Сумма элементов: {1}", Task.CurrentId, sum);
        }
        static void FindMaxDiv3(Task task, object o)
        {
            Console.WriteLine("Задача {0} начала выполнение...", Task.CurrentId);
            List<List<int>> matrix = (List<List<int>>)o;
            int maximum = int.MinValue;
            foreach (List<int> list in matrix)
            {
                foreach (int i in list)
                {
                    if (i % 3 == 0 && maximum < i) maximum = i;
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("Задача {0} завершена успешно. Максимальное значение: {1}", Task.CurrentId, maximum);
        }
        static void FindMin(Task task, object o)
        {
            Console.WriteLine("Задача {0} начала выполнение...", Task.CurrentId);
            List<List<int> > matrix = (List<List<int>>)o;
            int minimum = int.MaxValue;
            foreach (List<int> list in matrix)
            {
                foreach (int i in list)
                {
                    if (i < minimum) minimum = i;
                }
            }
            Thread.Sleep(1500);
            Console.WriteLine("Задача {0} завершена успешно. Минимальное значение: {1}", Task.CurrentId, minimum);
        }
    }
}
/*
Основная задача 4 начала выполнение...
Основная задача 4 завершила выполнение.
Создана матрица размера 5214 на 5214
Задача 1 начала выполнение...
Задача 3 начала выполнение...
Задача 2 начала выполнение...
Задача 2 завершена успешно. Максимальное значение: 999999
Задача 3 завершена успешно. Минимальное значение: 0
Задача 1 завершена успешно. Сумма элементов: 46147132
*/