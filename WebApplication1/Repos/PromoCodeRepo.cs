
using CWDBreedingAPI.Entities;
using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI;

namespace CWDBreedingAPI.Repos
{
    public class PromoCodeRepository
    {
        private readonly CWDBreedingContext _context;

        public PromoCodeRepository(CWDBreedingContext context)
        {
            _context = context;
        }

        public PromoCode? GetPromoCode(string? promoCode)
        {
            if (promoCode == null)
            {
                return null;
            } 
            else
            {
                PromoCode? promo = _context.PromoCodes.FirstOrDefault(p => p.Code == promoCode) ?? null;
                if(promo != null)
                {
                    promo.Uses++;
                    _context.SaveChanges();
                }
                return promo;
            }
        }
    }
}
