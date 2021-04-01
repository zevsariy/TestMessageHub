using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;
using TestMessageHub.Models.DTO;

namespace TestMessageHub.Converters
{
    /// <summary>
    /// Message converter for different companies
    /// </summary>
    public class MessageConverter
    {
        private readonly IMapper _mapper;

        private const string CompanyErrorMessage = "Unknown company name: {0}";
        public MessageConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Convert any messages to DBMessageEntity
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>DBMessageEntity</returns>
        public DBMessageEntity ConvertToDBMessageEntity(MessageBaseDTO message)
        {
            var companyName = message?.From?.ToUpper();

            if (companyName == Companies.Adidas)
                return _mapper.Map<DBMessageEntity>(message as AdidasMessageDTO);

            if (companyName == Companies.Nike)
                return _mapper.Map<DBMessageEntity>(message as NikeMessageDTO);

            if (companyName == Companies.Puma)
                return _mapper.Map<DBMessageEntity>(message as PumaMessageDTO);

            throw new Exception(string.Format(CompanyErrorMessage, message.From));
        }

        /// <summary>
        /// Convert DBMessageEntity to AdidasMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Adidas message</returns>
        public AdidasMessageDTO ConvertToAdidasMessage(DBMessageEntity message)
        {
            return _mapper.Map<AdidasMessageDTO>(message);
        }

        /// <summary>
        /// Convert DBMessageEntity to NikeMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Nike message</returns>
        public NikeMessageDTO ConvertToNikeMessage(DBMessageEntity message)
        {
            return _mapper.Map<NikeMessageDTO>(message);
        }

        /// <summary>
        /// Convert DBMessageEntity to PumaMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Puma message</returns>
        public PumaMessageDTO ConvertToPumaMessage(DBMessageEntity message)
        {
            return _mapper.Map<PumaMessageDTO>(message);
        }

        /// <summary>
        /// Convert DBMessageEntity to another message type for company
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>T message</returns>
        public T ConvertToMessageBase<T>(DBMessageEntity message)
        {
            return _mapper.Map<T>(message);
        }

        /// <summary>
        /// Map messages from list of DBMessageEntity to list of T
        /// </summary>
        /// <typeparam name="T">Type of message for company</typeparam>
        /// <param name="messages">List of DBMessageEntity</param>
        /// <returns>List of T messages</returns>
        public List<T> PrepareCompanyMessages<T>(List<DBMessageEntity> messages)
        {
            var companyMessages = messages.Select(
                (message) => _mapper.Map<T>(message)
            ).ToList();
            return companyMessages;
        }
    }
}
