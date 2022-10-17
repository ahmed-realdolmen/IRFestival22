using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IRFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PicturesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string[] GetAllPictureUrls()
        {
            return Array.Empty<string>();
        }

        [HttpPost]
        public async void PostPicture(IFormFile file)
        {
            await using var client = new ServiceBusClient(_configuration.GetConnectionString("ServiceBusSenderConnection"));
            var sender = client.CreateSender(_configuration.GetValue<string>("QueueNameMails"));
            var message = new ServiceBusMessage(@"{""hi"": 1}");

            await sender.SendMessageAsync(message);
        }
    }
}
