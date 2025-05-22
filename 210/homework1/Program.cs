using System;
using System.Linq;
using System.Threading.Tasks;

namespace homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tanks = GetTanks();
            var units = GetUnits();
            var factories = GetFactories();

            Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");

            var foundUnit = FindUnit(units, tanks, "Резервуар 2");
           
            var factory = FindFactory(factories, foundUnit);
            Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
            

            var totalVolume = GetTotalVolume(tanks);
            Console.WriteLine($"Общий объем резервуаров: {totalVolume}");

            var totalMaxVolume = GetTotalMaxVolume(tanks);
            Console.WriteLine($"Общая сумма загрузки всех резервуаров: {totalMaxVolume}");

            PrintAllTanksWithDetails(tanks, units, factories); // Вывод всех резервуаров

            // Поиск резервуара
            Console.WriteLine("\nВведите название резервуара для поиска:");
            string searchTerm = Console.ReadLine();

            List<Tank> searchResults = SearchTanksByName(tanks, searchTerm);

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Результаты поиска:");
                foreach (Tank tank in searchResults)
                {
                    Console.WriteLine($"- {tank.Name}, описание: {tank.Description}, заполненный объем: {tank.Volume}," +
                        $" максимальный объем: {tank.MaxVolume}");
                }
            }
            else
            {
                Console.WriteLine("Резервуары не найдены.");
            }
        }

        /// <summary>
        /// Метод поиска информации об резервуарах
        /// по наименованию резервуара
        /// </summary>
        /// <param name="tanks"></param>
        /// <param name="searchTerm"></param>
        /// <returns>result</returns>
        public static List<Tank> SearchTanksByName(Tank[] tanks, string searchTerm)
        {
            List<Tank> results = new List<Tank>();
            string searchTermLower = searchTerm.ToLower();

            foreach (Tank tank in tanks)
            {
                if (tank.Name.ToLower() == searchTermLower)
                {
                    results.Add(tank);
                }
            }

            return results;
        }
    

        /// <summary>
        /// Метод, который возвращает массив резервуаров
        /// </summary>
        /// <returns>массив Tanks</returns>
        public static Tank[] GetTanks()
        {
            return new[]
            {
                new Tank { Id = 1, Name = "Резервуар 1", Description = "Надземный - вертикальный", Volume = 1500, MaxVolume = 2000, UnitId = 1 },
                new Tank { Id = 2, Name = "Резервуар 2", Description = "Надземный - горизонтальный", Volume = 2500, MaxVolume = 3000, UnitId = 1 },
                new Tank { Id = 3, Name = "Дополнительный резервуар 24", Description = "Надземный - горизонтальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 4, Name = "Резервуар 35", Description = "Надземный - вертикальный", Volume = 3000, MaxVolume = 3000, UnitId = 2 },
                new Tank { Id = 5, Name = "Резервуар 47", Description = "Подземный - двустенный", Volume = 4000, MaxVolume = 5000, UnitId = 2 },
                new Tank { Id = 6, Name = "Резервуар 256", Description = "Подводный", Volume = 500, MaxVolume = 500, UnitId = 3 }
            };
        }

        /// <summary>
        /// Метод, возвращающий массив установок
        /// </summary>
        /// <returns>массив Units</returns>
        public static Unit[] GetUnits()
        {
            return new[]
            {
                new Unit { Id = 1, Name = "ГФУ-2", Description = "Газофракционирующая установка", FactoryId = 1 },
                new Unit { Id = 2, Name = "АВТ-6", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 1 },
                new Unit { Id = 3, Name = "АВТ-10", Description = "Атмосферно-вакуумная трубчатка", FactoryId = 2 }
            };
        }

        /// <summary>
        /// Метод, возвращающий массив заводов
        /// </summary>
        /// <returns>массив Factories</returns>
        public static Factory[] GetFactories()
        {
            return new[]
            {
                new Factory { Id = 1, Name = "НПЗ№1", Description = "Первый нефтеперерабатывающий завод" },
                new Factory { Id = 2, Name = "НПЗ№2", Description = "Второй нефтеперерабатывающий завод" }
            };
        }

        /// Метод нахождения установки по резервуару
        /// </summary>
        /// <param name="units">Массив установок</param>
        /// <param name="tanks">Массив резервуаров</param>
        /// <param name="tankName">Имя резервуара</param>
        /// <returns>Установка, которой принадлежит резервуар</returns>
        public static Unit FindUnit(Unit[] units, Tank[] tanks, string tankName)
        {
            foreach (Tank tank in tanks)
            {
                if (tank.Name.ToLower() == tankName.ToLower())
                {
                    foreach (Unit unit in units)
                    {
                        if (unit.Id == tank.UnitId)
                        {
                            return unit;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Метод нахождения завода по установке
        /// </summary>
        /// <param name="factories">Массив заводов</param>
        /// <param name="unit">Установка</param>
        /// <returns>Завод, которому принадлежит установка</returns>
        public static Factory FindFactory(Factory[] factories, Unit unit)
        {
            // Ищем завод по ID установки
            foreach (var factory in factories)
            {
                if (factory.Id == unit.FactoryId)
                {
                    return factory;
                }
            }
            return null;
        }

        /// <summary>
        /// Метод для подсчета общего объема резервуаров
        /// (суммарный объем резервуаров в массиве)
        /// </summary>
        /// <param name="tanks">Массив резервуаров</param>
        /// <returns>Суммарный объем резервуаров</returns>
        public static int GetTotalVolume(Tank[] tanks)
        {
            int totalVolume = 0;
            // Суммируем объемы резервуаров
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

        /// <summary>
        /// Метод вывода всех резервуаров, включая имена
        /// цеха и фабрики, в которых они числятся
        /// </summary>
        /// <param name="tanks"></param>
        /// <param name="units"></param>
        /// <param name="factories"></param>
        public static void PrintAllTanksWithDetails(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            Console.WriteLine("\nВсе резервуары:");

            foreach (Tank tank in tanks)
            {
                Unit unit = null;
                foreach (Unit u in units)
                {
                    if (u.Id == tank.UnitId)
                    {
                        unit = u;
                        break;
                    }
                }

                Factory factory = null;
                if (unit != null)
                {
                    foreach (Factory f in factories)
                    {
                        if (f.Id == unit.FactoryId)
                        {
                            factory = f;
                            break;
                        }
                    }
                }

                string unitName = (unit != null) ? unit.Name : "Неизвестно";
                string factoryName = (factory != null) ? factory.Name : "Неизвестно";

                Console.WriteLine($"{tank.Name} (Цех: {unitName}, Фабрика(завод): {factoryName})");
            }
        }


    }

}