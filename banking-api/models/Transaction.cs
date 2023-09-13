using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Transaction {

    public static string TRANSACTION_DEPOSIT = "D";
    public static string TRANSACTION_WITHDRAW = "W";

    public int Id { get; set; } = 0;
    [Column(TypeName = "decimal(9,2)")]
    public decimal PreviousBalance { get; set; } = 0;
    [StringLength(1)]
    public string TransactionType { get; set; } = TRANSACTION_WITHDRAW;
    [Column(TypeName = "decimal(9,2)")]
    public decimal NewBalance { get; set; } = 0;
    [Column(TypeName = "decimal(9,2)")]
    public decimal TransactionAmount { get; set; } = 0;
    [StringLength(80)]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int AccountId { get; set; } = 0;
    [JsonIgnore]
    public virtual Account? Account { get; set; } = null;

}