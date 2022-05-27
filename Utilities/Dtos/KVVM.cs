namespace Utilities_aspnet.Utilities.Dtos;

public class KVVM {
    public Guid Key { get; set; }
    public string Value { get; set; }
}

public class KVIdTitle {
    public int Id { get; set; }
    public string Title { get; set; }
}

public class KVVMs {
    //public FormBuilderUseCase UseCase { get; set; }
    //public Guid Id { get; set; }
    public string? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? TutorialId { get; set; }
    public Guid? EventId { get; set; }
    public Guid? AdId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TenderId { get; set; }
    public Guid? ServiceId { get; set; }
    public Guid? MagazineId { get; set; }


    public List<KVVM> KVVM { get; set; }
}