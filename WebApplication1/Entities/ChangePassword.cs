using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class ChangePassword : BaseEntity
    {
        public Guid? UserId { get; set; }
        public Guid? RanchId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Ranch? Ranch { get; set; }
        public virtual User? User { get; set; }
    }
}
