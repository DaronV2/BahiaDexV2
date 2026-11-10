using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IEmplacementCardServiceApi
    {
        Task<EmplacementCard> Get(Guid id);
        Task<List<EmplacementCard>> GetAll();
        Task<bool> Delete(Guid id);
        Task<EmplacementCard> UpSert(EmplacementCard emplacementCard);
    }

    public class EmplacementCardServiceApi : IEmplacementCardServiceApi
    {
        public async Task<EmplacementCard> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<EmplacementCard>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<EmplacementCard>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<EmplacementCard>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<EmplacementCard>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<EmplacementCard>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<EmplacementCard>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<EmplacementCard> UpSert(EmplacementCard emplacementCard)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<EmplacementCard>());
                string response = await Http.HttpPost(link, emplacementCard);
                return JsonConvert.DeserializeObject<EmplacementCard>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
