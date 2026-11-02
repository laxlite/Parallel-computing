// Вариант 20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks_101
{
    internal class Program
    {
        private const int MATRIX_SIZE = 10;
        private const int MAX_VALUE = 1000;
        static void Main(string[] args)
        {
            Random random = new Random();
            Action<object> action = CalcMatrix;
            List<List<int> > matrix = new List<List<int>>();
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                matrix.Add(new List<int>());
                for (int j = 0; j < MATRIX_SIZE; j++)
                {
                    matrix[i].Add(random.Next(MAX_VALUE));
                }
            }

            Console.WriteLine("Матрица сгенерирована. Вывод:");
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write("{0}, ", item);
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nЗадача отправлена на выполнение...");
            Task task = new Task(action, matrix);
            task.Start();
            Console.ReadKey();
        }

        static void CalcMatrix(object o)
        {
            List<List<int> > matrix = (List<List<int>>)o;
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                for(int j = 0; j < MATRIX_SIZE; j++)
                {
                    if (matrix[i][j] % 2 == 0) matrix[i][j] = (int)Math.Tan((double)matrix[i][j]);
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("\nЗадача выполнена. Вывод:");
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write("{0}, ", item);
                }
                Console.WriteLine();
            }
        }
    }
}
/*
Матрица сгенерирована. Вывод:
960, 660, 908, 975, 347, 422, 112, 767, 435, 669,
505, 253, 490, 461, 4, 337, 72, 764, 831, 779,
717, 176, 308, 540, 960, 777, 657, 53, 82, 437,
183, 928, 77, 609, 286, 179, 89, 382, 71, 771,
957, 22, 152, 740, 164, 466, 363, 442, 123, 62,
604, 446, 412, 663, 932, 784, 352, 367, 14, 570,
764, 59, 684, 997, 485, 576, 175, 881, 175, 825,
248, 689, 692, 59, 821, 695, 24, 568, 376, 494,
414, 215, 929, 19, 833, 736, 516, 414, 676, 502,
827, 721, 368, 501, 7, 898, 337, 154, 254, 587,

Задача отправлена на выполнение...

Задача выполнена. Вывод:
-4, 0, 0, 975, 347, 1, -1, 767, 435, 669,
505, 253, 0, 461, 1, 337, 0, 0, 831, 779,
717, 0, 0, 0, -4, 777, 657, 53, 0, 437,
183, 2, 77, 609, 0, 179, 89, -3, 71, 771,
957, 0, 2, -6, 0, 1, 363, -1, 123, -1,
1, 0, 0, 663, -1, -5, 0, 367, 7, 4,
0, 59, -1, 997, 485, 1, 175, 881, 175, 825,
0, 689, 1, 59, 821, 695, -2, 0, -1, 0,
0, 215, 929, 19, 833, 1, 0, 0, 0, 0,
827, 721, 0, 501, 7, 0, 337, 0, 0, 587,
*/