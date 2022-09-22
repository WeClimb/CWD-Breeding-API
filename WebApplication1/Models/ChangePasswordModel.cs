using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class ChangePasswordModel : BaseModel
    {
        public Guid? UserId { get; set; }
        public Guid? RanchId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual RanchModel? Ranch { get; set; }
        public virtual UserModel? User { get; set; }
    }
}
