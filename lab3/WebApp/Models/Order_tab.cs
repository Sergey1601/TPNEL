using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Order_tab
    {
        public int Number { get; set; }
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Order_tab() { }

        public Order_tab(int number, int id, int quantity)
        {
            Id = id;
            Number = number;
            Quantity = quantity;
        }
    }
}
