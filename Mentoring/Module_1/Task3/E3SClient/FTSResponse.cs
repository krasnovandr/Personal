using System.Collections.Generic;

namespace Task3.E3SClient
{
	public class Item<T>
	{
		public T data { get; set; }

	}

	public class FTSResponse<T> where T : class
	{
		public int total { get; set; }

		public List<Item<T>> items { get; set; }
	}
}
