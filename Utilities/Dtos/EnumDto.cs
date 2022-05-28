namespace Utilities_aspnet.Utilities.Dtos;

public class EnumDto {

    public List<IdTitleReadDto>? Colors { get; set; }
    public List<IdTitleReadDto>? Favorites { get; set; }
    public List<IdTitleReadDto>? SpecialtyCategories { get; set; }
    public List<IdTitleReadDto>? Specialties { get; set; }
    public List<IdTitleReadDto>? UserRole { get; set; }
    public List<IdTitleReadDto>? Categories { get; internal set; }
    public List<IdTitleReadDto>? FormFieldType { get; internal set; }
    public List<IdTitleReadDto>? CategoryUseCase { get; internal set; }
}