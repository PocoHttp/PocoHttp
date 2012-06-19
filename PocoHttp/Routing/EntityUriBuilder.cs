using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Routing
{
	public class EntityUriBuilder : IEntityUriBuilder
	{

		private ConcurrentDictionary<Type,EntityUriAttribute> _entityUriAttributesCache
			= new ConcurrentDictionary<Type, EntityUriAttribute>();

		public Uri BuildUri(Type type, bool usePluralUris)
		{
			var attributeUri = GetAttributeUri(type);
			if(attributeUri!=null)
				return new Uri(GetAttributeUri(type), UriKind.Relative);

			string uri = "/" + type.Name;
			if(usePluralUris)
				uri += "s";
			return new Uri(uri, UriKind.Relative);
			
		}

		private string GetAttributeUri(Type type)
		{
			EntityUriAttribute attribute = null;
			if(!_entityUriAttributesCache.TryGetValue(type, out attribute))
			{
				attribute = type.GetCustomAttributes(true).OfType<EntityUriAttribute>().FirstOrDefault();
				_entityUriAttributesCache.TryAdd(type, attribute);
			}
			return attribute == null ? null : attribute.RelativeUri;
		}

	}
}
