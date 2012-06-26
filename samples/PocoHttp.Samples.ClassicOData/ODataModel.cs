using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Samples.ClassicOData
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
