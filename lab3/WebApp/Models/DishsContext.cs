using WebApp.Controllers;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class DishContext:DbContext
    {
        public DishContext(DbContextOptions<DishContext> options) : base(options) {}

        public DbSet<Dish> Dishs { get; set; } = null!;
        public DbSet<Order_tab> Order_tabs { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
