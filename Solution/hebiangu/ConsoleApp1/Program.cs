using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                List<int> collection = new List<int>();
                while (true)
                {
                    var value = Console.ReadLine();

                    int p1 = int.Parse(value);

                    if (p1 == -999) break;

                    collection.Add(p1);



                }

                Console.Clear();

                foreach (var p1 in collection)
                {
                    if (p1 < 2)
                    {
                        Console.WriteLine(p1 * 20);
                    }
                    else
                    {
                        Console.WriteLine((p1 - 2) * 50 + 40);
                    }
                }
            }



            //            Console.WriteLine(value);


            //            Console.Read();


            //            int[] array = { 3,4
            //,14
            //,0
            //,0
            //,6
            //,2
            //,5
            //};

            //            int[] array1 = { 10, 10, 10, 10, 10, 10, 10, 10 };


            //            //SumCloum(array, array1);

            //            Methed(array);


            //            Console.Read();
        }


        public static int Sum(int param1, int param2)
        {
            return param1 + param2;
        }

        public static void SumCloum(int[] array, int[] array1)
        {

            for (int i = 0; i < array.Length; i++)
            {
                int p1 = array[i];

                int p2 = array1[i];

                if (p1 < 3)
                {

                }
                else
                {
                    int reslt = Sum(p1, p2);

                    Console.WriteLine(reslt);
                }


            }
        }

        public static void Methed(int[] array)
        {

            for (int i = 0; i < array.Length; i++)
            {
                int p1 = array[i];


                if (p1 < 2)
                {
                    Console.WriteLine(p1 * 20);
                }
                else
                {
                    Console.WriteLine((p1 - 2) * 50 + 40);
                }


            }
        }

    }




}
