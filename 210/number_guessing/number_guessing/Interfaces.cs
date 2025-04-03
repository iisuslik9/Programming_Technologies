using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_guessing
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T input);
    }

    public interface IOutput
    {
        //void Write(string message);
        void WriteLine(string message);
        string ReadLine();
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        
        //конструктор
        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}
