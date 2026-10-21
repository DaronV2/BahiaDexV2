using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IVariantService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Variant> lst);
        Task<Variant> Get(Guid id);
        Task<Variant> Insert(Variant obj);
        Task<List<Variant>> InsertRange(List<Variant> lst);
        Task<Variant> Update(Variant obj);
        Task<List<Variant>> UpdateRange(List<Variant> lst);
    }

    public class VariantService : IVariantService
    {
        private readonly ApplicationDbContext _context; 

        public VariantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            Variant obj = await this.Get(id);
            _context.Variant.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Variant> lst)
        {
            _context.Variant.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<Variant> Get(Guid id)
        {
            return await _context.Variant.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Variant> Insert(Variant obj)
        {
            _context.Variant.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Variant>> InsertRange(List<Variant> lst)
        {
            _context.Variant.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<Variant> Update(Variant obj)
        {
            _context.Variant.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Variant>> UpdateRange(List<Variant> lst)
        {
            _context.Variant.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}