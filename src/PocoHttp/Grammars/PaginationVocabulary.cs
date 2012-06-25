using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Grammars
{
	public class PaginationVocabulary
	{

		public PaginationVocabulary()
		{
			Skip = "skip";
			Count = "count";
			OrderBy = "order";
			OrderByDescending = "orderDesc";
		}

		public string Skip { get; set; }

		public string Count { get; set; }

		public string OrderBy { get; set; }

		public string OrderByDescending { get; set; }

	}
}
