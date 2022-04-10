using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Wallet.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        [Required]
        public string TransactionUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDateTime { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }


        [ForeignKey(nameof(TransactionUser))]
        public virtual UserEntity TransactionUserNavigation { get; set; }
    }
}
