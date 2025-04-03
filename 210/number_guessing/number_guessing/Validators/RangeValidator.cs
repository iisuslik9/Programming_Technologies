using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_guessing.Validators
{
    public class RangeValidator : IValidator<int>
    {
        private int _min;
        private int _max;

        public RangeValidator(int min, int max)
        {
            if (max > min)
            {
                _min = min;
                _max = max;
            }               
        }

        public int Min
        {
            get => _min;
            set
            {
                if (value < _max)
                    _min = value;
            }
        }

        public int Max
        {
            get => _max;
            set
            {
                if (value > _min)
                    _max = value;
            }
        }
        //метод проверки числа
        public ValidationResult Validate(int input)
        {
            if (input < _min || input > _max)
                return new ValidationResult(false, $"Number must be between {_min}-{_max}");
            return new ValidationResult(true, string.Empty);
        }
    }
}
