
using System;
using System.Threading.Tasks;

namespace EK.Core
{
	public interface IHttpGetter : IDisposable
	{
		Task<string> GetStringAsync(string query);
	}
}
