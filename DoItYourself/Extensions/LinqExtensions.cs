using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>
	/// 自作のLINQです。
	/// </summary>
	public static class LinqExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> items, int size)
		{
			var list = new List<T>(size);
			foreach (var item in items)
			{
				list.Add(item);
				if (list.Count == size)
				{
					yield return list;
					list = new List<T>(size);
				}
			}
			if (list.Count != 0)
			{
				yield return list;
			}
		}

		public static IEnumerable<IEnumerable<T>> Chunk2<T>(this IEnumerable<T> items, int size)
		{
			while (items.Any())
			{
				yield return items.Take(size);
				items = items.Skip(size);
			}
		}
	}
}
