using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_args
{
    public struct Data
    {
        public string message;
        public int offset;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Data data_1 = new Data { message = "Hello, world! It's the TEST_STRING!!!", offset = 95 };
            Data data_2 = new Data { message = "Hello, world! It's the TEST_STRING!!!", offset = 12 };
            Thread th_1 = new Thread(EncryptMessage);
            Thread th_2 = new Thread(EncryptMessage);
            th_1.Start(data_1);
            Thread.Sleep(1000);
            th_2.Start(data_2);
            Console.ReadKey();
        }

        static void EncryptMessage(object o)
        {
            string message = ((Data)o).message;
            int n = ((Data)o).offset;
            StringBuilder sb = new StringBuilder();
            foreach (char c in message)
            {
                char current_c = (char)(c + n);
                if(current_c > (char)126)
                {
                    current_c = (char)(current_c % 127 + ' ');
                }
                sb.Append(current_c);
            }
            Console.WriteLine("Исходная строка: {0}", message);
            Console.WriteLine("Защифрованная строка: {0}", sb);
        }
    }
}
/*
Исходная строка: Hello, world! It's the TEST_STRING!!!
Защифрованная строка: Hello, world! It's the TEST_STRING!!!
Исходная строка: Hello, world! It's the TEST_STRING!!!
Защифрованная строка: Tqxx{8,${~xp-,U!3 ,!tq,`Q_`k_`^UZS---
*/