using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task3.E3SClient
{
	[JsonObject]
	public class Statement
	{
		[JsonProperty("query")]
		public string Query { get; set; }
	}

	[JsonObject]
	public class Filter
	{
		[JsonProperty("field")]
		public string Field { get; set; }

		[JsonProperty("values")]
		public List<string> Values { get; set; }
	}

	public enum SortOrder
	{
		ASCending = 1,
		DESCending = -1
	}

	[JsonDictionary]
	public class SortingCollection : Dictionary<string, SortOrder> { }

	[JsonObject]
	public class FTSQueryRequest
	{
		public FTSQueryRequest()
		{
			//Statements = new List<Statement>();
			//Filters = new List<Filter>();
			//Sorting = new SortingCollection();
		}

		[JsonProperty("statements")]
		public List<Statement> Statements { get; set; }

		[JsonProperty("filters")]
		public List<Filter> Filters { get; set; }

		[JsonProperty("sorting")]
		public SortingCollection Sorting { get; set; }

		[JsonProperty("start")]
		public int Start { get; set; }

		[JsonProperty("limit")]
		public int Limit { get; set; }
	}
}

