using System;

namespace Api.Controllers
{
	public class Task
	{
		public System.Guid Id { get; set; }
		public int Level { get; set; }
		public string Text { get; set; }

		public static Task Parse(string line, int levelIndex)
		{
			try
			{
				var parts = line.Split(new[] { '\t', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
				return new Task
				{
					Id = Guid.Parse(parts[0]),
					Text = parts[1],
					Level = levelIndex
				};
			}
			catch (Exception e)
			{
				throw new Exception(string.Format("Bad format: '{0}'. {1}", line, e.Message), e);
			}
		}
	}
}