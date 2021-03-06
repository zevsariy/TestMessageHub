using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using TestMessageHub.Converters;
using TestMessageHub.Models.Const;
using TestMessageHub.Models.DTO;

namespace TestMessageHub.Tests
{
    [TestClass]
    public class JSONMessageConverterTests
    {
        private JSONMessageConverter _jsonMessageConverter;

        [TestInitialize]
        public void BeforeEach()
        {
            _jsonMessageConverter = new JSONMessageConverter();
        }

        [TestMethod]
        public void CanConvert_AdidasMessage_False()
        {
            // arrange
            Type type = typeof(AdidasMessageDTO);
            // act
            bool canConvert = _jsonMessageConverter.CanConvert(type);
            // assert
            Assert.IsFalse(canConvert);
        }

        [TestMethod]
        public void CanConvert_NikeMessage_False()
        {
            // arrange
            Type type = typeof(NikeMessageDTO);
            // act
            bool canConvert = _jsonMessageConverter.CanConvert(type);
            // assert
            Assert.IsFalse(canConvert);
        }

        [TestMethod]
        public void CanConvert_PumaMessage_False()
        {
            // arrange
            Type type = typeof(PumaMessageDTO);
            // act
            bool canConvert = _jsonMessageConverter.CanConvert(type);
            // assert
            Assert.IsFalse(canConvert);
        }

        [TestMethod]
        public void CanConvert_MessageBase_True()
        {
            // arrange
            Type type = typeof(MessageBaseDTO);
            // act
            bool canConvert = _jsonMessageConverter.CanConvert(type);
            // assert
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void MessageWithAdidasSender_ReturnAdidasMessage()
        {
            // arrange
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(_jsonMessageConverter);

            var adidasMessage = new AdidasMessageDTO
            {
                Header = Guid.NewGuid().ToString(),
                Content = Guid.NewGuid().ToString(),
                From = Companies.Adidas,
                To = Companies.Puma,
                SendDate = DateTime.UtcNow,
                Read = true
            };
            string stringOfAdidasMessage = JsonConvert.SerializeObject(adidasMessage, jsonSettings);

            // act
            var messageBase = JsonConvert.DeserializeObject<MessageBaseDTO>(stringOfAdidasMessage, jsonSettings);

            // assert
            Assert.AreEqual(Companies.Adidas, messageBase.From);

            var deserializedAdidasMessage = (AdidasMessageDTO)messageBase;
            Assert.AreEqual(adidasMessage.From, deserializedAdidasMessage.From);
            Assert.AreEqual(adidasMessage.To, deserializedAdidasMessage.To);
            Assert.AreEqual(adidasMessage.Header, deserializedAdidasMessage.Header);
            Assert.AreEqual(adidasMessage.Content, deserializedAdidasMessage.Content);
            Assert.AreEqual(adidasMessage.SendDate, deserializedAdidasMessage.SendDate);
            Assert.AreEqual(adidasMessage.Read, deserializedAdidasMessage.Read);
        }

        [TestMethod]
        public void MessageWithNikeSender_ReturnNikeMessage()
        {
            // arrange
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(_jsonMessageConverter);

            var nikeMessage = new NikeMessageDTO
            {
                Caption = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString(),
                From = Companies.Nike,
                To = Companies.Puma,
                SendDate = DateTime.UtcNow,
                Read = true
            };
            string stringOfNikeMessage = JsonConvert.SerializeObject(nikeMessage, jsonSettings);

            // act
            var messageBase = JsonConvert.DeserializeObject<MessageBaseDTO>(stringOfNikeMessage, jsonSettings);

            // assert
            Assert.AreEqual(Companies.Nike, messageBase.From);

            var deserializedNikeMessage = (NikeMessageDTO)messageBase;
            Assert.AreEqual(nikeMessage.From, deserializedNikeMessage.From);
            Assert.AreEqual(nikeMessage.To, deserializedNikeMessage.To);
            Assert.AreEqual(nikeMessage.Caption, deserializedNikeMessage.Caption);
            Assert.AreEqual(nikeMessage.Message, deserializedNikeMessage.Message);
            Assert.AreEqual(nikeMessage.SendDate, deserializedNikeMessage.SendDate);
            Assert.AreEqual(nikeMessage.Read, deserializedNikeMessage.Read);
        }

        [TestMethod]
        public void MessageWithPumaSender_ReturnPumaMessage()
        {
            // arrange
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(_jsonMessageConverter);

            var pumaMessage = new PumaMessageDTO
            {
                Title = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString(),
                From = Companies.Puma,
                To = Companies.Nike,
                SendDate = DateTime.UtcNow,
                Read = true
            };
            string stringOfPumaMessage = JsonConvert.SerializeObject(pumaMessage, jsonSettings);

            // act
            var messageBase = JsonConvert.DeserializeObject<MessageBaseDTO>(stringOfPumaMessage, jsonSettings);

            // assert
            Assert.AreEqual(Companies.Puma, messageBase.From);

            var deserializedPumaMessage = (PumaMessageDTO)messageBase;
            Assert.AreEqual(pumaMessage.From, deserializedPumaMessage.From);
            Assert.AreEqual(pumaMessage.To, deserializedPumaMessage.To);
            Assert.AreEqual(pumaMessage.Title, deserializedPumaMessage.Title);
            Assert.AreEqual(pumaMessage.Body, deserializedPumaMessage.Body);
            Assert.AreEqual(pumaMessage.SendDate, deserializedPumaMessage.SendDate);
            Assert.AreEqual(pumaMessage.Read, deserializedPumaMessage.Read);
        }
    }
}
