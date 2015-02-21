using System;
using System.Linq;
using System.Web.Http;
using Api.Helpers;
using Api.Models;
using EF;
using EF.Enums;

namespace Api.Controllers
{
    public class TaskController : ApiController
    {
        private readonly TaskParser taskParser;

        public TaskController()
        {
            taskParser = new TaskParser();
        }

        public TaskViewModel Get()
        {
            using (var context = new InfostyleEntities())
            {
                var task = context.Tasks.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
                if (task == null)
                    throw new Exception("No content");

                return taskParser.Parse(task);
            }
        }
    }
}