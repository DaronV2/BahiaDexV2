using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface ISerieService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Serie> lst);
        Task<Serie> Get(Guid id);
        Task<Serie> Insert(Serie obj);
        Task<List<Serie>> InsertRange(List<Serie> lst);
        Task<Serie> Update(Serie obj);
        Task<List<Serie>> UpdateRange(List<Serie> lst);
    }

    public class SerieService : ISerieService
    {
        private readonly ApplicationDbContext _context; 

        public SerieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            Serie obj = await this.Get(id);
            _context.Serie.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Serie> lst)
        {
            _context.Serie.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<Serie> Get(Guid id)
        {
            return await _context.Serie.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Serie> Insert(Serie obj)
        {
            _context.Serie.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Serie>> InsertRange(List<Serie> lst)
        {
            _context.Serie.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<Serie> Update(Serie obj)
        {
            _context.Serie.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Serie>> UpdateRange(List<Serie> lst)
        {
            _context.Serie.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}