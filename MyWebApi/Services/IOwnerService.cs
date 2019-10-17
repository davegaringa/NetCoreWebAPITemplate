using MyWebApi.Domain;
using MyWebApi.Domain.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public interface IOwnerService
    {
        Task<List<Owner>> GetOwnersAsync();
        Task<bool> CreateOwnerAsync(Owner owner);
        Task<Owner> GetOwnerByIdAsync(Guid ownerId);
        Task<bool> UpdateOwnerAsync(Owner ownerToUpdate);
        Task<bool> DeleteOwnerAsync(Guid ownerId);
        Task<OwnerExtended> GetOwnerWithDetailsAsync(Guid ownerId);
    }
}
