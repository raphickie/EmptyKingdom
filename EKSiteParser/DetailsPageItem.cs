
namespace EK.SiteParser
{
    public class DetailsPageItem:IDetailsPageItem
    {
        public string[] ImageUrls { get; set; }

        public string[] Tags { get; set; }

        public DetailsPageItem(string[] imageUrls, string[] tags)
        {
            ImageUrls = imageUrls;
            Tags = tags;
        }
    }
}
