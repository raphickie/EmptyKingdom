using System.Threading.Tasks;

namespace EK.VkPosterLib
{
    public interface IVkPostCreator
    {
        Task<long> PostToVkAsync(string message, string[] imageUrls);
    }
}