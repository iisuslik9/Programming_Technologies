using System;

namespace lesson_0
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO переопределить класс tostring чтобы вывести инф-ию о человеке в строку
            Person person = new(161, 50, DateTime.Parse("12.12.2012"), "sdf", "ffh");
            Console.WriteLine();

        }
    }
}
