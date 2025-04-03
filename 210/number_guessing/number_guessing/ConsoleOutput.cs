using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number_guessing
{
    public class ConsoleOutput : IOutput
    {
        //public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
        //public string ReadLine() => Console.ReadLine();
        public string ReadLine() => Console.ReadLine() ?? string.Empty;

    }
}
