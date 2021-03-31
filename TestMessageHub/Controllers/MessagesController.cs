using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestMessageHub.Converters;
using TestMessageHub.Interfaces;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;

namespace TestMessageHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessageConverter _messageConverter;

        private readonly IDBMessagesService _DBMessagesService;

        public MessagesController(
            IMapper mapper,
            IDBMessagesService DBMessagesService)
        {
            _messageConverter = new MessageConverter(mapper);
            _DBMessagesService = DBMessagesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetMessagesForCompanyByNameAndDateTimeRange(
            [FromQuery] string companyName,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] bool? read)
        {
            var messages = await _DBMessagesService.GetMessages(companyName, fromDate, toDate, read);

            return companyName.ToUpper() switch
            {
                Companies.Adidas => Ok(_messageConverter.PrepareCompanyMessages<AdidasMessage>(messages)),
                Companies.Nike => Ok(_messageConverter.PrepareCompanyMessages<NikeMessage>(messages)),
                Companies.Puma => Ok(_messageConverter.PrepareCompanyMessages<PumaMessage>(messages)),
                _ => BadRequest(string.Format(ErrorMessages.CantResolveCompanyName, companyName)),
            };
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(
            [FromBody] MessageBase message)
        {
            await _DBMessagesService.SaveMessage(_messageConverter.ConvertToDBMessageEntity(message));
            return Ok();
        }
    }
}
