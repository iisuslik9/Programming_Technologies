using System;

namespace pract_1
{
    class Program
    {
        static void Main(string[] args)
        {
            SumOfEvenDigitsOfNums(-100, -1);
        }

        static void SumOfEvenDigitsOfNums(int left, int right)
        {
            int sum = 0;
            for (int i = left; i <= right; i++)
            {
                sum += SumOfEvenDigit(i);
            }
            Console.WriteLine(sum);
        }
        static int SumOfEvenDigit(int num)
        {
            int sum = 0, digit =0;
            num = Math.Abs(num);
            for(int i = 0; i < 3; i++)
            {
                digit = num % 10;
                if (digit % 2 == 0)
                {
                    sum += digit;
                }
                num = num / 10;             
            }
            return sum;
        }
    }
}
//поляб методы свойства(преф сет гет) делегаты конструкторы диструкторы события
//репозииторий технологии программирования практики(коммит каждой практики) домашки
