using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IHasCardServiceApi
    {
        Task<HasCard> Get(Guid id);
        Task<List<HasCard>> GetAll();
        Task<bool> Delete(Guid id);
        Task<HasCard> UpSert(HasCard hasCard);
    }

    public class HasCardServiceApi : IHasCardServiceApi
    {
        public async Task<HasCard> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<HasCard>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<HasCard>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<HasCard>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<HasCard>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<HasCard>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<HasCard>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<HasCard> UpSert(HasCard hasCard)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<HasCard>());
                string response = await Http.HttpPost(link, hasCard);
                return JsonConvert.DeserializeObject<HasCard>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
