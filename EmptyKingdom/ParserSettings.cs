using EK.EmptyKingdom.Properties;
using EK.SiteParser;
using System;

namespace EK.EmptyKingdom
{
    public class ParserSettings : IParserSettings
    {
        public DateTime LastPostDate
        {
            get
            {
                return Settings.Default.LastArticleDate;
            }
            set
            {
                Settings.Default.LastArticleDate = value;
                Settings.Default.Save();
            }
        }
    }
}
