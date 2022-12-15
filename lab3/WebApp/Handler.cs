using System.Text.Json;

namespace WebApp
{
    public class Handler<Class> where Class : class
    {
        private string NameFile;

        JsonSerializerOptions options = new JsonSerializerOptions {WriteIndented = true};

        public Handler() { }

        public Handler(string nameFile)
        {
            NameFile = nameFile;
        }

        public void SetFileName(string nameFile)
        {
            NameFile = nameFile;
        }

        public void Write(List<Class> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            if (new FileInfo(NameFile).Length == 0)
            {
                File.WriteAllText(NameFile, jsonString);
            }
            else
            {
                Console.WriteLine("file path is not empty");
            }
        }

        public void Delete()
        {
            File.WriteAllText(NameFile, string.Empty);
        }

        public void Update(List<Class> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(NameFile, jsonString);
        }

        public void Read(ref List<Class> list)
        {
            if (File.Exists(NameFile))
            {
                if (new FileInfo(NameFile).Length != 0)
                {
                    string jsonString = File.ReadAllText(NameFile);
                    list = JsonSerializer.Deserialize<List<Class>>(jsonString);
                }
                else
                {
                    Console.WriteLine("file path is not empty");
                }
            }
        }

        public void OutputJsonContents()
        {
            string jsonString = File.ReadAllText(NameFile);

            Console.WriteLine(jsonString);
        }

        public void OutputSerializedList(List<Class> list)
        {
            Console.WriteLine(JsonSerializer.Serialize(list, options));
        }
    }
}
