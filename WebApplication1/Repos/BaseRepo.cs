using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public abstract class BaseRepo<Entity> where Entity : BaseEntity
    {
        protected readonly CWDBreedingContext _reviewPlatformDBContext;

        public BaseRepo(CWDBreedingContext reviewPlatformDBContext)
        {
            _reviewPlatformDBContext = reviewPlatformDBContext;
        }
        public abstract DbSet<Entity> LoadDbSet();

        public Entity? LoadByPrimaryKey(Guid id)
        {
            return LoadDbSet().Find(id);
        }

        public Guid Create(Entity entity)
        {
            LoadDbSet().Add(entity);
            _reviewPlatformDBContext.SaveChanges();

            return entity.Id;
        }

        public void Update(Entity entity)
        {
            LoadDbSet().Update(entity);
            _reviewPlatformDBContext.SaveChanges();
        }

        public int SaveChanges()
        {
            return _reviewPlatformDBContext.SaveChanges();
        }
    }
}
