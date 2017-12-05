using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VkPosterLib
{
    public class VkPoster : IVkPoster
    {
        public const string WallPostRequest = "https://api.vk.com/method/wall.post?owner_id={0}&access_token={1}&from_group={2}&message={3}";

        public async Task WallPostAsync(int ownerId, string accessToken, bool fromGroup, string message)
        {
            if (fromGroup && ownerId > 0)
                throw new ArgumentException("If from group, ownerId shold be <0");

            var query = string.Format(WallPostRequest, ownerId, accessToken, fromGroup, message);
            var postId = await PostToVkAsync(query).ConfigureAwait(false);
        }

        private async Task<int> PostToVkAsync(string query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(query).ConfigureAwait(false);
                var responseText = await response.Content?.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseText);
                int postId = responseObject.post_id.Value;
                return postId;
            }
        }
    }
}
