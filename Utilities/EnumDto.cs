namespace Utilities_aspnet.Utilities;

public class EnumDto {
	public IEnumerable<CategoryReadDto>? FormFieldType { get; set; }
	public IEnumerable<CategoryReadDto>? TransactionStatuses { get; set; }
	public IEnumerable<GenderEntity>? Genders { get; set; }
}