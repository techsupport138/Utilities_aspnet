using Utilities_aspnet.User;

namespace Utilities_aspnet.Wallet.Entities;

public class BankTransaction {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BankTransactionId { get; set; }

    public DateTime? CreateDateTime { get; set; }
    public UserEntity? UserEntity { get; set; }
    public string? UserName { get; set; }
    public string? OrderId { get; set; }
    public double? Amount { get; set; }
    public bool? Ok { get; set; }
}