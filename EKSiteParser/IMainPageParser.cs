using System.Threading.Tasks;

namespace EKSiteParser
{
    public interface IMainPageParser
    {
        Task<IMainPageItem> GetLastMessagesAsync(string pageText);
    }
}
