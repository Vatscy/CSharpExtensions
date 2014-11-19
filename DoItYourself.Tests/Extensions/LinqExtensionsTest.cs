using System;
using System.Collections.Generic;
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
			var time = Utility.Time(() =>
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
			Console.WriteLine("result: {0}ms", time);
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
