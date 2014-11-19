using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>
	/// 自作のLINQです。
	/// </summary>
	public static class LinqExtensions
	{
		/// <summary>
		/// シーケンスを指定したサイズの子シーケンスに分割します。
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="items">シーケンス</param>
		/// <param name="size">分割サイズ</param>
		/// <returns>分割された子シーケンスのシーケンス</returns>
		public static IEnumerable<IList<T>> Chunk<T>(this IEnumerable<T> items, int size)
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
	}
}
