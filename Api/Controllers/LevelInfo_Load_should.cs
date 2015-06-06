using NUnit.Framework;

namespace Api.Controllers
{
	[TestFixture]
	public class LevelInfo_Load_should
	{
		[Test]
		public void read_single_cards()
		{
			var level = LevelInfo.LoadFrom(42, new[] { "ABA1396F-C2D6-4F62-940B-173FF2D69237 task one" });
			Assert.AreEqual(1, level.Cards.Count);
			Assert.AreEqual(0, level.CrawlLines.Count);
			Assert.AreEqual(42, level.LevelIndex);
		}

		[Test]
		public void ignore_comments_and_empty_lines()
		{
			var level = LevelInfo.LoadFrom(42, new[] { "#", "# ha!", "", "   ", "ABA1396F-C2D6-4F62-940B-173FF2D69237 task one" });
			Assert.AreEqual(1, level.Cards.Count);
		}

		[Test]
		public void read_crawl_lines()
		{
			var level = LevelInfo.LoadFrom(42, new[] { "CRAWL LINE:", "ABA1396F-C2D6-4F62-940B-173FF2D69237 some crawl line" });
			Assert.AreEqual(1, level.CrawlLines.Count);
			
		}
	}
}