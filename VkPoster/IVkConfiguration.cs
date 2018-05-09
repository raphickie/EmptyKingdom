namespace EK.VkPosterLib
{
    public interface IVkConfiguration
    {
        string AccessToken { get; }

        long AlbumId { get; }

        long GroupId { get; }

        string Version { get; }
    }
}
