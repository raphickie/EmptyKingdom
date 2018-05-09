using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EK.Core;
using System;

namespace EK.SiteParser
{
    public class EkSiteParser : IEkSiteParser
    {
        private readonly IMainPageParser _mainPageParser;
        private readonly IDetailsPageParser _detailsPageParser;
        private readonly IHttpGetter _httpGetter;
        private readonly IParserSettings _parserSettings;

        public EkSiteParser(IMainPageParser mainPageParser, IDetailsPageParser detailsPageParser, IParserSettings parserSettings, IHttpGetter httpGetter)
        {
            _mainPageParser = mainPageParser;
            _detailsPageParser = detailsPageParser;
            _httpGetter = httpGetter;
            _parserSettings = parserSettings;
        }

        public async Task<Tuple<IMainPageItem, IDetailsPageItem>[]> ParseSiteAsync(string sitePage)
        {
            var pageText = await _httpGetter.GetStringAsync(sitePage);
            var pageItems = _mainPageParser.ParseLastMessages(pageText);

            var detailsList = new List<Tuple<IMainPageItem, IDetailsPageItem>>();
            foreach (var item in pageItems.OrderByDescending(i => i.CreationDate))
            {
                var detailsPageText = await _httpGetter.GetStringAsync(item.Url);
                var details = _detailsPageParser.ParseDetails(detailsPageText);
                detailsList.Add(new Tuple<IMainPageItem,IDetailsPageItem>(item, details));
            }
            return detailsList.ToArray();
        }
    }
}
