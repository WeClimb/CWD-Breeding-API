using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class DeerModel : BaseModel
    {
        public DeerModel()
        {
            LevelOneRelationships = new HashSet<LevelOneRelationshipModel>();
            LevelThreeRelationships = new HashSet<LevelThreeRelationshipModel>();
            LevelTwoRelationships = new HashSet<LevelTwoRelationshipModel>();
            Media = new HashSet<Medium>();
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
        public virtual RanchModel Ranch { get; set; } = null!;
        public virtual ICollection<LevelOneRelationshipModel> LevelOneRelationships { get; set; }
        public virtual ICollection<LevelThreeRelationshipModel> LevelThreeRelationships { get; set; }
        public virtual ICollection<LevelTwoRelationshipModel> LevelTwoRelationships { get; set; }
        public virtual ICollection<MediaModel> Media { get; set; }
    }
}
