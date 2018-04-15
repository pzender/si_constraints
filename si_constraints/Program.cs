using System;

namespace si_constraints
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 1; i <= 13; i++)
            {
                Console.WriteLine(i + ":");
                N_Queens test = new N_Queens(i);
                test.StartBT();
                test.StartFC();
                Console.WriteLine("-------------------------------");
            }

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(i + ":");
                LatinSquare test = new LatinSquare(i);
                test.StartBT();
                test.StartFC();
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
