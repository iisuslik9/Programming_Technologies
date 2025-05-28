using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public static class Queries
    {
    

        public static (int tanksCount, int unitsCount) GetCountsQuery(Tank[] tanks, Unit[] units)
        {
            var tanksCount = (from t in tanks select t).Count();
            var unitsCount = (from u in units select u).Count();
            return (tanksCount, unitsCount);
        }

        public static (int tanksCount, int unitsCount) GetCountsMethod(Tank[] tanks, Unit[] units)
        {
            return (tanks.Length, units.Length);
        }

       

        public static Unit FindUnitByTankNameQuery(Unit[] units, Tank[] tanks, string tankName)
        {
            var query = from tank in tanks
                        where tank.Name.Equals(tankName, StringComparison.OrdinalIgnoreCase)
                        join unit in units on tank.UnitId equals unit.Id
                        select unit;
            return query.FirstOrDefault();
        }

        public static Unit FindUnitByTankNameMethod(Unit[] units, Tank[] tanks, string tankName)
        {
            return tanks.Where(t => t.Name.Equals(tankName, StringComparison.OrdinalIgnoreCase))
                        .Join(units, t => t.UnitId, u => u.Id, (t, u) => u)
                        .FirstOrDefault();
        }

  

        public static Factory FindFactoryByUnitQuery(Factory[] factories, Unit unit)
        {
            var query = from factory in factories
                        where factory.Id == unit.FactoryId
                        select factory;
            return query.FirstOrDefault();
        }

        public static Factory FindFactoryByUnitMethod(Factory[] factories, Unit unit)
        {
            return factories.FirstOrDefault(f => f.Id == unit.FactoryId);
        }



        public static int GetTotalVolumeQuery(Tank[] tanks)
        {
            var total = (from tank in tanks select tank.Volume).Sum();
            return total;
        }

        public static int GetTotalVolumeMethod(Tank[] tanks)
        {
            return tanks.Sum(t => t.Volume);
        }


        public static int GetTotalMaxVolumeQuery(Tank[] tanks)
        {
            var totalMax = (from tank in tanks select tank.MaxVolume).Sum();
            return totalMax;
        }

        public static int GetTotalMaxVolumeMethod(Tank[] tanks)
        {
            return tanks.Sum(t => t.MaxVolume);
        }



        public static List<Tank> FindTanksByNameQuery(Tank[] tanks, string searchTerm)
        {
            var query = from tank in tanks
                        where tank.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                        select tank;
            return query.ToList();
        }

        public static List<Tank> FindTanksByNameMethod(Tank[] tanks, string searchTerm)
        {
            return tanks.Where(t => t.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public static void PrintAllTanksInfoQuery(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            Console.WriteLine("Список всех резервуаров с цехами и фабриками:");

            var query = from tank in tanks
                        join unit in units on tank.UnitId equals unit.Id into unitGroup
                        from unit in unitGroup.DefaultIfEmpty()
                        join factory in factories on unit?.FactoryId equals factory.Id into factoryGroup
                        from factory in factoryGroup.DefaultIfEmpty()
                        select new { tank, unit, factory };

            foreach (var item in query)
            {
                Console.WriteLine($"Резервуар: {item.tank.Name} (Объем: {item.tank.Volume}/{item.tank.MaxVolume})");
                Console.WriteLine($"  Установка: {item.unit?.Name ?? "Не найден"}");
                Console.WriteLine($"  Завод: {item.factory?.Name ?? "Не найден"}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public static void PrintAllTanksInfoMethod(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            Console.WriteLine("Список всех резервуаров с цехами и фабриками:");

            var list = tanks.GroupJoin(units,
                                      tank => tank.UnitId,
                                      unit => unit.Id,
                                      (tank, unitGroup) => new { tank, unit = unitGroup.FirstOrDefault() })
                            .GroupJoin(factories,
                                       tu => tu.unit?.FactoryId,
                                       factory => factory.Id,
                                       (tu, factoryGroup) => new { tu.tank, tu.unit, factory = factoryGroup.FirstOrDefault() });

            foreach (var item in list)
            {
                Console.WriteLine($"Резервуар: {item.tank.Name} (Объем: {item.tank.Volume}/{item.tank.MaxVolume})");
                Console.WriteLine($"  Установка: {item.unit?.Name ?? "Не найден"}");
                Console.WriteLine($"  Завод: {item.factory?.Name ?? "Не найден"}");
                Console.WriteLine(new string('-', 40));
            }
        }

   

        public static List<Tank> FindTankByNameQuery(Tank[] tanks, string searchTerm)
        {
            var query = from tank in tanks
                        where tank.Name.IndexOf(searchTerm ?? "", StringComparison.OrdinalIgnoreCase) >= 0
                        select tank;
            return query.ToList();
        }

        public static List<Tank> FindTankByNameMethod(Tank[] tanks, string searchTerm)
        {
            return tanks.Where(t => t.Name.IndexOf(searchTerm ?? "", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }


        public static void PrintFoundTanksInfoQuery(List<Tank> foundTanks, Unit[] units, Factory[] factories)
        {
            if (foundTanks == null || foundTanks.Count == 0)
            {
                Console.WriteLine("Резервуары не найдены.");
                return;
            }

            var query = from tank in foundTanks
                        join unit in units on tank.UnitId equals unit.Id into unitGroup
                        from unit in unitGroup.DefaultIfEmpty()
                        join factory in factories on unit?.FactoryId equals factory.Id into factoryGroup
                        from factory in factoryGroup.DefaultIfEmpty()
                        select new { tank, unit, factory };

            foreach (var item in query)
            {
                Console.WriteLine($"Резервуар: {item.tank.Name}, Описание: {item.tank.Description}, Объем: {item.tank.Volume} / {item.tank.MaxVolume}");
                Console.WriteLine($"Установка: {item.unit?.Name ?? "Не найден"}, Завод: {item.factory?.Name ?? "Не найден"}");
                Console.WriteLine(new string('-', 40));
            }
        }

        public static void PrintFoundTanksInfoMethod(List<Tank> foundTanks, Unit[] units, Factory[] factories)
        {
            if (foundTanks == null || foundTanks.Count == 0)
            {
                Console.WriteLine("Резервуары не найдены.");
                return;
            }

            var list = foundTanks.GroupJoin(units,
                                           tank => tank.UnitId,
                                           unit => unit.Id,
                                           (tank, unitGroup) => new { tank, unit = unitGroup.FirstOrDefault() })
                                 .GroupJoin(factories,
                                            tu => tu.unit?.FactoryId,
                                            factory => factory.Id,
                                            (tu, factoryGroup) => new { tu.tank, tu.unit, factory = factoryGroup.FirstOrDefault() });

            foreach (var item in list)
            {
                Console.WriteLine($"Резервуар: {item.tank.Name}, Описание: {item.tank.Description}, Объем: {item.tank.Volume} / {item.tank.MaxVolume}");
                Console.WriteLine($"Установка: {item.unit?.Name ?? "Не найден"}, Завод: {item.factory?.Name ?? "Не найден"}");
                Console.WriteLine(new string('-', 40));
            }
        }

    }
}
