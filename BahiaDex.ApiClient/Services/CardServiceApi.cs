using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface ICardServiceApi
    {
        Task<Card> Get(Guid id);
        Task<List<Card>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Card> UpSert(Card card);
    }

    public class CardServiceApi : ICardServiceApi
    {
        public async Task<Card> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Card>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Card>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Card>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Card>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Card>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Card>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Card> UpSert(Card card)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Card>());
                string response = await Http.HttpPost(link, card);
                return JsonConvert.DeserializeObject<Card>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
