using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IHasCardService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<HasCard> lst);
        Task<HasCard> Get(Guid id);
        Task<HasCard> Insert(HasCard obj);
        Task<List<HasCard>> InsertRange(List<HasCard> lst);
        Task<HasCard> Update(HasCard obj);
        Task<List<HasCard>> UpdateRange(List<HasCard> lst);
    }

    public class HasCardService : IHasCardService
    {
        private readonly ApplicationDbContext _context; 

        public HasCardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            HasCard obj = await this.Get(id);
            _context.HasCard.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<HasCard> lst)
        {
            _context.HasCard.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<HasCard> Get(Guid id)
        {
            return await _context.HasCard.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HasCard> Insert(HasCard obj)
        {
            _context.HasCard.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<HasCard>> InsertRange(List<HasCard> lst)
        {
            _context.HasCard.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<HasCard> Update(HasCard obj)
        {
            _context.HasCard.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<HasCard>> UpdateRange(List<HasCard> lst)
        {
            _context.HasCard.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}