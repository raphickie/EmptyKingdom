using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EK.Core
{
	public class HttpGetter : IHttpGetter
	{
		private readonly HttpClient _client;

		public HttpGetter(HttpClient client)
		{
			_client = client;
		}

		public async Task<string> GetStringAsync(string query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return await _client.GetStringAsync(query);
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
