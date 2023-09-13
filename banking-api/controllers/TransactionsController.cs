

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController] 
public class TransactionsController : ControllerBase {

    private readonly BankingContext _context;

    public TransactionsController(BankingContext context) {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions() {
        return await _context.Transactions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id) {
        var trans = await _context.Transactions
                                    .SingleOrDefaultAsync(x => x.Id == id);
        if(trans is null)
            return NotFound();

        return trans;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction trans) {
        trans.CreatedDate = DateTime.Now;
        _context.Transactions.Add(trans);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetTransaction", new { id = trans.Id }, trans);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTransaction(int id, Transaction trans) {

        if(id != trans.Id)
            return BadRequest();

        _context.Entry(trans).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch(DbUpdateConcurrencyException) {
            if(!TransactionExists(id)) 
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id) {

        var trans = await _context.Transactions.FindAsync(id);

        if(trans is null) 
            return NotFound();

        _context.Transactions.Remove(trans);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(int id) {
        return (_context.Transactions?.Any(x => x.Id == id)).GetValueOrDefault();
    }
}