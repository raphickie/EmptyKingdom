using System;
using System.Threading.Tasks;

namespace EK.SiteParser
{
    public interface IEkSiteParser
    {
        Task<Tuple<IMainPageItem, IDetailsPageItem>[]> ParseSiteAsync(string sitePage);
    }
}
