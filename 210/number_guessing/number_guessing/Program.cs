using System;
using number_guessing.Validators;
namespace number_guessing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                var validator = new RangeValidator(1, 100);
                var game = new Game(new ConsoleOutput(), validator);

                game.UpdateRange(9, 5);
                game.Play();

                Console.WriteLine("Do you want to play again? y for yes");
                var input = Console.ReadLine().ToLower();

                playAgain = input == "y";
            }
        }
    }
}
