using EK.SiteParser;
using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;

namespace EK.SiteParser
{
	public class MainPageParser : IMainPageParser
	{
		public IMainPageItem[] ParseLastMessages(string pageText)
		{
			HtmlDocument hdoc = new HtmlDocument();
			hdoc.LoadHtml(pageText);

			if (hdoc.ParseErrors != null && hdoc.ParseErrors.Count() > 0)
			{
				// Handle any parse errors as required
			}
			else
			{

				if (hdoc.DocumentNode != null)
				{
					var bodyNode = hdoc.DocumentNode.SelectSingleNode("//body");

					if (bodyNode != null)
					{
						var posts = bodyNode
						.Descendants("div")
						.Where(d =>d.GetAttributeValue("class","").Contains(" post "))
						.ToArray();

						return posts.Select(post => CreatePageItemFromPost(post)).ToArray();
					}
				}
			}
			throw new Exception("couldn't parse main page");
		}

		private IMainPageItem CreatePageItemFromPost(HtmlNode currentPost)
		{
			var link = currentPost.Descendants("a").First();
			
			var entryTitle = currentPost.Descendants("h2").First(d =>
d.HasClass("entry-title"));
			var name = entryTitle.InnerText.Clean();
			var url = link.GetAttributeValue("href", "");

			var categoryDiv = currentPost.Descendants("span")
				.First(d => d.Attributes.Contains("class") &&
				d.Attributes["class"].Value.Contains("cat-links"));

			var categoryName = categoryDiv.InnerText.Clean();

			var desc = currentPost.Descendants("time");
			var dateDiv = desc.First(d => d.HasClass("entry-date"));

			var dateString = dateDiv.InnerText;

			var creationDate = DateTime.ParseExact(dateString, "MMMM d, yyyy", new CultureInfo("en-US"));

			return new MainPageItem(name, url, categoryName, creationDate);
		}

	}
}
