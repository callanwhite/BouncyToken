using System.Web.Script.Serialization;

namespace BouncyToken.Utility
{
	public class JsonSerializer : IJsonSerializer
	{
		JavaScriptSerializer serializer = new JavaScriptSerializer();

		public string Serialize(object @object)
		{
			return serializer.Serialize(@object);
		}

		public T Deserialize<T>(string json)
		{
			return serializer.Deserialize<T>(json);
		}
	}
}
