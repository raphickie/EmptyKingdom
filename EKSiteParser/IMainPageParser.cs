
namespace EK.SiteParser
{
    public interface IMainPageParser
    {
        IMainPageItem[] ParseLastMessages(string pageText);
    }
}
