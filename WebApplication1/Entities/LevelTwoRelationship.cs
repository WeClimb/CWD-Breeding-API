using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class LevelTwoRelationship : BaseEntity
    {
        public Guid DeerId { get; set; }
        public string? DamA { get; set; }
        public string? DamB { get; set; }
        public string? SireA { get; set; }
        public string? SireB { get; set; }
        public virtual Deer Deer { get; set; } = null!;
    }
}
