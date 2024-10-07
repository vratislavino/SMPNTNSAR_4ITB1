using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMPNTNSAR
{
    public class SaveLoadManager
    {
        public async Task<List<Shape>> LoadShapes(string path)
        {
            var stringToLoad = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<Shape>>(stringToLoad);
        }

        public async Task SaveShapes(string path, List<Shape> shapes)
        {
            var stringToSave = JsonConvert.SerializeObject(shapes);
            await File.WriteAllTextAsync(path, stringToSave);
        }

    }
}
