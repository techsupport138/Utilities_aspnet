using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Utilities_aspnet.Utilities.Entities;
using Utilities_aspnet.Utilities.Enums;

namespace Utilities_aspnet.Models.Dto
{
    public class ContactInformationCreateDto
    {
        public string Value { get; set; } = "";
        public long ContactInfoItemId { get; set; }
        [EnumDataType(typeof(VisibilityType))]
        public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
    }
    
    public class ContactInformationDto
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public ContactInfoItemDto? ContactInfoItem { get; set; }
        public string? Link { get; set; }
        public VisibilityType Visibility { get; set; } = VisibilityType.UsersOnly;
    }

    public class AutoMapperContactInformation : Profile
    {
        public AutoMapperContactInformation()
        {
            CreateMap<ContactInformationEntity, ContactInformationDto>();
        }
    }
}
