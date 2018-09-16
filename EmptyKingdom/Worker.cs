using EK.EmptyKingdom.Properties;
using EK.SiteParser;
using EK.VkPosterLib;
using log4net;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EK.EmptyKingdom
{
	public class Worker : IWorker
	{
		private readonly IEkSiteParser _ekSiteParser;
		private readonly IVkPostCreator _vkPostCreator;
		private readonly IParserSettings _parserSettings;
		private readonly ILog _logger;

		public Worker(IEkSiteParser ekSiteParser, IVkPostCreator vkPostCreator, IParserSettings parserSettings)
		{
			_ekSiteParser = ekSiteParser;
			_vkPostCreator = vkPostCreator;
			_parserSettings = parserSettings;
			_logger = LogManager.GetLogger(this.GetType().Name);
		}

		public async Task UpdateWallAsync()
		{
			try
			{
				_logger.Info("Updater job started");
				var lastPostDate = _parserSettings.LastPostDate;
				_logger.Info($"Last post date: {lastPostDate}");
				_logger.Info($"Parsing site...");
				var ekPosts = await _ekSiteParser.ParseSiteAsync("http://www.emptykingdom.com");
				_logger.Info($"Parsing finished: {ekPosts.Length} new posts found");

				var unpostedPosts = ekPosts.Where(p => p.Item1.CreationDate > lastPostDate).OrderBy(p => p.Item1.CreationDate).ToArray();

				foreach (var post in unpostedPosts)
				{
					_logger.Info($"Posting message {post.Item1.Name}...");
					var message = CreateMessage(post);
					await _vkPostCreator.PostToVkAsync(message, post.Item2.ImageUrls);
					_parserSettings.LastPostDate = post.Item1.CreationDate;
					_logger.Info($"Last post date set to {post.Item1.CreationDate}");
				}

				_logger.Info("Updater job finished.");
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private string CreateMessage(Tuple<IMainPageItem, IDetailsPageItem> post)
		{
			var hashtags = "#" + String.Join(", #", post.Item2.Tags.Select(t => t.Replace(" ", "")));
			var resultMessage = $"{post.Item1.Name}\n[{post.Item1.CategoryName}]\n{hashtags}\n{post.Item1.Url}";

			var resultMessageEncoded = HttpUtility.UrlEncode(resultMessage);

			return resultMessageEncoded;
		}
	}
}
