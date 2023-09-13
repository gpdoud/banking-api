using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Account {

    public static string ACCOUNT_CHECKING = "CK";
    public static string ACCOUNT_SAVINGS = "SV";

    public int Id { get; set; } = 0;
    [StringLength(2)]
    public string Type { get; set; } = ACCOUNT_CHECKING;
    [StringLength(80)]
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(4,2)")]
    public decimal InterestRate { get; set; } = 0;
    [Column(TypeName = "decimal(11,2)")]
    public decimal Balance { get; set; } = 0;
    public DateTime? LastTransactionDate { get; set; } = null;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; } = null;

    public int CustomerId { get; set; } = 0;
    [JsonIgnore]
    public virtual Customer? Customer { get; set; } = null;

    public virtual IEnumerable<Transaction>? Transactions { get; set; } = null;

}