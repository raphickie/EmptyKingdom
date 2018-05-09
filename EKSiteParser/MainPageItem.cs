using System;

namespace EK.SiteParser
{
    class MainPageItem : IMainPageItem
    {
        public string CategoryName { get; set; }

        public DateTime CreationDate { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public MainPageItem(string name, string url, string categoryName, DateTime creationDate)
        {
            Name = name;
            Url = url;
            CategoryName = categoryName;
            CreationDate = creationDate;
        }
    }
}
