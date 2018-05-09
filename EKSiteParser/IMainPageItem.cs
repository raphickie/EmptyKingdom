using System;

namespace EK.SiteParser
{
    public interface IMainPageItem
    {
        string Name { get; set; }

        string Url { get; set; }

        string CategoryName { get; set; }

        DateTime CreationDate { get; set; }
    }
}
