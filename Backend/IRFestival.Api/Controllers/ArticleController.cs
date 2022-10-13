using IRFestival.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Net;

namespace IRFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _websiteArticlesContainer;

        public ArticleController(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration.GetConnectionString("CosmosConnection"));
            _websiteArticlesContainer = _cosmosClient.GetContainer("IRFestivalArticles", "WebsiteArticles");
        }

        [HttpPost]
        public async Task<ActionResult> CreateItemAsync()
        {
            await _websiteArticlesContainer.CreateItemAsync(new Article
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Second article from cosmos",
                Date = DateTime.Now,
                Message = "Hello dummy article",
                Status = "Unpublished",
                Tag = "important"
            });
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK, Type=typeof(Article))]
        public async Task<IActionResult> GetAsync()
        {
            var result = new List<Article>();
            var queryDefinition = _websiteArticlesContainer.GetItemLinqQueryable<Article>()
                .Where(p => p.Status == "Published")
                .OrderBy(p => p.Date);

            var iterator = queryDefinition.ToFeedIterator();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                result = response.ToList();
            }

            return Ok(result);
        }
    }
}
