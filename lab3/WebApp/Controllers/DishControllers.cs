using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Route("api/DataBase")]
    [ApiController]
    public class DishsController : Controller
    {
        private readonly DishContext ContextDish;

        public DishsController(DishContext contextDish)
        {
            ContextDish = contextDish;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishs()
        {
            return await ContextDish.Dishs.ToListAsync();
        }


        [HttpGet("GetDish/{id}")]
        public async Task<ActionResult<Dish>> GetClient(int id)
        {
            var dish = await ContextDish.Dishs.FindAsync(id);
            if (dish == null) return NotFound();
            return dish;
        }

        [HttpGet("GetOrder_tab/{id}")]
        public async Task<ActionResult<Order_tab>> GetOrder_tab(int id)
        {
            var oreder_tab = await ContextDish.Order_tabs.FindAsync(id);
            if (oreder_tab == null) return NotFound();
            return oreder_tab;
        }

        [HttpPost("PostDish")] 
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        { 
            ContextDish.Dishs.Add(dish);
            await ContextDish.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetDishs), new { id = dish.Id}, dish);
        }

        [HttpPost("PostOrder_tab")] 
        public async Task<ActionResult<Order_tab>> PostOrder_tab(Order_tab order_tab)
        {
            ContextDish.Order_tabs.Add(order_tab);
            await ContextDish.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDishs), new { id = order_tab.Id }, order_tab);
        }
        
        [HttpPut("PutDish/{id}")] 
        public async Task<IActionResult> PutDish([FromQuery] int id, [FromBody] Dish dish)
        {
            return id == dish.Id ? NoContent() : BadRequest();
        }

        [HttpPut("PutOrder_tab/{id}")] 
        public async Task<IActionResult> PutOrder_tab([FromQuery] int id, [FromBody] Order_tab order_tab)
        {
            return id == order_tab.Id ? NoContent() : BadRequest();
        }

        [HttpDelete("DeleteDish/{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            if (ContextDish.Dishs == null) return NotFound();

            var dish = await ContextDish.Dishs.FindAsync(id);
            if (dish == null) return NotFound();

            ContextDish.Dishs.Remove(dish);
            await ContextDish.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteOrder_tab/{id}")]
        public async Task<IActionResult> DeleteOrder_tab(int id)
        {
            if (ContextDish.Order_tabs == null) return NotFound();

            var order_tab = await ContextDish.Order_tabs.FindAsync(id);
            if (order_tab == null) return NotFound();

            ContextDish.Order_tabs.Remove(order_tab);
            await ContextDish.SaveChangesAsync();

            return NoContent();
        }
    }
}
