using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Margarita;
using System.ComponentModel;

namespace Margarita;

[ApiController]
[Route("api/staff")]
[Produces("application/json")]
public class StaffsController : ControllerBase
{
    private readonly MargdbContext _context;

    public StaffsController(MargdbContext context)
    {
        _context = context;
    }

    // GET: api/Staffs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
    {
        return await _context.Staff.ToListAsync();
    }

    // GET: api/Staffs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Staff>> GetStaff([DefaultValue("00000001-0001-0001-0001-000000000001")] Guid id)
    {
        var staff = await _context.Staff.FindAsync(id);

        if (staff == null)
        {
            return NotFound();
        }

        return staff;
    }

    // POST: api/Staffs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Staff>> PostStaff(Staff staff)
    {
        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
    }

    // DELETE: api/Staffs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStaff(Guid id)
    {
        var staff = await _context.Staff.FindAsync(id);
        if (staff == null)
        {
            return NotFound();
        }

        _context.Staff.Remove(staff);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
