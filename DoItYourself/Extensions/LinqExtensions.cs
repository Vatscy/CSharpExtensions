using System.Collections;
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

		public static IEnumerable<IEnumerable<T>> Chunk3<T>(this IEnumerable<T> items, int size)
		{
			return new ChunkEnumerable<T>(items, size);
		}

		public class ChunkEnumerable<T> : IEnumerable<IEnumerable<T>>
		{
			private IEnumerable<T> enumerable;
			private int chunkSize;

			public ChunkEnumerable(IEnumerable<T> enumerable, int chunkSize)
			{
				this.enumerable = enumerable;
				this.chunkSize = chunkSize;
			}

			public IEnumerator<IEnumerable<T>> GetEnumerator()
			{
				return new ChunkEnumerator(this.enumerable.GetEnumerator(), this.chunkSize);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public struct ChunkEnumerator : IEnumerator<IEnumerable<T>>
			{
				private IEnumerator<T> enumerator;
				private int chunkSize;

				public ChunkEnumerator(IEnumerator<T> enumerator, int chunkSize)
				{
					this.enumerator = enumerator;
					this.chunkSize = chunkSize;
				}

				public IEnumerable<T> Current
				{
					get
					{
						yield return this.enumerator.Current;
						for (var i = 1; i < chunkSize; i++)
						{
							if (this.enumerator.MoveNext())
							{
								yield return this.enumerator.Current;
							}
							else
							{
								break;
							}
						}
					}
				}

				public bool MoveNext()
				{
					return this.enumerator.MoveNext();
				}

				public void Dispose()
				{
				}

				public void Reset()
				{
					this.enumerator.Reset();
				}

				object IEnumerator.Current
				{
					get
					{
						return Current;
					}
				}
			}
		}
	}
}
