using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IItemRepository
    {
        Task AddAsync(Item item);
        Task<Item> GetByIdAsync(int id);
        Task<IEnumerable<Item>> ListAsync();
        Task UpdateAsync(Item item);
        Task DeleteAsync(Item item);
    }
}