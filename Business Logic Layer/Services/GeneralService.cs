using Azure.Core;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public abstract class GeneralService
    {
        protected IUnitOfWork _unitOfWork;
        public GeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
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
        protected async Task<T> CreateEntityAsync<T>(T entity, bool saveChanges = true) where T : class
        {
            ValidationHelper.ValidateEntity(entity);
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

    }
}