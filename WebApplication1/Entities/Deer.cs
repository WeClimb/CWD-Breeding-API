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
        public DateTime Dob { get; set; }
        public decimal? Age { get; set; }
        public decimal? AgeOfBuckDisplayed { get; set; }
        public string Description { get; set; } = "";
        public decimal GEBV { get; set; }
        public string Codon { get; set; }
        public decimal? SciScore { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaidDate { get; set; }
        public bool SemenAvailable { get; set; }
        public string? SemenCost { get; set; }
        public bool EmbryosAvailable { get; set; } // New property
        public string? EmbryosCost { get; set; } // New property
        public string Gender { get; set; } = null!; // New property
        public Guid RanchId { get; set; }
        public string? ProfileImage { get; set; }
        public string? VideoLink { get; set; }
        public string? DenialReason { get; set; }
        public virtual Ranch Ranch { get; set; } = null!;
        public virtual ICollection<LevelOneRelationship> LevelOneRelationships { get; set; }
        public virtual ICollection<LevelThreeRelationship> LevelThreeRelationships { get; set; }
        public virtual ICollection<LevelTwoRelationship> LevelTwoRelationships { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
