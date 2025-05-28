using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Tanks
{
    class Program
    {
        static void Main(string[] args)
        {
            string factoriesPath = "../../factory.json";
            string unitsPath = "../../unit.json";
            string tanksPath = "../../tank.json";

            Factory[] factories = DataLoader.LoadFactories(factoriesPath);
            Unit[] units = DataLoader.LoadUnits(unitsPath);
            Tank[] tanks = DataLoader.LoadTanks(tanksPath);
            Console.WriteLine("Данные загружены из файлов.");
            

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("=== меню ===");
                Console.WriteLine("1. Количество резервуаров и кстановок");
                Console.WriteLine("2. Установка резервуара 2");
                Console.WriteLine("3. Общий объем резервуаров");
                Console.WriteLine("4. Общая сумма загрузки всех резервуаров");
                Console.WriteLine("5. Информация о всех резервуарах");
                Console.WriteLine("6. Поиск по имени");
                Console.WriteLine("7. Сохранить все данные в JSON");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        
                        var counts = Queries.GetCountsMethod(tanks, units);
                        //var counts = Queries.GetCountsQuery(tanks, units);
                        Console.WriteLine($"Количество резервуаров: {counts.tanksCount}, установок: {counts.unitsCount}");
                        break;

                    case "2":
                        // var unit = Queries.FindUnitByTankNameQuery(units, tanks, "Резервуар 2");
                        var unit = Queries.FindUnitByTankNameMethod(units, tanks, "Резервуар 2");

                        // var factory = Queries.FindFactoryByUnitQuery(factories, unit);
                        var factory = Queries.FindFactoryByUnitMethod(factories, unit);

                        if (unit != null && factory != null)
                        {
                            Console.WriteLine($"Резервуар 2 принадлежит установке {unit.Name} и заводу {factory.Name}");
                        }
                        else
                        {
                            Console.WriteLine("Резервуар 2 или связанные данные не найдены.");
                        }
                        break;

                    case "3":
                        // int totalVolume = Queries.GetTotalVolumeQuery(tanks);
                        int totalVolume = Queries.GetTotalVolumeMethod(tanks);
                        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
                        break;

                    case "4":
                        // int totalMaxVolume = Queries.GetTotalMaxVolumeQuery(tanks);
                        int totalMaxVolume = Queries.GetTotalMaxVolumeMethod(tanks);
                        Console.WriteLine($"Общая максимальная загрузка резервуаров: {totalMaxVolume}");
                        break;

                    case "5":
                        Queries.PrintAllTanksInfoQuery(tanks, units, factories);

                       
                        // DataPrinter.PrintAllTanksInfoMethod(tanks, units, factories);
                        break;

                    case "6":
                        Console.Write("Введите название резервуара для поиска: ");
                        string inputName = Console.ReadLine();

                        var foundTanks = Queries.FindTankByNameMethod(tanks, inputName);
                        // var foundTanks = DataPrinter.FindTankByNameQuery(tanks, searchTerm);

                        // И для вывода найденных
                        Queries.PrintFoundTanksInfoMethod(foundTanks, units, factories);
                        // DataPrinter.PrintFoundTanksInfoQuery(foundTanks, units, factories);
                        break;

                    case "7":
                        try
                        {
                            
                            DataSaver.SaveDataToJson("../../data.json", factories, units, tanks);
                            Console.WriteLine("Данные сохранены в data.json");

                            string jsonFromFile = File.ReadAllText("../../data.json");

                            
                            Console.WriteLine("\nСодержимое файла:");
                            Console.WriteLine(jsonFromFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                        }
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте ещё раз.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                }
            }

        }

        


    }


}
