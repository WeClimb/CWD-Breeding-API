using CWDBreedingAPI.Models.Non_EntityModels;
using ReviewPlatformAPI.Entities;
using System;
using System.Collections.Generic;

namespace ReviewPlatformAPI.Models
{
    public partial class DeerModel : BaseModel
    {


        public string Name { get; set; } = null!;
        public string Nadr { get; set; } = null!;
        public DateTime Dob { get; set; }
        public decimal? Age { get; set; }
        public decimal? AgeOfBuckDisplayed { get; set; }
        public string Description { get; set; } = "";
        public decimal Gebu { get; set; }
        public string Codon { get; set; }
        public decimal? SciScore { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPaid { get; set; } = false; 
        public DateTime? PaidDate { get; set; }
        public bool SemenAvailable { get; set; }
        public string? SemenCost { get; set; }
        public Guid RanchId { get; set; }
        public string? ProfileImage { get; set; } = string.Empty;
        public string? VideoLink { get; set; }
        public string? DenialReason { get; set; }
        public virtual DeerFamilyModel? deerFamily { get; set;}
        public virtual RanchModel? Ranch { get; set; } = null!;



    }
}
