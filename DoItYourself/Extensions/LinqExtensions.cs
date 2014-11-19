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
		/// <param name="source">シーケンス</param>
		/// <param name="size">分割サイズ</param>
		/// <returns>分割された子シーケンスのシーケンス</returns>
		public static IEnumerable<IList<T>> Chunk<T>(this IEnumerable<T> source, int size)
		{
			var list = new List<T>(size);
			foreach (var item in source)
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

		/// <summary>
		/// シーケンスの要素に重複がないかどうかを判断します。
		/// </summary>
		/// <typeparam name="TSource">要素の型</typeparam>
		/// <param name="source">シーケンス</param>
		/// <returns>重複がなければtrue、重複していればfalse。</returns>
		public static bool AreUnique<TSource>(this IEnumerable<TSource> source)
		{
			var set = new HashSet<TSource>();
			return AreUnique(source, set, x => x);
		}

		/// <summary>
		/// シーケンスの要素に重複がないかどうかを判断します。
		/// 重複の判断は主キーによって行います。
		/// </summary>
		/// <typeparam name="TSource">要素の型</typeparam>
		/// <typeparam name="TSource1">主キーの型</typeparam>
		/// <param name="source">シーケンス</param>
		/// <param name="keySelector">主キーのセレクター</param>
		/// <returns>重複がなければtrue、重複していればfalse。</returns>
		public static bool AreUnique<TSource, TSource1>(this IEnumerable<TSource> source, Func<TSource, TSource1> keySelector)
		{
			var set = new HashSet<TSource1>();
			return AreUnique(source, set, keySelector);
		}

		/// <summary>
		/// シーケンスの要素に重複がないかどうかを判断します。
		/// 重複の判断は主キーによって行います。
		/// </summary>
		/// <typeparam name="TSource">要素の型</typeparam>
		/// <typeparam name="TSource1">複合主キー1の型</typeparam>
		/// <typeparam name="TSource2">複合主キー2の型</typeparam>
		/// <param name="source">シーケンス</param>
		/// <param name="keySelector1">複合主キー1のセレクター</param>
		/// <param name="keySelector2">複合主キー2のセレクター</param>
		/// <returns>重複がなければtrue、重複していればfalse。</returns>
		public static bool AreUnique<TSource, TSource1, TSource2>(this IEnumerable<TSource> source, Func<TSource, TSource1> keySelector1, Func<TSource, TSource2> keySelector2)
		{
			var set = new HashSet<Tuple<TSource1, TSource2>>(new AreUniqueComparer<TSource1, TSource2>());
			return AreUnique(source, set, x => Tuple.Create(keySelector1(x), keySelector2(x)));
		}

		private class AreUniqueComparer<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
		{
			public bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
			{
				return object.Equals(x.Item1, y.Item1) && object.Equals(x.Item2, y.Item2);
			}

			int IEqualityComparer<Tuple<T1, T2>>.GetHashCode(Tuple<T1, T2> x)
			{
				return x.GetHashCode();
			}
		}

		private static bool AreUnique<TSource, TElement>(IEnumerable<TSource> source, HashSet<TElement> set, Func<TSource, TElement> selector)
		{
			foreach (var item in source)
			{
				var element = selector(item);
				if (set.Contains(element))
				{
					return false;
				}
				set.Add(element);
			}
			return true;
		}
	}
}
