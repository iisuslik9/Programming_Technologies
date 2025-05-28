using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace ParserJson
{
    internal class Program
    {
        public class Deal
        {
            public int Sum { get; set; }
            public string Id { get; set; }
            public DateTime Date { get; set; }
        }


        IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
        {
            //
        }
        record SumByMonth(DateTime Month, int Sum);

        IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
        {
            //
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
