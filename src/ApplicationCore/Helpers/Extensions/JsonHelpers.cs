using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationCore.Helpers
{
	public static class JsonHelpers
    {
		public static T DeepCloneByJson<T>(this T source)
		{
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}
			var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
		}
	}
}
