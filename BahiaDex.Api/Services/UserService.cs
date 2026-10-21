using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface IUserService
    {
        Task Delete(string id);
        Task DeleteRange(List<User> lst);
        Task<User> Get(string id);
        Task<User> Insert(User obj);
        Task<List<User>> InsertRange(List<User> lst);
        Task<User> Update(User obj);
        Task<List<User>> UpdateRange(List<User> lst);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context; 

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(string id)
        {
            User obj = await this.Get(id);
            _context.User.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<User> lst)
        {
            _context.User.RemoveRange(lst);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(string id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.IdGoogleAcc == id);
        }

        public async Task<User> Insert(User obj)
        {
            _context.User.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<User>> InsertRange(List<User> lst)
        {
            _context.User.AddRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }

        public async Task<User> Update(User obj)
        {
            _context.User.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<User>> UpdateRange(List<User> lst)
        {
            _context.User.UpdateRange(lst);
            await _context.SaveChangesAsync();
            return lst;
        }
    }
}