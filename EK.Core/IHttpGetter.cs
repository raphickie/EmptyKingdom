
using System.Threading.Tasks;

namespace EK.Core
{
    public interface IHttpGetter
    {
        Task<string> GetStringAsync(string query);
    }
}
