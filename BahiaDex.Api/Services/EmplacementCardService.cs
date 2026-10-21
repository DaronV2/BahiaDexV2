using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IEmplacementCardService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<EmplacementCard> lst);
        Task<EmplacementCard> Get(Guid id);
        Task<EmplacementCard> Insert(EmplacementCard obj);
        Task<List<EmplacementCard>> InsertRange(List<EmplacementCard> lst);
        Task<EmplacementCard> Update(EmplacementCard obj);
        Task<List<EmplacementCard>> UpdateRange(List<EmplacementCard> lst);
    }

    public class EmplacementCardService : IEmplacementCardService
    {
        private readonly ApplicationDbContext _context; 

        public EmplacementCardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            EmplacementCard obj = await this.Get(id);
            _context.EmplacementCard.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<EmplacementCard> lst)
        {
            _context.EmplacementCard.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<EmplacementCard> Get(Guid id)
        {
            return await _context.EmplacementCard.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EmplacementCard> Insert(EmplacementCard obj)
        {
            _context.EmplacementCard.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<EmplacementCard>> InsertRange(List<EmplacementCard> lst)
        {
            _context.EmplacementCard.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<EmplacementCard> Update(EmplacementCard obj)
        {
            _context.EmplacementCard.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<EmplacementCard>> UpdateRange(List<EmplacementCard> lst)
        {
            _context.EmplacementCard.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}