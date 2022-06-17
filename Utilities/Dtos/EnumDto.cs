using Utilities_aspnet.Category;

namespace Utilities_aspnet.Utilities.Dtos;

public class EnumDto {
    public IEnumerable<CategoryReadDto>? Colors { get; set; }
    public IEnumerable<CategoryReadDto>? Favorites { get; set; }
    public IEnumerable<CategoryReadDto>? Specialties { get; set; }
    public IEnumerable<CategoryReadDto>? Categories { get; set; }
    public IEnumerable<CategoryReadDto>? FormFieldType { get; set; }
    public IEnumerable<CategoryReadDto>? CategoryUseCase { get; set; }
    public IEnumerable<CategoryReadDto>? TransactionStatuses { get; set; }
    public IEnumerable<GenderEntity>? Genders { get; set; }
}