using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.Entities.Actors;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Business_Logic_Layer.Services
{
    public abstract class GeneralService(IUnitOfWork unitOfWork)
    {
        protected IUnitOfWork _unitOfWork = unitOfWork;

        protected void CheckEntityExist<T>(Expression<Func<T, bool>> prediction) where T : class
        {
            bool exists = _unitOfWork.GetDynamicRepository<T>().Exists(prediction);

            if (!exists)
            {
                throw new BadRequestException($"{typeof(T).Name} does not exist.");
            }
        }
        protected void CheckCreatedState<T>(int EntityID)
        {
            if (EntityID <= 0)
                throw new NotFoundException($"{typeof(T).Name} created faild.");
        }
        protected async Task<T> CreateEntityAsync<T>(T entity, bool saveChanges = false, bool validate = true) where T : class
        {
            if(validate)ValidationHelper.ValidateEntity(entity);
            await _unitOfWork.GetDynamicRepository<T>().AddAsync(entity);
            if (saveChanges) await _unitOfWork.SaveChangesAsync();
            return entity;
        }


        protected bool CheckList<T>(IEnumerable<T> list)
        {
            return (list == null || !list.Any());
          
        }

        protected void CheckEntityIsNotNull<T>(T Entity)
        {
            if(Entity == null) 
                throw new NotFoundException(typeof(T).ToString() + " Is Null");
        }

        protected void DeleteEntity<T>(T Entity) where T : class
        {
            _unitOfWork.GetDynamicRepository<T>().Remove(Entity);
            _unitOfWork.SaveChanges();
        }

        protected void UpdateEntity<T>(T Entity) where T : class
        {
            ValidationHelper.ValidateEntity(Entity);
            _unitOfWork.GetDynamicRepository<T>().Update(Entity);
        }
        //public async Task<IEnumerable<TDTO>> GetAllAsync<TEntity, TDTO>() where TEntity : class
        //{
        //    var entities = _unitOfWork.GetDynamicRepository<TEntity>().GetAll().ToList();
        //    return _mapper.Map<IEnumerable<TDTO>>(entities);
        //}

    }
}