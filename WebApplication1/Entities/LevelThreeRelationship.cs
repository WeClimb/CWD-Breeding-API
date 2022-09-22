using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class LevelThreeRelationship : BaseEntity
    {
        public Guid DeerId { get; set; }
        public string? DamA { get; set; }
        public string? DamB { get; set; }
        public string? DamC { get; set; }
        public string? DamD { get; set; }
        public string? SireA { get; set; }
        public string? SireB { get; set; }
        public string? SireC { get; set; }
        public string? SireD { get; set; }

        public virtual Deer Deer { get; set; } = null!;
    }
}
