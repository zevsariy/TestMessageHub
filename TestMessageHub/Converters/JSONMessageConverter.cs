using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;

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

            switch (companyFromName)
			{
				case Companies.Adidas:
					{
						var target = new AdidasMessage();
						serializer.Populate(obj.CreateReader(), target);
						return target;
					}
				case Companies.Nike:
					{
						var target = new NikeMessage();
						serializer.Populate(obj.CreateReader(), target);
						return target;
					}
				case Companies.Puma:
					{
						var target = new PumaMessage();
						serializer.Populate(obj.CreateReader(), target);
						return target;
					}
				default:
					throw new JsonReaderException(
						string.Format(ErrorMessages.MissingMessageType, companyFromName));
			}
		}


		public override bool CanConvert(Type objectType)
		{
			return typeof(MessageBase) == objectType;
		}
	}
}