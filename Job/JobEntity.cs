namespace Utilities_aspnet.Job;

[Table("Jobs")]
public partial class JobEntity : BaseEntity
{
    [Required]
    public string UserId { get; set; }
    public int JobFieldId { get; set; }
    [Required]
    [StringLength(1000)]
    public string Value { get; set; }

    public virtual UserEntity User { get; set; }
    [ForeignKey(nameof(JobFieldId))]
    [InverseProperty(nameof(JobFieldListEntity.Jobs))]
    public virtual JobFieldListEntity JobField { get; set; }
}

[Table("FieldTypes")]
public partial class FieldTypeEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string FieldTypeName { get; set; }

    [InverseProperty(nameof(JobFieldListEntity.FieldType))]
    public virtual ICollection<JobFieldListEntity> JobFieldLists { get; set; }
}

[Table("JobFields")]
public partial class JobFieldListEntity
{

    [Key]
    public int JobFieldId { get; set; }
    public int JobTypeId { get; set; }
    public int FieldTypeId { get; set; }
    [Required]
    [StringLength(100)]
    public string Lable { get; set; }
    public bool Required { get; set; }
    [StringLength(200)]
    [Display(Name = "انتخاب ها", Prompt = "نمونه : زن, مرد  | مجرد, متاهل (فاصله اهمیت ندارد)")]
    public string? OptionList { get; set; }

    [ForeignKey(nameof(FieldTypeId))]
    public virtual FieldTypeEntity FieldType { get; set; }
    [ForeignKey(nameof(JobTypeId))]
    public virtual JobTypeEntity JobType { get; set; }
    [InverseProperty(nameof(JobEntity.JobField))]
    public virtual ICollection<JobEntity> Jobs { get; set; }
}

[Table("JobTypes")]
public partial class JobTypeEntity
{

    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string UserTypeName { get; set; }

    public bool Enable { get; set; }

    [InverseProperty(nameof(JobFieldListEntity.JobType))]
    public virtual ICollection<JobFieldListEntity> JobFieldLists { get; set; }
}
