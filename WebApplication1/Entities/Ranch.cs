using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class Ranch : BaseEntity
    {
        public Ranch()
        {
            ChangePasswords = new HashSet<ChangePassword>();
            Deer = new HashSet<Deer>();
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

        public virtual LoginData? LoginData { get; set; }
        public virtual ICollection<ChangePassword> ChangePasswords { get; set; }
        public virtual ICollection<Deer> Deer { get; set; }
    }
}
