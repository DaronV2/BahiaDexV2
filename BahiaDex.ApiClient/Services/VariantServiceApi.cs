using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IVariantServiceApi
    {
        Task<Variant> Get(Guid id);
        Task<List<Variant>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Variant> UpSert(Variant variant);
    }

    public class VariantServiceApi : IVariantServiceApi
    {
        public async Task<Variant> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Variant>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Variant>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Variant>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Variant>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Variant>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Variant>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Variant> UpSert(Variant variant)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Variant>());
                string response = await Http.HttpPost(link, variant);
                return JsonConvert.DeserializeObject<Variant>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
