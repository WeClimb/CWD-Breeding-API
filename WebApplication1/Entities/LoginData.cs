using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class LoginData : BaseEntity
    {
        public LoginData()
        {
            Ranches = new HashSet<Ranch>();
            Users = new HashSet<User>();
        }

        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<Ranch> Ranches { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
