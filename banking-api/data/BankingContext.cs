using Microsoft.EntityFrameworkCore;

public class BankingContext : DbContext {

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    public BankingContext(DbContextOptions<BankingContext> options) : base(options) {}

}