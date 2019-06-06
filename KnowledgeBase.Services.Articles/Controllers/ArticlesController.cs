using KnowledgeBase.Services.Articles.Handlers;
using KnowledgeBase.Services.Articles.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBase.Services.Articles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly GetArticleHandler _getArticleHandler;

        public ArticlesController(GetArticleHandler getArticleHandler)
        {
            _getArticleHandler = getArticleHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
           => Ok();

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] GetArticle query)
            => Ok(await _getArticleHandler.HandleAsync(query));
    }
}
