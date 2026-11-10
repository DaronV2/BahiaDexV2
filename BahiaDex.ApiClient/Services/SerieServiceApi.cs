using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface ISerieServiceApi
    {
        Task<Serie> Get(Guid id);
        Task<List<Serie>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Serie> UpSert(Serie serie);
    }

    public class SerieServiceApi : ISerieServiceApi
    {
        public async Task<Serie> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Serie>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Serie>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Serie>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Serie>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Serie>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Serie>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Serie> UpSert(Serie serie)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Serie>());
                string response = await Http.HttpPost(link, serie);
                return JsonConvert.DeserializeObject<Serie>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
