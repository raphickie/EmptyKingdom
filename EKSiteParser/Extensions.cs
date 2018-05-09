using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EK.SiteParser
{
	public static class Extensions
	{
		public static string Clean(this string str)
		{
			// TODO: Переделать
			return str.Replace("\r", string.Empty)
					  .Replace("\n", string.Empty)
					  .Replace("\t", string.Empty)
					  .Trim();
		}

		private static bool HasClass(this HtmlNode node, string className)
		{
			return node.Attributes.Contains("class") &&
				   node.Attributes["class"].Value.Contains(className);
		}
	}
}
