using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwinds
{
	class Customer
	{
		public string CustomerID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }

		public override string ToString()
		{
			return string.Format("{0} - {1} ({2})", CompanyName, City, Phone);
		}
	}
}
