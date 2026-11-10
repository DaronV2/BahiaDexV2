using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface ISetServiceApi
    {
        Task<Set> Get(Guid id);
        Task<List<Set>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Set> UpSert(Set set);
    }

    public class SetServiceApi : ISetServiceApi
    {
        public async Task<Set> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Set>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Set>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Set>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Set>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Set>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Set>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Set> UpSert(Set set)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Set>());
                string response = await Http.HttpPost(link, set);
                return JsonConvert.DeserializeObject<Set>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
