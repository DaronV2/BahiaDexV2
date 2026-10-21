using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IPageService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Page> lst);
        Task<Page> Get(Guid id);
        Task<Page> Insert(Page obj);
        Task<List<Page>> InsertRange(List<Page> lst);
        Task<Page> Update(Page obj);
        Task<List<Page>> UpdateRange(List<Page> lst);
    }

    public class PageService : IPageService
    {
        private readonly ApplicationDbContext _context; 

        public PageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            Page obj = await this.Get(id);
            _context.Page.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Page> lst)
        {
            _context.Page.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<Page> Get(Guid id)
        {
            return await _context.Page.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Page> Insert(Page obj)
        {
            _context.Page.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Page>> InsertRange(List<Page> lst)
        {
            _context.Page.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<Page> Update(Page obj)
        {
            _context.Page.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Page>> UpdateRange(List<Page> lst)
        {
            _context.Page.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}