using Newtonsoft.Json;
using BahiaDex.ApiClient.Common;

namespace BahiaDex.ApiClient.Services
{
    public interface IUserServiceApi
    {
        Task<User> Get(string id);
        Task<List<User>> GetAll();
        Task<bool> Delete(string id);
        Task<User> UpSert(User user);
    }

    public class UserServiceApi : IUserServiceApi
    {
        public async Task<User> Get(string id)
        {
            try
            {
                string link = $"{BahiaDexLinks.ObjectUri<User>()}{Uri.EscapeDataString(id)}";
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<User>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                string link = BahiaDexLinks.GetAll(BahiaDexLinks.ObjectUri<User>());
                string response = await Http.HttpGet(link);
                return JsonConvert.DeserializeObject<List<User>>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                string link = $"{BahiaDexLinks.ObjectUri<User>()}{Uri.EscapeDataString(id)}";
                return await Http.HttpDelete(link);
            }
            catch (Exception) { return false; }
        }

        public async Task<User> UpSert(User user)
        {
            try
            {
                string link = BahiaDexLinks.Update(BahiaDexLinks.ObjectUri<User>());
                string response = await Http.HttpPost(link, user);
                return JsonConvert.DeserializeObject<User>(response) ?? new();
            }
            catch (Exception) { return new(); }
        }
    }
}
