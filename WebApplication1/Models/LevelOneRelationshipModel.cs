using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class LevelOneRelationshipModel : BaseModel
    {
        public Guid DeerId { get; set; }
        public string? Dam { get; set; }
        public string? Sire { get; set; }
        public virtual DeerModel Deer { get; set; } = null!;
    }
}
