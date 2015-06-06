using System;
using System.Collections.Generic;
using System.IO;

namespace Api.Controllers
{
	public class LevelInfo
	{
		public LevelInfo(int levelIndex)
		{
			LevelIndex = levelIndex;
		}

		public readonly int LevelIndex;
		public readonly List<Task> Cards = new List<Task>();
		public readonly List<Task> CrawlLines = new List<Task>();

		public static LevelInfo LoadFrom(int levelIndex, FileInfo levelFile)
		{
			try
			{
				return LoadFrom(levelIndex, File.ReadLines(levelFile.FullName));
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Cant load level {0} from {1}. {2}", levelIndex, levelFile.FullName, e.Message), e);
			}
		}

		public static LevelInfo LoadFrom(int levelIndex, IEnumerable<string> lines)
		{
			var level = new LevelInfo(levelIndex);
			bool readCards = true;
			foreach (var line in lines)
			{
				if (line.Trim().Length == 0 || line.StartsWith("#")) continue;
				if (readCards)
				{
					if (line.Trim().Contains("CRAWL LINE")) readCards = false;
					else level.Cards.Add(Task.Parse(line, levelIndex));
				}
				else
				{
					level.CrawlLines.Add(Task.Parse(line, levelIndex));
				}
			}
			return level;
		}
	}
}