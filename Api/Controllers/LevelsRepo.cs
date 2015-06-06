using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace Api.Controllers
{
	public class LevelsRepo
	{
		public static LevelsRepo Instance = new LevelsRepo(LoadFrom(new DirectoryInfo(Path.Combine(GetAppPath(), "Tasks"))));

		private static string GetAppPath()
		{
			return HostingEnvironment.ApplicationPhysicalPath ?? ".";
		}
		
		public static IEnumerable<LevelInfo> LoadFrom(DirectoryInfo dir)
		{
			int levelIndex = 0;
			while (true)
			{
				var levelFile = dir.GetFiles("level-" + levelIndex + ".txt").FirstOrDefault();
				if (levelFile == null) yield break;

				yield return LevelInfo.LoadFrom(levelIndex++, levelFile);
			}
		}
		public LevelsRepo(IEnumerable<LevelInfo> levels)
		{
			Levels = levels.ToList();
		}

		public List<LevelInfo> Levels;
	}
}