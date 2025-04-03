using System;
using number_guessing.Validators;
namespace number_guessing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rangeValidator = new RangeValidator(1, 100);
            var game = new Game(new ConsoleOutput(), rangeValidator);

            // Change range dynamically
            game.UpdateRange(20, 80);
            game.Play();
        }
    }
}
