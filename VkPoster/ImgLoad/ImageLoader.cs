
using EK.Core;
using log4net;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace EK.VkPosterLib.ImgDownload
{
    public class ImageLoader : IImageLoader
    {
        private readonly IHttpGetter _httpGetter;
        private readonly IVkConfiguration _vkConfiguration;
        private readonly ILog _logger;

        private string GetUploadServerQuery => $"https://api.vk.com/method/photos.getUploadServer?group_id={_vkConfiguration.GroupId}&album_id={_vkConfiguration.AlbumId}&access_token={_vkConfiguration.AccessToken}&v={_vkConfiguration.Version}";

        public ImageLoader(IVkConfiguration config, IHttpGetter httpGetter)
        {
            _vkConfiguration = config;
            _httpGetter = httpGetter;
            _logger = LogManager.GetLogger(this.GetType().Name);
        }

        public async Task<string[]> UploadToVkAsync(string[] imageUrls)
        {
            _logger.Info("Getting upload server");
            var uploadServer = await GetUploadServerAsync();

            _logger.Info($"Gathered upload url: {uploadServer}");

            var photoIds = new List<string>();
            foreach (var imageUrl in imageUrls)
            {
                var newImageName = Guid.NewGuid().ToString() + ".jpg";
                var newImagePath = "TEMP/" + newImageName;
                var client = new HttpClient();
                var c2 = new WebClient();
                if (!Directory.Exists("TEMP"))
                    Directory.CreateDirectory("TEMP");
                c2.DownloadFile(imageUrl, newImagePath);

                photoIds.Add(await UploadAsync(uploadServer, newImagePath));
                _logger.Info($"Uploaded image #{photoIds.Count} of {imageUrls.Length}");
            }
            return photoIds.ToArray();
        }

        private async Task<string> GetUploadServerAsync()
        {
            string responseString;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(GetUploadServerQuery);
                responseString = await response.Content?.ReadAsStringAsync();
                if (responseString.Contains("error_msg"))
                {
                    throw new Exception($"Probably bad request: {responseString}");
                }
            }

            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            try
            {
                string uploadUrl = responseJson.response.upload_url.Value;
                return uploadUrl;
            }
            catch (RuntimeBinderException ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<string> UploadAsync(string uploadUrl, string fileAddress)
        {
            var fileStream = File.OpenRead(fileAddress);
            HttpContent fileStreamContent = new StreamContent(fileStream);

            string response;

            using (var wc = new WebClient())
            {
                var bytes = await wc.UploadFileTaskAsync(new Uri(uploadUrl), fileAddress);
                response = Encoding.ASCII.GetString(bytes);
            }

            var responseDynamic = (dynamic)JsonConvert.DeserializeObject(response);
            long server = responseDynamic.server.Value;
            string photosList = responseDynamic.photos_list.Value;
            string hash = responseDynamic.hash.Value;
            string photoSaveQuery = $"https://api.vk.com/method/photos.save?group_id={_vkConfiguration.GroupId}&album_id={_vkConfiguration.AlbumId}&hash={hash}&photos_list={photosList}&server={server}&access_token={_vkConfiguration.AccessToken}&v={_vkConfiguration.Version}";

            using (var client = new HttpClient())
            {
                var response2 = await client.GetAsync(photoSaveQuery);
                var responseText = await response2.Content?.ReadAsStringAsync();
                var responseResult =  JsonConvert.DeserializeObject<PhotoSaveResponse>(responseText);
                var ownerId = responseResult.Response[0].Owner_Id;
                var pid = responseResult.Response[0].Id;
                    return $"photo{ownerId}_{pid}";

            }

        }

        private async Task<string> SavePhotosAsync(string responseQuery)
        {
            var responseString = await _httpGetter.GetStringAsync(GetUploadServerQuery);

            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            string uploadUrl = responseJson.upload_url.Value;
            return uploadUrl;
        }

        private class PhotoSaveResponse
        {
            public class ResponseObject
            {
                public string Id { get; set; }
                public string Owner_Id { get; set; }
            }

            public ResponseObject[] Response;
        }

    }
}
