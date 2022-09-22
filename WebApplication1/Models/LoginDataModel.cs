using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class LoginDataModel : BaseModel
    {
        public LoginDataModel()
        {
            Ranches = new HashSet<RanchModel>();
            Users = new HashSet<UserModel>();
        }

        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<RanchModel> Ranches { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
