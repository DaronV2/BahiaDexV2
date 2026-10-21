using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IBinderService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Binder> lst);
        Task<Binder> Get(Guid id);
        Task<Binder> Insert(Binder obj);
        Task<List<Binder>> InsertRange(List<Binder> lst);
        Task<Binder> Update(Binder obj);
        Task<List<Binder>> UpdateRange(List<Binder> lst);
    }

    public class BinderService : IBinderService
    {
        private readonly ApplicationDbContext _contextDB; 

        public BinderService(ApplicationDbContext context)
        {
            _contextDB = context;
        }

        public async Task Delete(Guid id)
        {
            Binder obj = await this.Get(id);
            _contextDB.Binder.Remove(obj);
            await _contextDB.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Binder> lst)
        {
            _contextDB.Binder.RemoveRange(lst);
            await _contextDB.SaveChangesAsync();
        }

        public async Task<Binder> Get(Guid id)
        {
            return await _contextDB.Binder.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Binder> Insert(Binder obj)
        {
            _contextDB.Binder.Add(obj);
            await _contextDB.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Binder>> InsertRange(List<Binder> lst)
        {
            _contextDB.Binder.AddRange(lst);
            await _contextDB.SaveChangesAsync();
            return lst;
        }

        public async Task<Binder> Update(Binder obj)
        {
            _contextDB.Binder.Update(obj);
            await _contextDB.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Binder>> UpdateRange(List<Binder> lst)
        {
            _contextDB.Binder.UpdateRange(lst);
            await _contextDB.SaveChangesAsync();
            return lst;
        }
    }
}