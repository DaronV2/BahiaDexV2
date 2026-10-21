using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IEnvyService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Envy> lst);
        Task<Envy> Get(Guid id);
        Task<Envy> Insert(Envy obj);
        Task<List<Envy>> InsertRange(List<Envy> lst);
        Task<Envy> Update(Envy obj);
        Task<List<Envy>> UpdateRange(List<Envy> lst);
    }

    public class EnvyService : IEnvyService
    {
        private readonly ApplicationDbContext _context; 

        public EnvyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            Envy obj = await this.Get(id);
            _context.Envy.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Envy> lst)
        {
            _context.Envy.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<Envy> Get(Guid id)
        {
            return await _context.Envy.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Envy> Insert(Envy obj)
        {
            _context.Envy.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Envy>> InsertRange(List<Envy> lst)
        {
            _context.Envy.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<Envy> Update(Envy obj)
        {
            _context.Envy.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Envy>> UpdateRange(List<Envy> lst)
        {
            _context.Envy.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}