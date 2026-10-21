using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface ISetService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Set> lst);
        Task<Set> Get(Guid id);
        Task<Set> Insert(Set obj);
        Task<List<Set>> InsertRange(List<Set> lst);
        Task<Set> Update(Set obj);
        Task<List<Set>> UpdateRange(List<Set> lst);
    }

    public class SetService : ISetService
    {
        private readonly ApplicationDbContext _context; 

        public SetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            Set obj = await this.Get(id);
            _context.Set.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Set> lst)
        {
            _context.Set.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<Set> Get(Guid id)
        {
            return await _context.Set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Set> Insert(Set obj)
        {
            _context.Set.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Set>> InsertRange(List<Set> lst)
        {
            _context.Set.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<Set> Update(Set obj)
        {
            _context.Set.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Set>> UpdateRange(List<Set> lst)
        {
            _context.Set.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}