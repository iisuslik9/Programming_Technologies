using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tanks
{
    public static class DataLoader
    {
        public static Factory[] LoadFactories(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Factory[]>(jsonString);
        }

        public static Unit[] LoadUnits(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Unit[]>(jsonString);
        }

        public static Tank[] LoadTanks(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Tank[]>(jsonString);
        }

    }
}
