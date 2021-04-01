using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMessageHub.Converters;
using TestMessageHub.Mappings;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;
using TestMessageHub.Models.DTO;

namespace TestMessageHub.Tests
{
    [TestClass]
    public class MessageConverterTests
    {
        private MessageConverter _messageConverter;

        [TestInitialize]
        public void BeforeEach()
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = false;
                mc.AddProfiles(new List<Profile>
                {
                    new MessageMappingProfile()
                });
            }).CreateMapper();
            _messageConverter = new MessageConverter(mapper);
        }

        [TestMethod]
        public void FromAdidasMessageToDBMessageEntity()
        {
            var message = new AdidasMessageDTO()
            {
                From = Companies.Adidas,
                Header = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString()
            };

            var DBMessageEntity = _messageConverter.ConvertToDBMessageEntity(message);

            Assert.IsNotNull(DBMessageEntity.Title);
            Assert.IsNotNull(DBMessageEntity.Message);
            Assert.AreEqual(message.Header, DBMessageEntity.Title);
            Assert.AreEqual(message.Content, DBMessageEntity.Message);
        }

        [TestMethod]
        public void FromNikeMessageToDBMessageEntity()
        {
            var message = new NikeMessageDTO()
            {
                From = Companies.Nike,
                Caption = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString()
            };

            var DBMessageEntity = _messageConverter.ConvertToDBMessageEntity(message);

            Assert.IsNotNull(DBMessageEntity.Title);
            Assert.IsNotNull(DBMessageEntity.Message);
            Assert.AreEqual(message.Caption, DBMessageEntity.Title);
            Assert.AreEqual(message.Message, DBMessageEntity.Message);
        }

        [TestMethod]
        public void FromPumaMessageToDBMessageEntity()
        {
            var message = new PumaMessageDTO()
            {
                From = Companies.Puma,
                Title = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            var pumaMessage = _messageConverter.ConvertToDBMessageEntity(message);

            Assert.IsNotNull(pumaMessage.Title);
            Assert.IsNotNull(pumaMessage.Message);
            Assert.AreEqual(message.Title, pumaMessage.Title);
            Assert.AreEqual(message.Body, pumaMessage.Message);
        }

        [TestMethod]
        public void FromDBMessageEntityToAdidasMessage()
        {
            var message = new DBMessageEntity()
            {
                From = Companies.Adidas,
                Title  = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString()
            };

            var adidasMessage = _messageConverter.ConvertToAdidasMessage(message);

            Assert.IsNotNull(adidasMessage.Header);
            Assert.IsNotNull(adidasMessage.Content);
            Assert.AreEqual(message.Title, adidasMessage.Header);
            Assert.AreEqual(message.Message, adidasMessage.Content);
        }

        [TestMethod]
        public void FromDBMessageEntityToNikeMessage()
        {
            var message = new DBMessageEntity()
            {
                From = Companies.Adidas,
                Title = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString()
            };

            var nikeMessage = _messageConverter.ConvertToNikeMessage(message);

            Assert.IsNotNull(nikeMessage.Caption);
            Assert.IsNotNull(nikeMessage.Message);
            Assert.AreEqual(message.Title, nikeMessage.Caption);
            Assert.AreEqual(message.Message, nikeMessage.Message);
        }

        [TestMethod]
        public void FromDBMessageEntityToPumaMessage()
        {
            var message = new DBMessageEntity()
            {
                From = Companies.Adidas,
                Title = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString()
            };

            var pumaMessage = _messageConverter.ConvertToPumaMessage(message);

            Assert.IsNotNull(pumaMessage.Title);
            Assert.IsNotNull(pumaMessage.Body);
            Assert.AreEqual(message.Title, pumaMessage.Title);
            Assert.AreEqual(message.Message, pumaMessage.Body);
        }

        [TestMethod]
        public void PrepareCompanyMessageToAdidasMessage()
        {
            var messages = new List<DBMessageEntity>
            {
                new DBMessageEntity()
                {
                    From = Companies.Nike,
                    To = Companies.Adidas,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                }
            };

            var adidasMessage = _messageConverter.PrepareCompanyMessages<AdidasMessageDTO>(messages)
                .FirstOrDefault();

            var inputMessage = messages.Single();

            Assert.IsNotNull(adidasMessage.Header);
            Assert.IsNotNull(adidasMessage.Content);
            Assert.AreEqual(inputMessage.Title, adidasMessage.Header);
            Assert.AreEqual(inputMessage.Message, adidasMessage.Content);
        }

        [TestMethod]
        public void PrepareCompanyMessageToNikeMessage()
        {
            var messages = new List<DBMessageEntity>
            {
                new DBMessageEntity()
                {
                    From = Companies.Puma,
                    To = Companies.Nike,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                }
            };

            var nikeMessage = _messageConverter.PrepareCompanyMessages<NikeMessageDTO>(messages)
                .FirstOrDefault();

            var inputMessage = messages.Single();

            Assert.IsNotNull(nikeMessage.Caption);
            Assert.IsNotNull(nikeMessage.Message);
            Assert.AreEqual(inputMessage.Title, nikeMessage.Caption);
            Assert.AreEqual(inputMessage.Message, nikeMessage.Message);
        }

        [TestMethod]
        public void PrepareCompanyMessageToPumaMessage()
        {
            var messages = new List<DBMessageEntity>
            {
                new DBMessageEntity()
                {
                    From = Companies.Nike,
                    To = Companies.Puma,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                }
            };

            var pumaMessage = _messageConverter.PrepareCompanyMessages<PumaMessageDTO>(messages)
                .FirstOrDefault();

            var inputMessage = messages.Single();

            Assert.IsNotNull(pumaMessage.Title);
            Assert.IsNotNull(pumaMessage.Body);
            Assert.AreEqual(inputMessage.Title, pumaMessage.Title);
            Assert.AreEqual(inputMessage.Message, pumaMessage.Body);
        }

        [TestMethod]
        public void PrepareCompanyMessagesToPumaMessages()
        {
            var messages = new List<DBMessageEntity>
            {
                new DBMessageEntity()
                {
                    From = Companies.Nike,
                    To = Companies.Puma,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                },
                new DBMessageEntity()
                {
                    From = Companies.Nike,
                    To = Companies.Puma,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                },
                new DBMessageEntity()
                {
                    From = Companies.Nike,
                    To = Companies.Puma,
                    Title = Guid.NewGuid().ToString(),
                    Message = Guid.NewGuid().ToString()
                },
            };

            var pumaMessages = _messageConverter.PrepareCompanyMessages<PumaMessageDTO>(messages);

            Assert.AreEqual(3, pumaMessages.Count);
            Assert.IsFalse(pumaMessages.Any(message => string.IsNullOrEmpty(message.Title)));
            Assert.IsFalse(pumaMessages.Any(message => string.IsNullOrEmpty(message.Body)));
            Assert.IsFalse(pumaMessages.Any(message => string.IsNullOrEmpty(message.From)));
            Assert.IsFalse(pumaMessages.Any(message => string.IsNullOrEmpty(message.To)));
        }
    }
}
