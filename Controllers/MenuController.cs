using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Margarita;

[ApiController]
[Route("api/menu")]
[Produces("application/json")]
public class MenuController : ControllerBase
{
    private readonly MargdbContext _context;

    public MenuController(MargdbContext context)
    {
        _context = context;
    }

    // GET: api/menu
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
    {
        return await _context.Menus.AsNoTracking().Where(m => m.IsActive).ToListAsync();
    }

    // POST: api/menu
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(Menu menu)
    {
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    // DELETE: api/menu/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu == null)
        {
            return NotFound();
        }

        menu.IsActive = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
