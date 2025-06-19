using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Margarita;

[ApiController]
[Route("api/orders")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly MargdbContext _context;

    public OrdersController(MargdbContext context)
    {
        _context = context;
    }

    // GET: api/orders/mine
    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
    {
        // TODO
        return await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Menu).ToListAsync();
    }

    // GET: api/orders/todo
    [HttpGet("todo")]
    public async Task<ActionResult<IEnumerable<Order>>> GetTodoOrders([FromQuery][DefaultValue("00000001-0001-0001-0001-000000000001")] Guid staff)
    {
        var usr = await _context.Staff.FindAsync(staff);
        if (usr == null)
        {
            return NotFound();
        }

        return await _context.Orders.AsNoTracking()
            .Include(o => o.Customer!).ThenInclude(c => c.Team)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Menu)
            .Where(o => o.TakenInChargeBy == null).OrderBy(o => o.CreatedDate).ToListAsync();
    }

    // GET: api/orders/one
    [HttpGet("one")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // POST: api/orders
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder([FromQuery][DefaultValue("cc68a50a-b1cd-4207-9230-c0cc534eb1ef")] Guid cust, [FromBody] List<OrderItem> orderItems)
    {
        /*
         * Exemple : api/orders?cust=cc68a50a-b1cd-4207-9230-c0cc534eb1ef
         * 
[
  {
    "menuId": "ae1052c7-0040-45e1-a41d-1fd97eedbf78",
    "amount": 18
  },
  {
    "menuId": "11f4fa39-8f27-4272-84e8-34cb2b09ad05",
    "amount": 19
  }
]

         * */
        var usr = await _context.Users.FindAsync(cust);
        if (usr == null)
        {
            return NotFound();
        }

        // always override
        var order = new Order();
        order.OrderItems = orderItems.Where(o => o.Amount > 0).ToList();
        order.CustomerId = cust;
        order.CreatedBy = cust;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMyOrders", order);
    }
}
