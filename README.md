PocoHttp
========

PocoHttp (where POCO stands for Plain-Old-CSharp-Object) is a .NET Client Library for accessing HTTP data services using a familiar IQueryable<T> interface, similar to ORMs such as Entity Framework. This allows the client to work with the IQueryable<T> interface similar to other data source and being abstracted from HTTP calls.

PocoHttp support different querying Grammars but by default uses an implementation of OData querying Grammar specified by OData spec. Not all OData querying features are suported. The intention is not to replicate the OData client features already available but to expose those features that are supported by ASP.NET Web API.

PocoHttp is dependent on System.Net.Http available as NuGet packages.