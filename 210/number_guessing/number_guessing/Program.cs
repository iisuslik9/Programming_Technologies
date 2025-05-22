using System;
using number_guessing.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;
using System.ComponentModel.DataAnnotations;
namespace number_guessing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                /////
                //Console.WriteLine("Введите минимальное значение диапазона:");
                //var minInput = Console.ReadLine();
                //Console.WriteLine("Введите максимальное значение диапазона:");
                //var maxInput = Console.ReadLine();

                //if (!int.TryParse(minInput, out var min) || !int.TryParse(maxInput, out var max))
                //{
                //    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целые числа.");
                //    continue;
                //}

                //if (max < min)
                //{
                //    Console.WriteLine("Максимальное значение должно быть больше или равно минимальному.");
                //    continue;
                //}

                //var validator = new RangeValidator(min, max);
                //var game = new Game(new ConsoleOutput(), validator);

                ///
                var services = new ServiceCollection()
                    .AddTransient<IOutput, ConsoleOutput>()
                    .AddTransient<IValidator<string>, NumberValidator>()
                    .AddTransient<IValidator<string>, RangeValidator>()
                    .AddSingletone<number_guessing.Game>();

                var validator = new RangeValidator(1, 100);
                //всместо создания объекта класса new consoleOutput связать интерфейс и класс через dependency injection
                //var game = new Game(new ConsoleOutput(), validator);
                var game = IServiceProvider.GetService<Game>();
                game.UpdateRange(9, 5);
                game.Play();

                Console.WriteLine("Do you want to play again? y for yes");
                var input = Console.ReadLine().ToLower();

                playAgain = input == "y";
            }
        }
    }
}
