using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Item item)
        {
            await _db.Items.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _db.Items.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> ListAsync()
        {
            return await _db.Items.ToListAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            _db.Items.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Item item)
        {
            _db.Items.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}