namespace Utilities_aspnet.Utilities.Dtos;

public class EnumDto {
    public List<IdTitleReadDto>? Colors { get; set; }
    public List<IdTitleReadDto>? Favorites { get; set; }
    public List<IdTitleReadDto>? Specialties { get; set; }
    public List<IdTitleReadDto>? Categories { get; set; }
    public List<IdTitleReadDto>? FormFieldType { get; set; }
    public List<IdTitleReadDto>? CategoryUseCase { get; set; }
    public List<GenderEntity>? Genders { get; set; }
}