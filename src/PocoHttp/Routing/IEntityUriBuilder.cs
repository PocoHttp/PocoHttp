using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Routing
{
	public interface IEntityUriBuilder
	{
		Uri BuildUri(Type type, bool usePluralUris);
	}
}
