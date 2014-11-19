using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoItYourself.Tests.Extensions
{
	[TestClass]
	public class LinqExtensionsTest
	{
		[TestMethod]
		public void Chunk()
		{
			MeasureTime(() =>
			{
				var total = 0;
				var items = GetRange(1000).Chunk(9);
				foreach (var item in items)
					foreach (var i in item)
					{
						total += i;
					}
				Console.WriteLine(total);
			});
		}

		private void MeasureTime(Action action)
		{
			var watch = new Stopwatch();
			watch.Start();
			action();
			watch.Stop();
			Console.WriteLine("result: {0}ms", watch.ElapsedMilliseconds);
		}

		private IEnumerable<int> GetRange(int count)
		{
			for (var i = 0; i < count; i++)
			{
				Console.WriteLine("get " + i);
				yield return i;
			}
		}
	}
}
