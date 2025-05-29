using number_guessing.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_guessing
{
    public class Game
    {
        private readonly IOutput _output;
        private readonly IRangeValidator _rangeValidator;
        private readonly Random _random = new();

        public Game(IOutput output, IRangeValidator rangeValidator)
        {
            _output = output;
            _rangeValidator = rangeValidator;
        }

        public void UpdateRange(int newMin, int newMax)
        {
            _rangeValidator.Min = newMin;
            _rangeValidator.Max = newMax;
        }

        public void Play()
        {
            var target = _random.Next(_rangeValidator.Min, _rangeValidator.Max + 1);
            _output.WriteLine($"Guess number between {_rangeValidator.Min}-{_rangeValidator.Max}");

            while (true)
            {
                var input = _output.ReadLine();
                var validation = ValidateInput(input);

                if (!validation.IsValid)
                {
                    _output.WriteLine(validation.Message);
                    continue;
                }

                var guess = int.Parse(input);
                if (guess == target)
                {
                    _output.WriteLine("Correct!");
                    break;
                }

                _output.WriteLine(guess > target ? "Too high" : "Too low");
            }
        }

        private ValidationResult ValidateInput(string input)
        {
            var numberValidator = new NumberValidator();
            var numberValidation = numberValidator.Validate(input);

            if (!numberValidation.IsValid)
                return numberValidation;

            var number = int.Parse(input);
            return _rangeValidator.Validate(number);
        }
    }

}

