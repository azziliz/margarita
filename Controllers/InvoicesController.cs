using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Margarita;

[ApiController]
[Route("api/invoices")]
[Produces("application/json")]
public class InvoicesController : ControllerBase
{
    private readonly MargdbContext _context;

    public InvoicesController(MargdbContext context)
    {
        _context = context;
    }

    // GET: api/invoices
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices([FromQuery] Guid staff)
    {
        return await _context.Invoices.ToListAsync();
    }

    // GET: api/invoices/mine?cust=cc68a50a-b1cd-4207-9230-c0cc534eb1ef
    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByTeam([FromQuery][DefaultValue("cc68a50a-b1cd-4207-9230-c0cc534eb1ef")] Guid cust)
    {
        var usr = await _context.Users.FindAsync(cust);
        if (usr == null)
        {
            return NotFound();
        }

        return await _context.Invoices.AsNoTracking().Include(i => i.InvoiceItems).ThenInclude(it => it.Menu).Where(i => i.CustomerId == cust).OrderByDescending(i => i.CreatedDate).ToListAsync();
    }

    // POST: api/invoices
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Invoice>> PostInvoice([FromQuery][DefaultValue("00000001-0001-0001-0001-000000000001")] Guid staff, [FromBody] Invoice invoice)
    {
        /*
         * Exemple : api/invoices?staff=00000001-0001-0001-0001-000000000001
         * 
{
"orderId": "1493db12-09fe-435a-bd51-3ef88ee68cc1",
"customerId": "cc68a50a-b1cd-4207-9230-c0cc534eb1ef",
"total": 25,
"invoiceItems": [
  {
    "menuId": "ae1052c7-0040-45e1-a41d-1fd97eedbf78",
    "amount": 18
  },
  {
    "menuId": "11f4fa39-8f27-4272-84e8-34cb2b09ad05",
    "amount": 19
  }
]
}
         * */
        var usr = await _context.Staff.FindAsync(staff);
        if (usr == null)
        {
            return NotFound();
        }

        // always override
        invoice.CreatedBy = staff;
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAllInvoices", invoice);
    }

    // DELETE: api/invoices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(Guid id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
