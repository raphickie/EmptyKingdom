using System.Threading.Tasks;

namespace VkPosterLib
{
    public interface IVkPoster
    {
        Task<long> WallPostAsync(long ownerId, string accessToken, bool fromGroup, string message, string[] attachments, string version);
    }
}
