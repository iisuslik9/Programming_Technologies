using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public static class Queries
    {
        //Количество резервуаров и установок

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

        //Найти установку по имени резервуара

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

        //Найти завод по установке

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

        //Общий объем

        public static int GetTotalVolumeQuery(Tank[] tanks)
        {
            var total = (from tank in tanks select tank.Volume).Sum();
            return total;
        }

        public static int GetTotalVolumeMethod(Tank[] tanks)
        {
            return tanks.Sum(t => t.Volume);
        }

        //Общая максимальная загрузка резервуаров

        public static int GetTotalMaxVolumeQuery(Tank[] tanks)
        {
            var totalMax = (from tank in tanks select tank.MaxVolume).Sum();
            return totalMax;
        }

        public static int GetTotalMaxVolumeMethod(Tank[] tanks)
        {
            return tanks.Sum(t => t.MaxVolume);
        }

        // Поиск резервуаров по имени

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
    }
}
