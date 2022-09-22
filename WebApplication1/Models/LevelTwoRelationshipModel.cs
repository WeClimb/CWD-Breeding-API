using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class LevelTwoRelationshipModel : BaseModel
    {
        public Guid DeerId { get; set; }
        public string? DamA { get; set; }
        public string? DamB { get; set; }
        public string? SireA { get; set; }
        public string? SireB { get; set; }
        public virtual DeerModel Deer { get; set; } = null!;
    }
}
