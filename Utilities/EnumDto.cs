namespace Utilities_aspnet.Utilities;

public class EnumDto {
	public IEnumerable<CategoryReadDto>? FormFieldType { get; set; }
	public IEnumerable<CategoryReadDto>? TransactionStatuses { get; set; }
	public IEnumerable<CategoryReadDto>? UtilitiesStatusCodes { get; set; }
	public IEnumerable<CategoryReadDto>? OtpResult { get; set; }
	public IEnumerable<CategoryReadDto>? DatabaseType { get; set; }
	public IEnumerable<CategoryReadDto>? OrderStatuses { get; set; }
	public IEnumerable<CategoryReadDto>? PayType { get; set; }
	public IEnumerable<CategoryReadDto>? SendType { get; set; }
	public IEnumerable<CategoryReadDto>? ProductStatus { get; set; }
	public IEnumerable<CategoryReadDto>? Sender { get; set; }
	public AppSettings? AppSettings { get; set; }
}