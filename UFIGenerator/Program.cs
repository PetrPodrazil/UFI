using System;

namespace UFIGenerator
{
    class Program
    {static void Main(string[] args)
        {
            //Generate example UFI
            Console.WriteLine(UFI.GenerateUFI("CZ978563421", 0));
            Console.WriteLine(UFI.GenerateUFI("CZ978563421", 15790899));
            Console.WriteLine(UFI.GenerateUFI("DE112358132", 134217728));
            Console.ReadKey();
        }
    }
}
