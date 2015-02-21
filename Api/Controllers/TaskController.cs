using System;
using System.Linq;
using System.Web.Http;
using Api.Models;
using EF;
using EF.Enums;

namespace Api.Controllers
{
    public class TaskController : ApiController
    {
        public GameTaskViewModel Get()
        {
            using (var entities = new InfostyleEntities())
            {
                var entity = entities.Tasks.FirstOrDefault();
                if (entity == null)
                    throw new Exception("No content");

                return new GameTaskViewModel
                {
                    Id = Guid.NewGuid(),
                    Type = TaskType.CrawlLine,
                    Data = new[]
                    {
                        new Phrase {Text = entity.Text, Type = TextType.Normal},
                    }
                };
            }
        }
    }
}