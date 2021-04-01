using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using TestMessageHub.Models.Const;
using TestMessageHub.Models.DTO;

namespace TestMessageHub.Converters
{
	public class JSONMessageConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(
			JsonWriter writer, 
			object value, 
			JsonSerializer serializer)
		{
		}

		public override object ReadJson(
			JsonReader reader, 
			Type objectType, 
			object existingValue, 
			JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var companyFromName = obj.GetValue("from", StringComparison.OrdinalIgnoreCase)?
				.Value<string>()?.ToUpper() ?? "undefined";

			if(companyFromName == Companies.Adidas)
            {
				var target = new AdidasMessageDTO();
				serializer.Populate(obj.CreateReader(), target);
				return target;
			}

			if (companyFromName == Companies.Nike)
			{
				var target = new NikeMessageDTO();
				serializer.Populate(obj.CreateReader(), target);
				return target;
			}

			if (companyFromName == Companies.Puma)
			{
				var target = new PumaMessageDTO();
				serializer.Populate(obj.CreateReader(), target);
				return target;
			}

			throw new JsonReaderException(
				string.Format(ErrorMessages.MissingMessageType, companyFromName));
		}


		public override bool CanConvert(Type objectType)
		{
			return typeof(MessageBaseDTO) == objectType;
		}
	}
}