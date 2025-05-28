using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Program
    {
        static void Main(string[] args)
        {
            var tanks = GetTanks();
            var units = GetUnits();
            var factories = GetFactories();

            bool exit = false;

            while (!exit)
            {
                //Console.Clear();
                Console.WriteLine("=== меню ===");
                Console.WriteLine("1. Количество резервуаров и кстановок");
                Console.WriteLine("2. Установка резервуара 2");
                Console.WriteLine("3. Общий объем резервуаров");
                Console.WriteLine("4. Общая сумма загрузки всех резервуаров");
                Console.WriteLine("5. Информация о всех резервуарах");
                Console.WriteLine("6. Поиск по имени");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Выбран 1");
                        Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");
                        break;
                    case "2":
                        Console.WriteLine("Выбран 2");
                        var foundUnit = FindUnit(units, tanks, "Резервуар 2");
                        var factory = FindFactory(factories, foundUnit);
                        Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
                        break;
                    case "3":
                        Console.WriteLine("Выбран 3");
                        var totalVolume = GetTotalVolume(tanks);
                        Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
                        break;
                    case "4":
                        Console.WriteLine("Выбран 4");
                        var totalMaxVolume = GetTotalMaxVolume(tanks);
                        Console.WriteLine($"Общая сумма загрузки всех резервуаров: {totalMaxVolume}");
                        break;
                    case "5":
                        Console.WriteLine("Выбран 5");
                        PrintAllTanksInfo(tanks, units, factories);
                        break;
                    case "6":
                        Console.WriteLine("Выбран 5");
                        Console.WriteLine("Введите название резервуара для поиска:");
                        string inputName = Console.ReadLine();
                        List<Tank> foundTanks = FindTankByName(tanks, inputName);
                        PrintFoundTanksInfo(foundTanks, units, factories);
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

        // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенным таблицам
        // можно использовать создание объектов прямо в C# коде через new, или читать из файла (на своё усмотрение)
        public static Tank[] GetTanks()
        {
            return new Tank[]
            {
                new Tank { Id = 1, Name = "Резервуар 1", Description = "Надземный - вертикальный", Volume = 1500, MaxVolume = 2000, UnitId = 1 },
                new Tank { Id = 2, Name = "Резервуар 2", Description = "Надземный - горизонтальный", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
                new Tank { Id = 3, Name = "Дополнительный резервуар 24", Description = "Надземный - горизонтальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 4, Name = "Резервуар 35", Description = "Надземный - вертикальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 5, Name = "Резервуар 47", Description = "Подземный - двустенный", Volume = 4000, MaxVolume = 5000, UnitId = 2 },
                new Tank { Id = 6, Name = "Резервуар 256", Description = "Подводный", Volume = 500, MaxVolume = 500, UnitId = 3 }
            };
        }
        // реализуйте этот метод, чтобы он возвращал массив установок, согласно приложенным таблицам
        public static Unit[] GetUnits()
        {
            return new Unit[]
            {
                new Unit { Id = 1, Name = "ГФУ-2", Description = "Газофракционирующая установка", FactoryId = 1 },
                new Unit { Id = 2, Name = "АВТ-6", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 1 },
                new Unit { Id = 3, Name = "АВТ-10", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 2 }
            };
        }
        // реализуйте этот метод, чтобы он возвращал массив заводов, согласно приложенным таблицам
        public static Factory[] GetFactories()
        {
            return new[]
            {
                new Factory { Id = 1, Name = "НПЗ№1", Description = "Первый нефтеперерабатывающий завод" },
                new Factory { Id = 2, Name = "НПЗ№2", Description = "Второй нефтеперерабатывающий завод" }
            };
        }

        // реализуйте этот метод, чтобы он возвращал установку (Unit), которой
        // принадлежит резервуар (Tank), найденный в массиве резервуаров по имени
        // учтите, что по заданному имени может быть не найден резервуар
        public static Unit FindUnit(Unit[] units, Tank[] tanks, string tankName)
        {
            foreach (Tank tank in tanks)
            {
                if (tank.Name.Equals(tankName, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (Unit unit in units)
                    {
                        if (unit.Id == tank.UnitId)
                            return unit;
                    }
                }
            }
            return null;
        }

        // реализуйте этот метод, чтобы он возвращал объект завода, соответствующий установке
        public static Factory FindFactory(Factory[] factories, Unit unit)
        {
            if (unit == null)
                return null;

            foreach (Factory factory in factories)
            {
                if (factory.Id == unit.FactoryId)
                    return factory;
            }

            return null;
        }

        // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
        public static int GetTotalVolume(Tank[] tanks)
        {
            int totalVolume = 0;
            foreach (var tank in tanks)
            {
                totalVolume += tank.Volume;
            }
            return totalVolume;
        }

        /// <summary>
        /// Метод общей суммы загрузки всех резервуаров
        /// </summary>
        /// <param name="tanks">Массив резервуаров</param>
        /// <returns>Общая сумма загрузки всех резервуаров</returns>
        public static int GetTotalMaxVolume(Tank[] tanks)
        {
            int totalMaxVolume = 0;
            foreach (var tank in tanks)
            {
                totalMaxVolume += tank.MaxVolume;
            }
            return totalMaxVolume;
        }

        
        public static void PrintAllTanksInfo(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            
            Console.WriteLine("Список всех резервуаров с цехами и фабриками:");

            foreach (var tank in tanks)
            {
                var unit = Array.Find(units, u => u.Id == tank.UnitId);

                var factory = unit != null ? Array.Find(factories, f => f.Id == unit.FactoryId) : null;

                Console.WriteLine($"Резервуар: {tank.Name} (Объем: {tank.Volume}/{tank.MaxVolume})");
                Console.WriteLine($"  Установка: {unit?.Name ?? "Не найден"}");
                Console.WriteLine($"  Завод: {factory?.Name ?? "Не найден"}");
                Console.WriteLine(new string('-', 40));
            }           
        }
    
        public static List<Tank> FindTankByName(Tank[] tanks, string searchTerm)
        {
            List<Tank> results = new List<Tank>();

            foreach (Tank tank in tanks)
            {
                if (tank.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(tank);
                }
            }

            return results;
        }

        // Печать информации о резервуаре с указанием цеха (установки) и фабрики
        public static void PrintFoundTanksInfo(List<Tank> foundTanks, Unit[] units, Factory[] factories)
        {
            if (foundTanks == null || foundTanks.Count == 0)
            {
                Console.WriteLine("Резервуары не найдены.");
                return;
            }


            foreach (var tank in foundTanks)
            {
                var unit = Array.Find(units, u => u.Id == tank.UnitId);
                var factory = unit != null ? Array.Find(factories, f => f.Id == unit.FactoryId) : null;

                Console.WriteLine($"Резервуар: {tank.Name}, Описание: {tank.Description}, Объем: {tank.Volume} / {tank.MaxVolume},\n" +
                $"Установка: {unit?.Name ?? "Не найден"}, Завод: {factory?.Name ?? "Не найден"}");

                Console.WriteLine(new string('-', 40));
            }
        }


    }


}
