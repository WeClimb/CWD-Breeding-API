using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public abstract class BaseService<Model, Entity> where Model : BaseModel where Entity : BaseEntity
    {
        public abstract BaseRepo<Entity> LoadRepo();

        public abstract Entity ConverToEntityForAdd(Model model);

        public abstract void CopyDataForUpdate(Entity entity, Model model);

        public abstract Model CreateModelForIndividualLookup(Entity entity);

        public abstract Model CreateModelForListLookup(Entity entity);

        public virtual Guid Create(Model model)
        {

            Entity entity = ConverToEntityForAdd(model);

            entity.Id = Guid.NewGuid();

            AdditionalPreAddLogic(entity);

            Guid id = LoadRepo().Create(entity);

            AdditonalPostAddLogic(entity);

            return id;
        }

        protected virtual void AdditonalPostAddLogic(Entity entity)
        {

        }

        protected virtual void AdditionalPreAddLogic(Entity entity)
        {

        }

        public void Update(Guid id, Model model)
        {
            try
            {
                Entity? entity = LoadRepo().LoadByPrimaryKey(id);
                if (entity != null)
                {
                    CopyDataForUpdate(entity, model);
                    LoadRepo().Update(entity);
                } 
                else
                {
                    throw new Exception("Update Failed");
                }
            }
            catch
            {
                throw new Exception("Update Failed");
            }
        }

        public Model? LookupById(Guid id)
        {
            Entity? entity = LoadRepo().LoadByPrimaryKey(id);
            if(entity != null)
            {
                return CreateModelForIndividualLookup(entity);
            }

            return null;

        }
    }
}
