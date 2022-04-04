//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities_aspnet.Utilities.Entities;

//namespace Utilities_aspnet.Tender.Entities
//{
//    public class TenderCategoryEntity
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int CategoryId { get; set; }
//        [StringLength(200)]
//        public string Title { get; set; }

//        [ForeignKey(nameof(MediaId))]
//        public MediaEntity? Media { get; set; }
//        public Guid? MediaId { get; set; }
//    }
//}
