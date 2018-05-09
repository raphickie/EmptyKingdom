using EK.VkPosterLib;
using System;
using System.Configuration;
using System.Globalization;

namespace EK.EmptyKingdom
{
    public class VkConfiguration : IVkConfiguration
    {
        public string AccessToken
        {
            get
            {
                return ConfigurationManager.AppSettings["AccessToken"];
            }
        }

        public long AlbumId
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["AlbumId"]);
            }
        }

        public long GroupId
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["GroupId"]);
            }
        }

        public string Version => ConfigurationManager.AppSettings["ApiVersion"];
    }
}
