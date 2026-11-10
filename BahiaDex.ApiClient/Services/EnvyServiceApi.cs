using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IEnvyServiceApi
    {
        Task<Envy> Get(Guid id);
        Task<List<Envy>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Envy> UpSert(Envy envy);
    }

    public class EnvyServiceApi : IEnvyServiceApi
    {
        public async Task<Envy> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Envy>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Envy>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Envy>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Envy>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Envy>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Envy>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Envy> UpSert(Envy envy)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Envy>());
                string response = await Http.HttpPost(link, envy);
                return JsonConvert.DeserializeObject<Envy>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
