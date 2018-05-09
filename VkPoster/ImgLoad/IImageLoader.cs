using System.Threading.Tasks;

namespace EK.VkPosterLib.ImgDownload
{
    public interface IImageLoader
    {
        Task<string[]> UploadToVkAsync(string[] imageUrls);
    }
}
