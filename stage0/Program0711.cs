using System;

namespace Targil0
{
    partial class Program
    {
        private static void Main(string[] arg)
        {

            welcome0711();
            Welcome8917();
        }

        private static void welcome0711()
        {
            Console.WriteLine("Enter your name: ");
            string des = Console.ReadLine();
            Console.WriteLine(@"{0}, welcome to my first console appliction", des);
        }

        static partial void Welcome8917();
    }
}