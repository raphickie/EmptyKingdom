
using System.Threading.Tasks;

namespace VkPosterLib
{
    public interface IVkPoster
    {
        Task WallPostAsync(int ownerId, string accessToken, bool fromGroup, string message);
    }
}
