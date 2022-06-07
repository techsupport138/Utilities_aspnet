namespace Utilities_aspnet.Utilities.Seeder;
public interface ISeedRepository
{
    Task<bool> SeedLocations();
}

public class SeedRepository : ISeedRepository
{
    private readonly DbContext _context;

    public SeedRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> SeedLocations()
    {
        var path = Directory.GetCurrentDirectory() + "\\Utilities" + "\\AllLocationNew.json";
        var data = File.ReadAllText(path);
        var cities = JsonConvert.DeserializeObject<List<LocationCreateDto>>(data);

        if (cities != null)
        {
            foreach (var country in cities)
            {
                if (!await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.country))
                {
                    var countryLocation = new LocationEntity()
                    {
                        Type = LocationType.Country,
                        Title = country.country,
                    };

                    await _context.AddAsync(countryLocation);
                    await _context.SaveChangesAsync();
                    var cityLocation = new LocationEntity()
                    {
                        Type = LocationType.City,
                        Title = country.city,
                        Latitude = Convert.ToDouble(country.lat),
                        Longitude = Convert.ToDouble(country.lng),
                        ParentId = countryLocation.Id
                    };
                    await _context.AddAsync(cityLocation);
                    await _context.SaveChangesAsync();
                }
                else if (await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.country) && !await _context.Set<LocationEntity>().AnyAsync(s => s.Title == country.city))
                {
                    var parentId = await _context.Set<LocationEntity>().FirstOrDefaultAsync(s => s.Title == country.country);
                    if (parentId != null)
                    {
                        var cityLocation = new LocationEntity()
                        {
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
            }
        }
        return true;
    }
}