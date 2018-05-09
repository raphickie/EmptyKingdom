using System.Threading.Tasks;

namespace EK.SiteParser
{
    public interface IDetailsPageParser
    {
        IDetailsPageItem ParseDetails(string pageText);
    }
}
