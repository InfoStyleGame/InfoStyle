﻿using System;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class TaskController : ApiController
    {
        public GameTask Get()
        {
            return new GameTask
            {
                Id = Guid.NewGuid(),
                Type = TaskType.CrawlLine,
                Text = new[]
                {
                    new Phrase {Text = "Все в порядке. ", Type = TextType.Normal},
                    new Phrase {Text = "СТОП!", Type = TextType.Stop},
                    new Phrase {Text = " И снова все в порядке.", Type = TextType.Normal}
                }
            };
        }
    }
}