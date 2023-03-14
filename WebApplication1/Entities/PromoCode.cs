namespace CWDBreedingAPI.Entities
{
    public class PromoCode
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Code { get; set; }
        public decimal PercentageOff { get; set; }
        public int Uses { get; set; }
    }
}
