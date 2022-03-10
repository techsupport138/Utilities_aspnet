using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.ServiceProvider.Entities;

public class ServiceProviderEntity : BaseEntity {
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public string Type { get; set; }
    public IEnumerable<ContactInformationEntity>? ContactInformation { get; set; }
}