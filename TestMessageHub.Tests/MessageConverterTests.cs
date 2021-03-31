using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TestMessageHub.Converters;
using TestMessageHub.Mappings;
using TestMessageHub.Models;

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
            var message = new AdidasMessage()
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
            var message = new NikeMessage()
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
            var message = new PumaMessage()
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
    }
}
