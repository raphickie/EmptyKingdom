using EK.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace VkPosterLib
{
    public class VkPoster : IVkPoster
    {
        public const string WallPostRequest = "https://api.vk.com/method/wall.post?owner_id={0}&access_token={1}&from_group={2}&message={3}&attachments={4}&v={5}";
        public const string WallGetRequest = "https://api.vk.com/method/wall.post?owner_id={0}&access_token={1}&v={2}";
        private readonly IHttpGetter _httpGetterClient;

        public VkPoster(IHttpGetter httpGetterClient)
        {
            _httpGetterClient = httpGetterClient;
        }

        public async Task<long> WallPostAsync(long ownerId, string accessToken, bool fromGroup, string message, string[] attachments,string version)
        {
            if (fromGroup && ownerId > 0)
                throw new ArgumentException("If from group, ownerId shold be <0");

            var attachmentsString = String.Join(",", attachments);
            var query = string.Format(WallPostRequest, ownerId, accessToken, fromGroup, message, attachmentsString, version);
            var postId = await PostToVkAsync(query).ConfigureAwait(false);
            return postId;
        }

        private async Task<long> PostToVkAsync(string query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var responseText = await _httpGetterClient.GetStringAsync(query);

            dynamic responseObject = JsonConvert.DeserializeObject(responseText);
            long postId = responseObject.response.post_id.Value;
            return postId;
        }
    }
}
