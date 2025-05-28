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

        public static IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
        {
            return deals
                .Where(d => d.Sum >= 100)
                .OrderBy(d => d.Date)
                .Take(5)
                .OrderByDescending(d => d.Sum)
                .Select(d => d.Id)
                .ToList();
        }
        public record SumByMonth(DateTime Month, int Sum);

        public static IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
        {
            return deals
                .GroupBy(d => new DateTime(d.Date.Year, d.Date.Month, 1))
                .Select(g => new SumByMonth(g.Key, g.Sum(d => d.Sum)))
                .OrderBy(s => s.Month)
                .ToList();
        }

        static void Main(string[] args)
        {
            string jsonFilePath = "../../../JSON_sample_1.json";


            string json = File.ReadAllText(jsonFilePath);

            List<Deal>deals = JsonSerializer.Deserialize<List<Deal>>(json);
            

            if (deals == null || deals.Count == 0)
            {
                Console.WriteLine("Сделки не найдены в файле.");
                return;
            }

           
            var dealIds = GetNumbersOfDeals(deals);
            Console.WriteLine($"Найдено сделок: {dealIds.Count}");
            Console.WriteLine("Идентификаторы сделок (отсортированы по сумме по убыванию):");
            Console.WriteLine(string.Join(", ", dealIds));

            var sumsByMonth = GetSumsByMonth(deals);
            Console.WriteLine("\nСуммы сделок по месяцам:");
            foreach (var item in sumsByMonth)
            {
                Console.WriteLine($"{item.Month:yyyy-MM} - {item.Sum}");
            }
        }
    }
    
}
