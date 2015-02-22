using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Api.Helpers;
using EF;

namespace Api.Controllers
{
    public class DailyController : ControllerBase
    {
		[HttpPost]
		public DailyTextViewModel GetText()
        {
			var today = DateTime.Now.ToUniversalTime().Date;
			using (var context = new InfostyleEntities())
	        {
		        var text = context.DailyTexts
					.Where(t => t.PublicationDate == today)
			        .OrderBy(_ => Guid.NewGuid())
			        .FirstOrDefault();
				// TODO: remove text, already edited by current user
				if (text == null)
					throw new HttpException(404, "Sorry, no texts");
				return new DailyTextViewModel(text.Id, text.Text);
	        }
        }

		public void PostEdit(int textId, string editResult)
		{
			int userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
			using (var context = new InfostyleEntities())
			{
				var edit = new DailyEdit()
				{
					UserId = userId,
					TimeTaken = TimeSpan.FromSeconds(0),
					Result = editResult,
					DailyTextId = textId,
					Time = DateTime.Now.ToUniversalTime()
				};
				context.DailyEdits.Add(edit);
				context.SaveChanges();
			}
		}

	    public DailyCompareViewModel GetEditsToCompare()
	    {
		    throw new NotImplementedException();
	    }

	}

	public class DailyCompareViewModel
	{
		public int TaskId { get; set; }
		public string InitialText { get; set; }
		public string Edit1 { get; set; }
		public int Edit1Id { get; set; }
		public string Edit2 { get; set; }
		public int Edit2Id { get; set; }
	}

	public class DailyTextViewModel
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public DailyTextViewModel(int id, string text)
		{
			Id = id;
			Text = text;
		}
	}
}