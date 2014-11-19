using System;
using System.Collections.Generic;
using System.Linq;
using DoItYourself.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoItYourself.Tests.Extensions
{
	[TestClass]
	public class LinqExtensionsTest
	{
		private class Test
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private static readonly List<Test> Source = Enumerable.Range(0, 10000).Select(s => new Test { Id = s, Name = s.ToString("00000") }).ToList();

		[TestMethod]
		public void AreUnique()
		{
			Utility.Time(() =>
			{
				Source.AreUnique(s => s.Id, s => s.Name);
			}).ConsoleWriteLine("result: {0}ms");
		}

		[TestMethod]
		public void Chunk()
		{
			Utility.Time(() =>
			{
				var total = 0;
				var items = GetRange(1000).Chunk(9);
				foreach (var item in items)
					foreach (var i in item)
					{
						total += i;
					}
				Console.WriteLine(total);
			}).ConsoleWriteLine("result: {0}ms");
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
