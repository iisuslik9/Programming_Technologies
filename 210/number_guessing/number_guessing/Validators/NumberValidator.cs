using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_guessing.Validators
{
    public class NumberValidator : IValidator<string>
    {
        public ValidationResult Validate(string input)
        {
            if (!int.TryParse(input, out _))
                return new ValidationResult(false, "Invalid number format");
            return new ValidationResult(true, string.Empty);
        }
    }

}
