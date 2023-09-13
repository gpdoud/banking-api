using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer {

    public int Id { get; set; } = 0;
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
    public int CardCode { get; set; } = 0;
    public int PinCode { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; } = null;

    public virtual IEnumerable<Account>? Accounts { get; set; } = null;
}