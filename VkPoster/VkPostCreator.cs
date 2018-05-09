using EK.Core;
using EK.VkPosterLib.ImgDownload;
using log4net;
using System;
using System.Threading.Tasks;
using VkPosterLib;

namespace EK.VkPosterLib
{
    public class VkPostCreator : IVkPostCreator
    {
        private readonly IVkConfiguration _config;
        private readonly IVkPoster _vkPoster;
        private readonly IImageLoader _imgLoader;
        private readonly ILog _logger;

        public VkPostCreator(IVkConfiguration config, IImageLoader imageLoader, IVkPoster vkPoster)
        {
            _config = config;
            _vkPoster = vkPoster;
            _imgLoader = imageLoader;
            _logger = LogManager.GetLogger(this.GetType().Name);
        }

        public async Task<long> PostToVkAsync(string message, string[] imageUrls)
        {
            _logger.Info($"Creating post with message '{message}'...");
            _logger.Info($"Uploading images...");
            var photoIds = await _imgLoader.UploadToVkAsync(imageUrls);
            _logger.Info($"{photoIds.Length} images uploaded");
            _logger.Info("Posting to wall...");
            var postResult = await _vkPoster.WallPostAsync(-_config.GroupId, _config.AccessToken, true, message, photoIds, _config.Version);
            _logger.Info($"Message {message} posted successfully");
            return postResult;
        }
    }
}
