using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarManager.Data;
using PocoHttp;

namespace CarManager.PocoHttp.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var pocoClient = new PocoClient()
			                 	{
			                 		BaseAddress = new Uri("http://localhost:12889/api/")
			                 	};
			var list = pocoClient.Context<Car>()
				.Take(1).ToList();

			Console.WriteLine(list[0]);

			Console.Read();
		}
	}
}
