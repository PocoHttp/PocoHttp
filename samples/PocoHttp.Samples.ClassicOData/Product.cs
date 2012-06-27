using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwinds
{
	class Product
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
		public DateTime? DiscontinuedDate { get; set; }
		public int Rating { get; set; }
		public float Price { get; set; }

	}
}
