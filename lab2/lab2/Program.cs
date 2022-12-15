using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serialization
{
    public class dishs
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        [Key]
        public ordernum Id { get; set; }
        public dishs() { }
        public dishs(string name, string category, int price, ordernum id)
        {
            Name = name;
            Category = category;
            Price = price;
            Id = id;
        }



        public class ordernum
        {
            [Column("Id")]
            public int Id { get; set; }
            public int Number { get; set; }
            public int Quantity { get; set; }
            public ordernum() { }
            public ordernum(int id, int Number, int quantity)
            {
                Id = id;
                this.Number = Number;
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
            List<dishs> partsList = new List<dishs> ();

            JsonHandler<dishs> partsHandler = new JsonHandler<dishs>("Filedish.json");

            partsList.Add(new dishs("Pepperoni", "pizza", 550, new dishs.ordernum(1, 1, 2)));
            partsList.Add(new dishs("Sirnaya", "pizza", 500, new dishs.ordernum(2, 2, 1)));
            partsList.Add(new dishs("Myasnaya", "pizza", 600, new dishs.ordernum(3, 3, 3)));
            partsList.Add(new dishs("Potato", "snack", 150, new dishs.ordernum(6, 6, 2)));
            partsList.Add(new dishs("Limonad", "drink", 100, new dishs.ordernum(9, 7, 3)));

            partsHandler.Rewrite(partsList);
            partsHandler.OutputJsonContents();
        }
    }
}
