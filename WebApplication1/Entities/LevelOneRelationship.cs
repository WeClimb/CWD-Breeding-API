using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class LevelOneRelationship : BaseEntity
    {
        public Guid DeerId { get; set; }
        public string? Dam { get; set; }
        public string? Sire { get; set; }
        public virtual Deer Deer { get; set; } = null!;
    }
}
