using System;
using System.Linq;
using Api.Helpers;
using Api.Models;
using EF;
using EF.Enums;

namespace Api.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly TaskMapper taskMapper;

        public TaskController()
        {
            taskMapper = new TaskMapper();
        }

        public TaskViewModel Get(TaskType type, int level)
        {
            using (var context = new InfostyleEntities())
            {
                var task = context.Tasks.Where(t => t.Type == type && t.Level == level)
                        .OrderBy(_ => Guid.NewGuid())
                        .FirstOrDefault();
                if (task == null)
                    throw new Exception("No content");

                return taskMapper.Parse(task);
            }
        }
    }
}