namespace Utilities_aspnet.Utilities.Seeder;

public interface ISeedRepository {
	Task<bool> SeedLocations();
	Task<bool> SeedGenders();
	Task<GenericResponse> SeedCategories(SeederCategoryDto dto);
}

public class SeedRepository : ISeedRepository {
	public static string[] genders =
		{"مرد", "زن", "تیم", "شرکت", "موسسه فرهنگی هنری", "اتحادیه(مشاغل آزاد)", "سازمان های مردم نهاد"};

	private readonly DbContext _context;
	private readonly IMapper _mapper;

	public SeedRepository(DbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<bool> SeedLocations() {
		string path = Directory.GetCurrentDirectory() + "\\AllLocationNew.json";
		string data = File.ReadAllText(path);
		IEnumerable<LocationCreateDto>? cities = JsonConvert.DeserializeObject<IEnumerable<LocationCreateDto>>(data);

		if (cities != null)
			foreach (LocationCreateDto? country in cities)
				if (!await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.country)) {
					LocationEntity countryLocation = new() {
						Type = LocationType.Country,
						Title = country.country
					};

					await _context.AddAsync(countryLocation);
					await _context.SaveChangesAsync();
					LocationEntity cityLocation = new() {
						Type = LocationType.City,
						Title = country.city,
						Latitude = Convert.ToDouble(country.lat),
						Longitude = Convert.ToDouble(country.lng),
						ParentId = countryLocation.Id
					};
					await _context.AddAsync(cityLocation);
					await _context.SaveChangesAsync();
				}
				else if (await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.country) &&
				         !await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.city)) {
					LocationEntity? parentId =
						await _context.Set<LocationEntity>().FirstOrDefaultAsync(s => s.Title == country.country);
					if (parentId != null) {
						LocationEntity cityLocation = new() {
							Type = LocationType.City,
							Title = country.city,
							Latitude = Convert.ToDouble(country.lat),
							Longitude = Convert.ToDouble(country.lng),
							ParentId = parentId.Id
						};
						await _context.AddAsync(cityLocation);
						await _context.SaveChangesAsync();
					}
				}

		return true;
	}

	public async Task<bool> SeedGenders() {
		GenderEntity? genderEntity = await _context.Set<GenderEntity>().FirstOrDefaultAsync();
		if (genderEntity == null)
			foreach (string item in genders) {
				await _context.Set<GenderEntity>().AddAsync(new GenderEntity {Title = item});
				await _context.SaveChangesAsync();
			}

		return true;
	}

	public async Task<GenericResponse> SeedCategories(SeederCategoryDto dto)
	{
        try
        {
			foreach (var item in dto.Categories)
			{
				CategoryEntity entity = _mapper.Map<CategoryEntity>(item);

				await _context.AddAsync(entity);
				await _context.SaveChangesAsync();
			}
		}
        catch
        {
			return new GenericResponse(UtilitiesStatusCodes.BadRequest);
		}

		return new GenericResponse(UtilitiesStatusCodes.Success);
	}


}