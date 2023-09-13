

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase {

    private readonly BankingContext _context;

    public CustomersController(BankingContext context) {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers() {
        return await _context.Customers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id) {
        var cust = await _context.Customers
                                    .Include(x => x.Accounts)
                                    .SingleOrDefaultAsync(x => x.Id == id);
        if(cust is null)
            return NotFound();

        return cust;
    }

    [HttpGet("{card}/{pin}")]
    public async Task<ActionResult<Customer>> Login(int card, int pin) {
        var cust = await _context.Customers
                                    .Include(x => x.Accounts)
                                    .SingleOrDefaultAsync(x => x.CardCode == card && x.PinCode == pin);
        if(cust is null)
            return NotFound();

        return cust;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer cust) {
        cust.CreatedDate = DateTime.Now;
        _context.Customers.Add(cust);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCustomer", new { id = cust.Id }, cust);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer cust) {

        if(id != cust.Id)
            return BadRequest();

        cust.ModifiedDate = DateTime.Now;
        _context.Entry(cust).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!CustomerExists(id)) 
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id) {

        var cust = await _context.Customers.FindAsync(id);

        if(cust is null) 
            return NotFound();

        _context.Customers.Remove(cust);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CustomerExists(int id) {
        return (_context.Customers?.Any(x => x.Id == id)).GetValueOrDefault();
    }
}