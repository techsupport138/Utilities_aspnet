using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities_aspnet.Base;
using Utilities_aspnet.Utilities.Entities;

namespace Utilities_aspnet.User.Entities;

public class SpecialtyEntity : BaseCategoryEntity {

    public ICollection<UserEntity>? Users { get; set; }
}