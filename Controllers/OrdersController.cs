using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Margarita;
using Margarita.Database;

namespace Margarita.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MargdbContext _context;

        public OrdersController(MargdbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Menu).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromQuery] Guid cust, [FromBody]Order order)
        {
            /*
             * Exemple :
             * 
{
    "customerId": "cc68a50a-b1cd-4207-9230-c0cc534eb1ef",
    "createdBy": "cc68a50a-b1cd-4207-9230-c0cc534eb1ef",
    "orderItems": [
      {
        "menuId": "ae1052c7-0040-45e1-a41d-1fd97eedbf78",
        "amount": 8
      },
      {
        "menuId": "11f4fa39-8f27-4272-84e8-34cb2b09ad05",
        "amount": 9
      }
    ]
}
             * */
            var usr = await _context.Users.FindAsync(cust);
            if (usr == null)
            {
                return NotFound();
            }

            order.CreatedBy = cust;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
    }
}
