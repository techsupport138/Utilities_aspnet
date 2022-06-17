using Utilities_aspnet.Category;

namespace Utilities_aspnet.Utilities.Dtos;

public class EnumDto {
    public List<CategoryReadDto>? Colors { get; set; }
    public List<CategoryReadDto>? Favorites { get; set; }
    public List<CategoryReadDto>? Specialties { get; set; }
    public List<CategoryReadDto>? Categories { get; set; }
    public List<CategoryReadDto>? FormFieldType { get; set; }
    public List<CategoryReadDto>? CategoryUseCase { get; set; }
    public List<CategoryReadDto>? TransactionStatuses { get; set; }
    public List<GenderEntity>? Genders { get; set; }
}