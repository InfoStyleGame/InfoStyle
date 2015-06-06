using System.Collections.Generic;

namespace System.Linq
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items, Random random = null)
		{
			random = random ?? new Random();
			var copy = items.ToList();
			for (int i = 0; i < copy.Count; i++)
			{
				var nextIndex = random.Next(i, copy.Count);
				yield return copy[nextIndex];
				copy[nextIndex] = copy[i];
			}
		}
		public static void ShuffleInPlace<T>(this IList<T> items, Random random = null)
		{
			random = random ?? new Random();
			for (int i = 0; i < items.Count; i++)
			{
				var nextIndex = random.Next(i, items.Count);
				var tmp = items[nextIndex];
				items[nextIndex] = items[i];
				items[i] = tmp;
			}
		}
	}
}
