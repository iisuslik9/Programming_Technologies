using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Tanks
{
    public static class DataSaver
    {
        public static void SaveDataToJson(string path, Factory[] factories, Unit[] units, Tank[] tanks)
        {

            var data = new
            {
                Factories = factories,
                Units = units,
                Tanks = tanks
            };
            // Настройки сериализации для корректного вывода кириллицы
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }
    }
}
