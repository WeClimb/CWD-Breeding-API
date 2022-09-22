using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class UserModel : BaseModel
    {
        public UserModel()
        {
            ChangePasswords = new HashSet<ChangePasswordModel>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public Guid LoginDataId { get; set; }
        public virtual LoginDataModel LoginData { get; set; } = null!;
        public virtual ICollection<ChangePasswordModel> ChangePasswords { get; set; }
    }
}
