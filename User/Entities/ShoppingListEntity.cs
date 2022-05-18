namespace Utilities_aspnet.User.Entities
{
    [Table("ShoppingList")]
    public class ShoppingListEntity : BaseEntity
    {
        public ShoppingListEntity()
        {
            User = new UserEntity();
            BankTransaction = new BankTransaction();
            Product = new ProductEntity();
        }
        public UserEntity User { get; set; }
        [StringLength(450)]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public BankTransaction BankTransaction { get; set; }
        [ForeignKey(nameof(BankTransaction))]
        public int? BankTransactionId { get; set; }

        public ProductEntity Product { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public BuyOrSale BuyOrSale { get; set; }

        [Column("amount", TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }


    }
}
