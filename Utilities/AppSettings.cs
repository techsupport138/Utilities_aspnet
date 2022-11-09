namespace Utilities_aspnet.Utilities;

public class AppSettings {
	public SmsPanelSettings? SmsPanelSettings { get; set; }
	public Pushe? Pushe { get; set; }
	public string? AndroidMinimumVersion { get; set; }
	public string? AndroidLatestVersion { get; set; }
	public string? IosMinimumVersion { get; set; }
	public string? IosLatestVersion { get; set; }
	public string? AndroidDownloadLink1 { get; set; }
	public string? AndroidDownloadLink2 { get; set; }
	public string? IosDownloadLink1 { get; set; }
	public string? IosDownloadLink2 { get; set; }
}

public class SmsPanelSettings {
	public string? Provider { get; set; }
	public string? UserName { get; set; }
	public string? LineNumber { get; set; }
	public string? SmsApiKey { get; set; }
	public string? SmsSecret { get; set; }
	public int? OtpId { get; set; }
	public string? PatternCode { get; set; }
}

public class Pushe {
	public string? AccessToken { get; set; }
	public string? Applications { get; set; }
}

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