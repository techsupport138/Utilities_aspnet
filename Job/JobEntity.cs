namespace Utilities_aspnet.Job;

[Table("Job")]
public partial class Job : BaseEntity
{
    [Required]
    public string UserId { get; set; }
    public int JobFieldId { get; set; }
    [Required]
    [StringLength(1000)]
    public string Value { get; set; }

    public virtual UserEntity User { get; set; }
    [ForeignKey(nameof(JobFieldId))]
    [InverseProperty(nameof(JobFieldList.Jobs))]
    public virtual JobFieldList JobField { get; set; }
}

[Table("FieldType")]
public partial class FieldType
{
    [Key]
    public int FieldTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string FieldTypeName { get; set; }

    [InverseProperty(nameof(JobFieldList.FieldType))]
    public virtual ICollection<JobFieldList> JobFieldLists { get; set; }
}

[Table("JobFieldList")]
public partial class JobFieldList
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
    [InverseProperty("JobFieldLists")]
    public virtual FieldType FieldType { get; set; }
    [ForeignKey(nameof(JobTypeId))]
    [InverseProperty("JobFieldLists")]
    public virtual JobType JobType { get; set; }
    [InverseProperty(nameof(Job.JobField))]
    public virtual ICollection<Job> Jobs { get; set; }
}

[Table("JobType")]
public partial class JobType
{

    [Key]
    public int JobTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string UserTypeName { get; set; }

    public bool Enable { get; set; }

    [InverseProperty(nameof(JobFieldList.JobType))]
    public virtual ICollection<JobFieldList> JobFieldLists { get; set; }
}
