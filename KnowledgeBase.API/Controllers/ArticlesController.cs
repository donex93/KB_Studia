using KnowledgeBase.API.Messages.Commands.Articles;
using KnowledgeBase.API.Queries;
using KnowledgeBase.API.Services;
using KnowledgeBase.Common.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KnowledgeBase.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _articlesService;
        private readonly IBusPublisher _busPublisher;

        public ArticlesController(IArticlesService articlesService, IBusPublisher busPublisher)
        {
            _articlesService = articlesService;
            _busPublisher = busPublisher;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseArticles query)
            => Ok(await _articlesService.BrowseAsync(query));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _articlesService.GetAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateArticle command)
        {
            await _busPublisher.SendAsync(command);
            return Accepted();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateArticle command)
        {
            command.SetId(id);
            await _busPublisher.SendAsync(command);

            return Accepted();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _busPublisher.SendAsync(new DeleteArticle(id));

            return Accepted();
        }
    }
}
