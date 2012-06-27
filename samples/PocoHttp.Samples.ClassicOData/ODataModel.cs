using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Samples.ClassicOData.V2
{
	internal class Payload<T>
	{
		public ODataResult<T> d { get; set; }
	}

	internal class ODataResult<T>
	{
		public IEnumerable<T> results { get; set; }
	}



}

namespace PocoHttp.Samples.ClassicOData.V1
{
	internal class Payload<T>
	{
		public IEnumerable<T> d { get; set; }
	}

}
