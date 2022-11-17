using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;


namespace Serialization
{
    public class blydo
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public ordernum Id { get; set; }
        public blydo() { }
        public blydo(string name, string category, int price, ordernum id)
        {
            Name = name;
            Category = category;
            Price = price;
            Id = id;
        }



        public class ordernum
        {
            public int Id { get; set; }
            public int Ordernumber { get; set; }
            public int Quantity { get; set; }
            public ordernum() { }
            public ordernum(int id, int ordernumber, int quantity)
            {
                Id = id;
                Ordernumber = ordernumber;
                Quantity = quantity;
            }

        }
    }

    public class JsonHandler<T> where T : class
    {
        private string NameFile;
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


        public JsonHandler() { }

        public JsonHandler(string NameFile)
        {
            this.NameFile = NameFile;
        }


        public void SetFileName(string NameFile)
        {
            this.NameFile = NameFile;
        }

        public void Write(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            if (new FileInfo(NameFile).Length == 0)
            {
                File.WriteAllText(NameFile, jsonString);
            }
            else
            {
                Console.WriteLine("Указанный путь к файлу не пустой");
            }
        }

        public void Delete()
        {
            File.WriteAllText(NameFile, string.Empty);
        }

        public void Rewrite(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(NameFile, jsonString);
        }

        public void Read(ref List<T> list)
        {
            if (File.Exists(NameFile))
            {
                if (new FileInfo(NameFile).Length != 0)
                {
                    string jsonString = File.ReadAllText(NameFile);
                    list = JsonSerializer.Deserialize<List<T>>(jsonString);
                }
                else
                {
                    Console.WriteLine("Указанный путь к файлу не пустой");
                }
            }
        }

        public void OutputJsonContents()
        {
            string jsonString = File.ReadAllText(NameFile);

            Console.WriteLine(jsonString);
        }

        public void OutputSerializedList(List<T> list)
        {
            Console.WriteLine(JsonSerializer.Serialize(list, options));
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            List< blydo> partsList = new List<blydo> ();

            JsonHandler<blydo> partsHandler = new JsonHandler<blydo>("FileBlydo.json");

            partsList.Add(new blydo("Pepperoni", "pizza", 550, new blydo.ordernum(1, 1, 2)));
            partsList.Add(new blydo("Sirnaya", "pizza", 500, new blydo.ordernum(2, 2, 1)));
            partsList.Add(new blydo("Myasnaya", "pizza", 600, new blydo.ordernum(3, 3, 3)));
            partsList.Add(new blydo("Potato", "snack", 150, new blydo.ordernum(6, 6, 2)));
            partsList.Add(new blydo("Limonad", "drink", 100, new blydo.ordernum(9, 7, 3)));

            partsHandler.Rewrite(partsList);
            partsHandler.OutputJsonContents();
        }
    }
}
