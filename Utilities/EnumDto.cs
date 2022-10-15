namespace Utilities_aspnet.Utilities;

public class IdTitleDto {
	public int? Id { get; set; }
	public string? Title { get; set; }
}

public class EnumDto {
	public IEnumerable<IdTitleDto>? FormFieldType { get; set; }
	public IEnumerable<IdTitleDto>? TransactionStatuses { get; set; }
	public IEnumerable<IdTitleDto>? UtilitiesStatusCodes { get; set; }
	public IEnumerable<IdTitleDto>? OtpResult { get; set; }
	public IEnumerable<IdTitleDto>? DatabaseType { get; set; }
	public IEnumerable<IdTitleDto>? OrderStatuses { get; set; }
	public IEnumerable<IdTitleDto>? PayType { get; set; }
	public IEnumerable<IdTitleDto>? SendType { get; set; }
	public IEnumerable<IdTitleDto>? ProductStatus { get; set; }
	public IEnumerable<IdTitleDto>? Sender { get; set; }
	public IEnumerable<IdTitleDto>? Currency { get; set; }
	public IEnumerable<IdTitleDto>? SeenStatus { get; set; }
	public IEnumerable<IdTitleDto>? Priority { get; set; }
	public IEnumerable<IdTitleDto>? ChatStatus { get; set; }
	public AppSettings? AppSettings { get; set; }
}