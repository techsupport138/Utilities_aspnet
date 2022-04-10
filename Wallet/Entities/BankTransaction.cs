using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Wallet.Entities
{
    public class BankTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankTransactionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDateTime { get; set; }
        [Required]
        public string UserName { get; set; }
        [ForeignKey(nameof(UserName))]
        public virtual UserEntity UserEntity { get; set; }
        [Column("order_id")]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Column("amount", TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column("OK")]
        public bool Ok { get; set; }
    }
}
