using Microsoft.VisualStudio.TestTools.UnitTesting;
using EK.VkPosterLib.ImgDownload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EK.Core;
using VkPosterLib;

namespace EK.VkPosterLib.ImgDownload.Tests
{
    public class VkConfig : IVkConfiguration
    {
        public string AccessToken { get; set; }

        public long AlbumId { get; set; }

        public long GroupId { get; set; }

        public string Version { get; set; }
    }

    [TestClass]
    public class ImageLoaderTests
    {
        private VkConfig _vkConfig;

        [TestInitialize]
        public void InitTest()
        {
            var albumId = 242149925;
            var groupId = 143112604;
            var accessToken = "dde31cca66dbc3972c0d5e2b13d6c72790963ae4c4840bb88c943dbe1e060142b7ea29d53e3a58bd451ac";
            _vkConfig = new VkConfig() { AccessToken = accessToken, AlbumId = albumId, GroupId = groupId };
        }

        [TestMethod()]
        public async Task UploadAsyncTest()
        {
           
            ImageLoader il = new ImageLoader(_vkConfig, new HttpGetter());
            var r = await il.UploadToVkAsync(new[] { "https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/68dd54ca-60cf-4ef7-898b-26d7cbe48ec7/10-dithering-opt.jpg" });
        }

        [TestMethod()]
        public async Task UploadAsyncTest2_VkPostCreator()
        {
            VkPostCreator pc = new VkPostCreator(_vkConfig, new ImageLoader(_vkConfig,new HttpGetter()),new VkPoster(new HttpGetter()));
            var r = await pc.PostToVkAsync("tt", new[] { "https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/68dd54ca-60cf-4ef7-898b-26d7cbe48ec7/10-dithering-opt.jpg" });
        }
    }
}