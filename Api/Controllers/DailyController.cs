using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Api.Helpers;
using Api.Models;
using EF;

namespace Api.Controllers
{
    public class DailyController : ControllerBase
    {
        [HttpPost]
        public DailyTextViewModel GetText()
        {
            var today = DateTime.UtcNow.Date;
            using (var context = new InfostyleEntities())
            {
                var text = context.DailyTexts
                    .Where(t => t.PublicationDate == today)
                    .OrderBy(_ => Guid.NewGuid())
                    .FirstOrDefault();
                // TODO: remove text, already edited by current user
                if (text == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return new DailyTextViewModel(text.Id, text.Text);
            }
        }

        [HttpPost]
        public void PostEdit(int textId, string editResult)
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            using (var context = new InfostyleEntities())
            {
                var edit = new DailyEdit
                {
                    UserId = userId,
                    TimeTaken = TimeSpan.FromSeconds(0),
                    Result = editResult,
                    DailyTextId = textId,
                    Time = DateTime.UtcNow
                };
                context.DailyEdits.Add(edit);
                context.SaveChanges();
            }
        }

        [HttpPost]
        public DailyCompareViewModel GetEditsToCompare()
        {
            const int competitorConut = 2;
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            var lowerBound = DateTime.UtcNow.Date.AddDays(-1);

            using (var context = new InfostyleEntities())
            {
                //todo Более лучшие алгоритмы подбора пары
                var group = context.DailyEdits
                    .OrderBy(_ => Guid.NewGuid())
                    .Where(e => e.Time > lowerBound && e.UserId != userId)
                    .GroupBy(e => e.DailyTextId)
                    .First(g => g.Count() >= competitorConut);

                var competitors = group.OrderBy(_ => Guid.NewGuid()).Take(competitorConut).ToArray();
                var origin = context.DailyTexts.First(t => t.Id == competitors.First().DailyTextId);

                return new DailyCompareViewModel
                {
                    TaskId = origin.Id,
                    InitialText = origin.Text,
                    Competitors = competitors.Select(c => new Competitor {Id = c.Id, Text = c.Result}).ToArray()
                };
            }
        }
    }
}