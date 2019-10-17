using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Domain;
using MyWebApi.Domain.ExtendedModels;

namespace MyWebApi.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly DataContext _dataContext;
        public OwnerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Owner>> GetOwnersAsync()
        {
            return await _dataContext.Owners.ToListAsync();
        }

        public async Task<Owner> GetOwnerByIdAsync(Guid ownerId)
        {
            return await _dataContext.Owners.SingleOrDefaultAsync(x => x.Id == ownerId);
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            _dataContext.Owners.Add(owner);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateOwnerAsync(Owner ownerToUpdate)
        {
            _dataContext.Owners.Update(ownerToUpdate);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteOwnerAsync(Guid ownerId)
        {
            var owner = new Owner { Id = ownerId };

            _dataContext.Owners.Attach(owner);

            if (owner == null)
                return false;

            _dataContext.Owners.Remove(owner);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<OwnerExtended> GetOwnerWithDetailsAsync(Guid ownerId)
        {
            var owner = await GetOwnerByIdAsync(ownerId);

            return new OwnerExtended(owner)
            {
                Accounts = _dataContext.Accounts.Where(x => x.Id == ownerId)
            };
        }
    }
}
