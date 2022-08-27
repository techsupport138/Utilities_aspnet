﻿namespace Utilities_aspnet.Repositories;

public interface IAppSettingRepository {
	Task<GenericResponse<EnumDto?>> Read();
}

public class AppSettingRepository : IAppSettingRepository {
	private readonly DbContext _context;

	public AppSettingRepository(DbContext context) => _context = context;

	public Task<GenericResponse<EnumDto?>> Read() {
		EnumDto model = new();

		IEnumerable<CategoryReadDto> formFieldType = EnumExtension.GetValues<FormFieldType>();
		IEnumerable<CategoryReadDto> transactionStatus = EnumExtension.GetValues<TransactionStatus>();
		model.FormFieldType = formFieldType;
		model.TransactionStatuses = transactionStatus;
		model.Genders = _context.Set<GenderEntity>().ToList();
		return Task.FromResult(new GenericResponse<EnumDto?>(model, UtilitiesStatusCodes.Success, "Success"));
	}
}