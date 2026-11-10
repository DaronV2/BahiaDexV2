using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;
using BahiaDex.ApiClient.Models;

namespace BahiaDex.ApiClient.Services
{
    public interface IPageServiceApi
    {
        Task<Page> Get(Guid id);
        Task<List<Page>> GetAll();
        Task<bool> Delete(Guid id);
        Task<Page> UpSert(Page page);
    }

    public class PageServiceApi : IPageServiceApi
    {
        public async Task<Page> Get(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.GetById(BahiaDexLinks.ObjectUri<Page>(), id);
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<Page>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<Page>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<Page>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<Page>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string link = BahiaDexLinks.DeleteById(BahiaDexLinks.ObjectUri<Page>(), id);
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<Page> UpSert(Page page)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<Page>());
                string response = await Http.HttpPost(link, page);
                return JsonConvert.DeserializeObject<Page>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
