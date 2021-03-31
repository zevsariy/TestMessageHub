using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;

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
        public DBMessageEntity ConvertToDBMessageEntity(MessageBase message)
        {
            return message?.From?.ToUpper() switch
            {
                Companies.Adidas => _mapper.Map<DBMessageEntity>(message as AdidasMessage),
                Companies.Nike => _mapper.Map<DBMessageEntity>(message as NikeMessage),
                Companies.Puma => _mapper.Map<DBMessageEntity>(message as PumaMessage),
                _ => throw new Exception(string.Format(CompanyErrorMessage, message.From))
            };
        }

        /// <summary>
        /// Convert DBMessageEntity to AdidasMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Adidas message</returns>
        public AdidasMessage ConvertToAdidasMessage(DBMessageEntity message)
        {
            return _mapper.Map<AdidasMessage>(message);
        }

        /// <summary>
        /// Convert DBMessageEntity to NikeMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Nike message</returns>
        public NikeMessage ConvertToNikeMessage(DBMessageEntity message)
        {
            return _mapper.Map<NikeMessage>(message);
        }

        /// <summary>
        /// Convert DBMessageEntity to PumaMessage
        /// </summary>
        /// <param name="message">DBMessageEntity</param>
        /// <returns>Puma message</returns>
        public PumaMessage ConvertToPumaMessage(DBMessageEntity message)
        {
            return _mapper.Map<PumaMessage>(message);
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
