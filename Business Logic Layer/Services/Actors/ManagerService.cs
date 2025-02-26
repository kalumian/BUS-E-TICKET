﻿using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Business_Logic_Layer.Services.Actors
{
    public class ManagerService : BaseUserService
    {
        private IMapper _mapper;

        public ManagerService(UserManager<AuthoUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(userManager, unitOfWork, httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RegisterManagerAccountDTO> RegisterAsync(RegisterManagerAccountDTO registerManagerAccountDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Verify role
                //EnsureAdminRole();

                var ManagerCreatrer = _unitOfWork.Managers.FirstOrDefault(i => i.AccountID == registerManagerAccountDTO.CreatedByID) ??
                    throw new BadRequestException("Creater Id Is Wrong");
                AuthoUser user = await CreateUserAccountAsync(registerManagerAccountDTO.Account);

                registerManagerAccountDTO.CreatedByID = ManagerCreatrer.ManagerID.ToString();
                // Map and set the manager entity
                var manager = _mapper.Map<ManagerEntity>(registerManagerAccountDTO);
                manager.Account = user;
                // Add manager entity
                await CreateEntityAsync(manager, saveChanges: false);

                // Save all changes
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return the DTO
                return _mapper.Map<RegisterManagerAccountDTO>(manager);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<List<ManagerDTO>> GetAllManagersAsync()
        {
            try
            {
                var managers = await _unitOfWork.Managers
                      .GetAllQueryable()
                      .Include(m => m.Account)
                      .Include(m => m.CreatedBy)
                      .ThenInclude(c=> c.CreatedBy.Account)
                      .ToListAsync();

                return _mapper.Map<List<ManagerDTO>>(managers);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to retrieve manager accounts, " + ex.Message);
            }
        }     
        public async Task<ManagerDTO> GetManagerByID(string id)
        {
            try
            {
                var manager = await _unitOfWork.Managers
                   .GetAllQueryable()
                   .Include(m => m.CreatedBy).ThenInclude(c => c.Account)
                   .Include(c => c.Account)
                   .Where(m => m.AccountID == id)
                   .FirstOrDefaultAsync();

                return _mapper.Map<ManagerDTO>(manager);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Failed to retrieve manager accounts, " + ex.Message);
            }
        }

        public async Task<ManagerDTO> UpdateManagerAsync(string id, RegisterManagerAccountDTO updateDTO)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var manager = await _unitOfWork.Managers
                 .GetAllQueryable()
                 .Include(m => m.Account)
                 .FirstOrDefaultAsync(m => m.AccountID == id)
                 ?? throw new NotFoundException("Manager not found");

                manager.Account.Email = updateDTO.Account.Email;
                manager.Account.PhoneNumber = updateDTO.Account.PhoneNumber;
                manager.Account.AccountStatus = updateDTO.Account.EnAccountStatus; // Update status

                if (string.IsNullOrWhiteSpace(manager.Account.Email) || !new EmailAddressAttribute().IsValid(manager.Account.Email))
                {
                    throw new BadRequestException("Invalid email format.");
                }

                if (string.IsNullOrWhiteSpace(manager.Account.PhoneNumber) || !Regex.IsMatch(manager.Account.PhoneNumber, @"^\+?\d{9,15}$"))
                {
                    throw new BadRequestException("Invalid phone number format.");
                }
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<ManagerDTO>(manager);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteManager(string id)
        {
            var manager = await _unitOfWork.Managers.GetAllQueryable().Include(i=>i.Account).FirstOrDefaultAsync(m => m.AccountID == id) ?? throw new NotFoundException("Manager not found.");

            manager.Account.AccountStatus = EnAccountStatus.Deleted;

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
