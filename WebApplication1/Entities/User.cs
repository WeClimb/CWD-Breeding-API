using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            ChangePasswords = new HashSet<ChangePassword>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public Guid LoginDataId { get; set; }
        public virtual LoginDatum LoginData { get; set; } = null!;
        public virtual ICollection<ChangePassword> ChangePasswords { get; set; }
    }
}
