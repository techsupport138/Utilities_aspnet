using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Values.Entities
{
    [Table("Values")]
    public class ValueEntity:BaseEntity
    {
        public ValueEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid ValueFieldId { get; set; }
        [ForeignKey(nameof(ValueFieldId))]
        public ValueFieldEntity ValueField { get; set; }


        public Guid?  ADSId { get; set; }
        [ForeignKey(nameof(ADSId))]
        public AdsEntity? AdsEntity { get; set; }

        public Guid? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity? ProductEntity { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserEntity? UserEntity { get; set; }

        public Guid? MediaId { get; set; }
        [ForeignKey(nameof(MediaId))]
        public MediaEntity? MediaEntity { get; set; }

        [Column(TypeName = "ntext")]
        public string? Value { get; set; } = null;

    }
}
