using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Entities
{
    public partial class Deer : BaseEntity
    {
        public Deer()
        {
            LevelOneRelationships = new HashSet<LevelOneRelationship>();
            LevelThreeRelationships = new HashSet<LevelThreeRelationship>();
            LevelTwoRelationships = new HashSet<LevelTwoRelationship>();
            Media = new HashSet<Media>();
        }

        public string Name { get; set; } = null!;
        public string Nadr { get; set; } = null!;
        public decimal? Dob { get; set; }
        public decimal? Age { get; set; }
        public decimal Gebu { get; set; }
        public decimal Codon { get; set; }
        public decimal? SciScore { get; set; }
        public bool IsApproved { get; set; }
        public bool SemenAvailable { get; set; }
        public string? SemenCost { get; set; }
        public Guid RanchId { get; set; }
        public virtual Ranch Ranch { get; set; } = null!;
        public virtual ICollection<LevelOneRelationship> LevelOneRelationships { get; set; }
        public virtual ICollection<LevelThreeRelationship> LevelThreeRelationships { get; set; }
        public virtual ICollection<LevelTwoRelationship> LevelTwoRelationships { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
