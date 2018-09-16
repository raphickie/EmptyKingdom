using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EK.SiteParser
{
	public class DetailsPageParser : IDetailsPageParser
	{
		public IDetailsPageItem ParseDetails(string pageText)
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
						var postNode = bodyNode
						.Descendants("div")
						.FirstOrDefault(d =>
						   d.Attributes.Contains("class") &&
						   d.Attributes["class"].Value.Contains("entry-content"));

						if (postNode == null)
							return null;

						var imagePaths = postNode.Descendants("img")
							.Where(i => i.Attributes.Contains("src"))
							.Select(i => i.Attributes["src"].Value).ToArray();

						var tagsSpan = bodyNode.Descendants("span")
							.FirstOrDefault(d => d.HasClass("tags-links"));

						string[] tags;

						if (tagsSpan == null)
							tags = new string[0];
						else
							tags = tagsSpan.ParentNode.Descendants("a")
							   .Where(a => a.Attributes.Contains("rel") &&
										   a.Attributes["rel"].Value == "tag")
							   .Select(a => a.InnerText.Replace(" ", string.Empty))
							   .Except(new[] { "website" })
							   .ToArray();

						return new DetailsPageItem(imagePaths, tags);
					}
				}
			}
			return null;
		}
	}
}
