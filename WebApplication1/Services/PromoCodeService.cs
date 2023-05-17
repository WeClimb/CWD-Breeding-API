namespace CWDBreedingAPI.Services
{
    using CWDBreedingAPI.Entities;
    using CWDBreedingAPI.Repos;
    using System;

    public class PromoCodeService
    {
        private readonly PromoCodeRepository _repository;

        public PromoCodeService(PromoCodeRepository repository)
        {
            _repository = repository;
        }
        
        public PromoCode? GetPromoCodeById(string id)
        {
            return _repository.GetPromoCode(id);
        }
    }
}
