using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class LevelThreeRelationshipModel : BaseModel
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

        public virtual DeerModel Deer { get; set; } = null!;
    }
}
