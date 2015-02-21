using System;
using System.Linq;
using System.Web.Http;
using Api.Helpers;
using Api.Models;
using EF;

namespace Api.Controllers
{
    public class TaskController : ApiController
    {
        private readonly TaskMapper taskMapper;

        public TaskController()
        {
            taskMapper = new TaskMapper();
        }

        public TaskViewModel Get()
        {
            using (var context = new InfostyleEntities())
            {
                var task = context.Tasks.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
                if (task == null)
                    throw new Exception("No content");

                return taskMapper.Parse(task);
            }
        }
    }
}