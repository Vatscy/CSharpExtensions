using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DoItYourself.Tests.Linq
{
    [TestFixture]
    public class LinqExtensionsTest
    {
        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        private static readonly List<Test> Source = Enumerable.Range(0, 100000).Select(s => new Test
            {
                Id = s,
                Name = s.ToString("0000000")
            }).ToList();

        private IEnumerable<int> GetRange(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine("get " + i);
                yield return i;
            }
        }

        [Test]
        public void AreUnique()
        {
            Utility.Time(() =>
                {
                    Source.AreUnique(s => s.Id, s => s.Name);
                }).ConsoleWriteLine("result: {0}ms");
        }

        [Test]
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

        [Test]
        public void Cycle_正常系_空リスト_空リストが返る()
        {
            var list = new List<int>();
            var circluarList = list.Cycle();
            Assert.That(circluarList.Any(), Is.False);
        }

        [Test]
        public void Cycle_正常系_空リストでない_繰り返し取得できる()
        {
            var list = new [] { 1, 2, 3, 4, 5 };
            var circluarList = list.Cycle();
            CollectionAssert.AreEqual(new [] { 1 }, circluarList.Take(1));
            CollectionAssert.AreEqual(new [] { 1, 2, 3, 4, 5 }, circluarList.Take(5));
            CollectionAssert.AreEqual(new [] { 1, 2, 3, 4, 5, 1 }, circluarList.Take(6));
            CollectionAssert.AreEqual(new [] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }, circluarList.Take(10));
            CollectionAssert.AreEqual(new [] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1 }, circluarList.Take(11));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Cycle_異常系_nullを指定_NullReferenceExceptionが発生する()
        {
            List<int> list = null;
            list.Cycle();
        }

        [Test]
        public void Fix_長さ0を指定_空シーケンスが返る()
        {
            var list = new[] { 1, 2, 3 };
            Assert.That(list.Fix(0).Any(), Is.False);
        }

        [Test]
        public void Fix_元シーケンスより短い長さを指定_指定した長さのシーケンスが返る()
        {
            var list = new[] { 1, 2, 3 };
            CollectionAssert.AreEqual(list.Fix(2), new[]{ 1, 2 });
        }

        [Test]
        public void Fix_元シーケンスと同じ長さを指定_指定した長さのシーケンスが返る()
        {
            var list = new[] { 1, 2, 3 };
            CollectionAssert.AreEqual(list.Fix(3), new[]{ 1, 2, 3 });
        }

        [Test]
        public void Fix_元シーケンスより長い長さを指定_指定した長さのシーケンスが返る()
        {
            var list = new[] { 1, 2, 3 };
            CollectionAssert.AreEqual(list.Fix(4), new[]{ 1, 2, 3, 0 });

            var list2 = new[] { "a", "b", "c" };
            CollectionAssert.AreEqual(list2.Fix(5), new [] { "a", "b", "c", null, null });
        }

        [Test]
        public void tttttttttt()
        {
            var list = Enumerable.Range(0, 1000);
            var list2 = Enumerable.Range(0, 1000).ToArray();
            Utility.Time(() =>
            {
                foreach (var i in Partition(list, 3)) {
                    foreach (var j in i) {

                    }
                }
            }).ConsoleWriteLine("1: {0}");
            Utility.Time(() =>
                {
                    foreach (var i in Partition(list2, 3)) {
                        foreach (var j in i) {

                        }
                    }
                }).ConsoleWriteLine("2: {0}");
        }

        public IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> source, int size)
        {
            while (source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
            }
        }
    }
}
