using Microsoft.EntityFrameworkCore;

namespace BahiaDex.Api.Services
{
    public interface ICardService
    {
        Task Delete(Guid id);
        Task DeleteRange(List<Card> lst);
        Task<Card> Get(Guid id);
        Task<Card> Insert(Card obj);
        Task<List<Card>> InsertRange(List<Card> lst);
        Task<Card> Update(Card obj);
        Task<List<Card>> UpdateRange(List<Card> lst);
    }

    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _contextDB; 

        public CardService(ApplicationDbContext context)
        {
            _contextDB = context;
        }

        public async Task Delete(Guid id)
        {
            Card obj = await this.Get(id);
            _contextDB.Card.Remove(obj);
            await _contextDB.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Card> lst)
        {
            _contextDB.Card.RemoveRange(lst);
            await _contextDB.SaveChangesAsync();
        }

        public async Task<Card> Get(Guid id)
        {
            return await _contextDB.Card.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Card> Insert(Card obj)
        {
            _contextDB.Card.Add(obj);
            await _contextDB.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Card>> InsertRange(List<Card> lst)
        {
            _contextDB.Card.AddRange(lst);
            await _contextDB.SaveChangesAsync();
            return lst;
        }

        public async Task<Card> Update(Card obj)
        {
            _contextDB.Card.Update(obj);
            await _contextDB.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Card>> UpdateRange(List<Card> lst)
        {
            _contextDB.Card.UpdateRange(lst);
            await _contextDB.SaveChangesAsync();
            return lst;
        }
    }
}