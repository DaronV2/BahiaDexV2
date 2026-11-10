using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IBinderServiceApi
    {
        Task<Binder> Get(Guid IdBinder);
        Task<bool> Delete(Guid IdBinder);
        Task<Binder> UpSert(Binder binder);
        Task<List<Binder>> GetAll();
    }
    public class BinderServiceApi : IBinderServiceApi
    {
        public async Task<Binder> Get(Guid IdBinder)
        {
            try
            {
                // Récupération du json
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Binder>(), IdBinder);
                string response = await Http.HttpGet(link);
                Binder res = JsonConvert.DeserializeObject<Binder>(response) ?? new();
                return res;
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Binder>> GetAll()
        {
            try
            {
                // Récupération du json
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Binder>());
                string response = await Http.HttpGet(link);
                List<Binder> res = JsonConvert.DeserializeObject<List<Binder>>(response) ?? new();
                return res;
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid IdBinder)
        {
            try
            {
                // Récupération du json
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Binder>(), IdBinder);
                bool response = await Http.HttpDelete(link);
                return response;
            }
            catch (Exception) { return new(); }
        }

        public async Task<Binder> UpSert(Binder binder)
        {
            try
            {
                // Récupération du json
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Binder>());
                string response = await Http.HttpPut(link, binder);
                Binder scCreated = JsonConvert.DeserializeObject<Binder>(response) ?? new();
                return scCreated;
            }
            catch (Exception) { return new(); }
        }
    }
}
