using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EK.Core
{
    public class HttpGetter:IHttpGetter
    {
        public async Task<string> GetStringAsync(string query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(query);
                var responseText = await response.Content?.ReadAsStringAsync();
                return responseText;
            }
        }
    }
}
