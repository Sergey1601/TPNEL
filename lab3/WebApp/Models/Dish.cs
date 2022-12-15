using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }
        public int Price { get; set; }

        public Order_tab Order_tab { get; set; }

        public Dish() { }

        public Dish(int id, string title, string category, int price, Order_tab order_tab) 
        {
            Id = id;
            this.Title = title;
            this.Category = category;
            this.Order_tab = order_tab;
            this.Price = price;
        }
    }
}
