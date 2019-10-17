using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Domain.ExtendedModels
{
    public class OwnerExtended
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public OwnerExtended()
        {
        }

        public OwnerExtended(Owner owner)
        {
            Id = owner.Id;
            Name = owner.Name;
            DateOfBirth = owner.DateOfBirth;
            Address = owner.Address;
        }
    }
}
