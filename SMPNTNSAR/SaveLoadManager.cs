using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
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

        public List<Shape> LoadShapesNonAsync(string path)
        {
            var stringToLoad = File.ReadAllText(path);

            var dtos = JsonConvert.DeserializeObject<List<Shape.ShapeDTO>>(stringToLoad);
            var shapes = dtos.Select(dto => 
                Activator.CreateInstance(dto.shapeType, dto) as Shape
            ).ToList();

            return shapes;
        }

        public async Task SaveShapes(string path, IEnumerable<Shape> shapes)
        {
            var stringToSave = JsonConvert.SerializeObject(shapes.Select(s => s.GetDTO()));
            await File.WriteAllTextAsync(path, stringToSave);
        }

        public void SaveShapesNonAsync(string path, IEnumerable<Shape> shapes)
        {
            var stringToSave = JsonConvert.SerializeObject(shapes.Select(s => s.GetDTO()));
            File.WriteAllText(path, stringToSave);
        }

        public void CopyDllToAppData(string path)
        {
            var finalPath = "";

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDataPath = Path.Combine(appDataPath, "MyNewShapes_4ITB1");
            if(!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            finalPath = Path.Combine(appDataPath, Path.GetFileName(path));
            
            Debug.WriteLine(finalPath);
            File.Copy(path, finalPath);
        }


    }
}
