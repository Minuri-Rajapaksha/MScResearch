using System;

namespace CalaisPhpToCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenCalais obj = new OpenCalais();
            obj.getEntities();
            Console.WriteLine("Hello World!");
        }
    }
}
