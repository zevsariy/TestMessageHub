using AutoMapper;
using System;
using TestMessageHub.Models;

namespace TestMessageHub.Converters
{
    public class MessageConverter
    {
        private readonly IMapper _mapper;

        private const string CompanyErrorMessage = "Unknown company name: {0}";
        public MessageConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

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

        public AdidasMessage ConvertToAdidasMessage(DBMessageEntity message)
        {
            return _mapper.Map<AdidasMessage>(message);
        }

        public NikeMessage ConvertToNikeMessage(DBMessageEntity message)
        {
            return _mapper.Map<NikeMessage>(message);
        }

        public PumaMessage ConvertToPumaMessage(DBMessageEntity message)
        {
            return _mapper.Map<PumaMessage>(message);
        }
    }
}
