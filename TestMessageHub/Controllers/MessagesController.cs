using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestMessageHub.Converters;
using TestMessageHub.Interfaces;
using TestMessageHub.Models.DTO;
using TestMessageHub.Models.Const;
using static System.Net.Mime.MediaTypeNames;
using TestMessageHub.Models;

namespace TestMessageHub.Controllers
{
    [ApiController]
    [Produces(Application.Json)]
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

        /// <summary>
        /// Get messages for company and date range with messages status check
        /// </summary>
        /// <param name="companyName">Company name</param>
        /// <param name="dateRange">Date range</param>
        /// <param name="read">Message read status</param>
        /// <response code="200">Request processed successfully</response>
		/// <response code="400">Bad request received. Additional info will be in a body</response>
        /// <returns>List of messages, converted for company messages format</returns>
        [HttpGet]
        public async Task<ActionResult> GetMessagesForCompanyByNameAndDateTimeRange(
            [FromQuery] string companyName,
            [FromQuery] DateRange dateRange,
            [FromQuery] bool? read)
        {
            var messages = await _DBMessagesService.GetMessages(companyName, dateRange, read);
            companyName = companyName.ToUpper();

            if (companyName == Companies.Adidas) 
                return Ok(_messageConverter.PrepareCompanyMessages<AdidasMessageDTO>(messages));

            if (companyName == Companies.Nike)
                return Ok(_messageConverter.PrepareCompanyMessages<NikeMessageDTO>(messages));

            if (companyName == Companies.Puma)
                return Ok(_messageConverter.PrepareCompanyMessages<PumaMessageDTO>(messages));

            return BadRequest(string.Format(ErrorMessages.CantResolveCompanyName, companyName));
        }

        /// <summary>
        /// Send message and save it to DB
        /// </summary>
        /// <param name="message">Message from company. Type of message will be detected by JSONConverter.</param>
        /// <response code="200">Request processed successfully</response>
		/// <response code="400">Bad request received. Additional info will be in a body</response>
        /// <returns>Ok result</returns>
        [HttpPost]
        public async Task<ActionResult> SendMessage(
            [FromBody] MessageBaseDTO message)
        {
            await _DBMessagesService.SaveMessage(_messageConverter.ConvertToDBMessageEntity(message));
            return Ok();
        }
    }
}
