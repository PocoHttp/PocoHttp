using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Routing
{
	/// <summary>
	/// Represents relative URI of the entity and used to 
	/// mark an entity server relative URI to something other than name of the entity.
	/// For example, class "Car" normally would be accessed at [BaseAddress]/Car(s)
	/// This overrides it
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class EntityUriAttribute : Attribute
	{
		private readonly string _relativeUri;

		public EntityUriAttribute(string relativeUri)
		{
			_relativeUri = relativeUri;
		}

		public string RelativeUri
		{
			get { return _relativeUri; }
		}
	}
}
