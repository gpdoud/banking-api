

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase {

    private readonly BankingContext _context;

    public AccountsController(BankingContext context) {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts() {
        return await _context.Accounts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id) {
        var acct = await _context.Accounts
                                    .Include(x => x.Transactions)
                                    .SingleOrDefaultAsync(x => x.Id == id);
        if(acct is null)
            return NotFound();

        return acct;
    }

    [HttpGet("accounts/{custId}")]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccountsForCustomer(int custId) {
        var accts = await _context.Accounts
                                    .Include(x => x.Transactions)
                                    .Where(x => x.CustomerId == custId)
                                    .ToListAsync();
        if(accts is null)
            return NotFound();

        return accts;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account acct) {
        acct.CreatedDate = DateTime.Now;
        _context.Accounts.Add(acct);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAccount", new { id = acct.Id }, acct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccount(int id, Account acct) {

        if(id != acct.Id)
            return BadRequest();

        acct.ModifiedDate = DateTime.Now;
        _context.Entry(acct).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!AccountExists(id)) 
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id) {

        var acct = await _context.Accounts.FindAsync(id);

        if(acct is null) 
            return NotFound();

        _context.Accounts.Remove(acct);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AccountExists(int id) {
        return (_context.Accounts?.Any(x => x.Id == id)).GetValueOrDefault();
    }
}