using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarManager.Data
{
	public class Car
	{
		public int Id { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public int BuildYear { get; set; }
		public double Price { get; set; }
		public int MaxSpeed { get; set; }
		public bool WarrantyProvided { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(Make).Append(" ")
				.Append(Model).Append(" ")
				.Append(BuildYear).Append(" with max speed ")
				.Append(MaxSpeed).Append("mph")
				.Append(WarrantyProvided ? " with warranty " : " no warranty ")
				.Append("for $").Append(Price);

			return sb.ToString();
		}
	}

	
}
