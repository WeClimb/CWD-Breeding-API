using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class RanchModel : BaseModel
    {
        public RanchModel()
        {
            ChangePasswords = new HashSet<ChangePasswordModel>();
            Deer = new HashSet<DeerModel>();
        }

        public string? Name { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerlastName { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? LoginDataId { get; set; }
        public string? StripeId { get;  set; }

        public virtual LoginDataModel? LoginData { get; set; }
        public virtual ICollection<ChangePasswordModel> ChangePasswords { get; set; }
        public virtual ICollection<DeerModel> Deer { get; set; }
    }
}
