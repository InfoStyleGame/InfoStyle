using System;
using System.Collections.Generic;
using EF;
using NUnit.Framework;

namespace Api.Ranking
{
	public enum CompareResult
	{
		FirstWin = 2,
		SecondWin = 0,
		Draw = 1
	}
	public class Ranker
	{
		public IEnumerable<DailyCompare> AddCompare(int userId, DailyEdit edit1, DailyEdit edit2, DateTime timestamp, CompareResult compareResult)
		{
			int score = (int) compareResult;
			UpdateRank(edit1.User, edit2.User, score / 2.0);
			UpdateRank(edit2.User, edit1.User, 1 - score / 2.0);
			yield return new DailyCompare
			{
				EditId = edit1.Id,
				OpponentEditId = edit2.Id,
				Time = timestamp,
				UserId = userId,
				Score = (short) score
			};
			yield return new DailyCompare
			{
				EditId = edit2.Id,
				OpponentEditId = edit1.Id,
				Time = timestamp,
				UserId = userId,
				Score = (short) (2 - score)
			};
		}

		public void UpdateRank(User player, User opponent, double score)
		{
			var sensitivity = 100; // TODO: Decrease for high ranks
			player.Rank += (int)(sensitivity * (score - EstimateScore(player.Rank, opponent.Rank)));
		}

		public double EstimateScore(int rank1, int rank2)
		{
			return 1 / (1 + Math.Pow(10, (rank2 - rank1) / 400.0));
		}
	}

	[TestFixture]
	public class Ranker_should
	{
		private Ranker ranker = new Ranker();

		[Test]
		public void estimate_equal_score_for_equal_ranks()
		{
			var estimate = ranker.EstimateScore(1000, 1000);
			Assert.That(estimate, Is.EqualTo(0.5));
		}

		[Test]
		public void estimate_win_of_strongest()
		{
			var estimate = ranker.EstimateScore(2000, 1000);
			Assert.That(estimate, Is.GreaterThan(0.5));
		}

		[Test]
		public void estimate_lose_of_weakest()
		{
			var estimate = ranker.EstimateScore(1000, 2000);
			Assert.That(estimate, Is.LessThan(0.5));
		}
		
		[Test]
		public void not_change_rank_on_draw_of_equal_opponents()
		{
			var chip = new User { Rank = 1000 };
			var dale = new User { Rank = 1000 };
			ranker.UpdateRank(chip, dale, 0.5);
			Console.WriteLine(chip.Rank);
			Assert.That(chip.Rank, Is.EqualTo(1000));
		}
	
		[Test]
		public void increase_rank_after_win()
		{
			var chip = new User { Rank = 1000 };
			var dale = new User { Rank = 1000 };
			ranker.UpdateRank(chip, dale, 1);
			Console.WriteLine(chip.Rank);
			Assert.That(chip.Rank, Is.GreaterThan(1000));
		}
		[Test]
		public void decrease_rank_after_lose()
		{
			var chip = new User { Rank = 1000 };
			var dale = new User { Rank = 1000 };
			ranker.UpdateRank(chip, dale, 0);
			Console.WriteLine(chip.Rank);
			Assert.That(chip.Rank, Is.LessThan(1000));
		}

		[Test]
		public void decrease_strong_player_rank_after_draw()
		{
			var chip = new User { Rank = 2000 };
			var dale = new User { Rank = 1000 };
			ranker.UpdateRank(chip, dale, 0.5);
			Console.WriteLine(chip.Rank);
			Assert.That(chip.Rank, Is.LessThan(2000));
		}

		[Test]
		public void increase_weak_player_rank_after_draw()
		{
			var chip = new User { Rank = 1000 };
			var dale = new User { Rank = 2000 };
			ranker.UpdateRank(chip, dale, 0.5);
			Console.WriteLine(chip.Rank);
			Assert.That(chip.Rank, Is.GreaterThan(1000));
		}
	}

}