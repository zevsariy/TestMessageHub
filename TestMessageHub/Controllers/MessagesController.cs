using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestMessageHub.Converters;
using TestMessageHub.Models;

namespace TestMessageHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        private readonly MessageConverter _messageConverter;

        public MessagesController(
            ILogger<MessagesController> logger,
            IMapper mapper)
        {
            _logger = logger;
            _messageConverter = new MessageConverter(mapper);
        }

        [HttpGet]
        public ActionResult GetMessagesForCompanyByNameAndDateTimeRange(
            [FromQuery] string companyName,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            Companies.Validate(companyName);

            using ApplicationContext db = new ApplicationContext();
            var messages = db.Messages.Where(
                (message) => message.To == companyName
                && (message.SendDate >= fromDate || message.SendDate <= toDate)
            );
            return Ok(messages);
        }

        [HttpPost]
        public ActionResult SendMessage(
            [FromBody] MessageBase message)
        {
            using ApplicationContext db = new ApplicationContext();
            db.Messages.Add(
                _messageConverter.ConvertToDBMessageEntity(message)
            );
            db.SaveChanges();
            return Ok();
        }
    }
}
