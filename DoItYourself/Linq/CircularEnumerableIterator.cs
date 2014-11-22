using System;
using System.Linq;
using System.Collections.Generic;

namespace DoItYourself.Linq
{
    internal class CircularEnumerableIterator<TSource> : EnumerableIterator<TSource>
    {
        private IEnumerable<TSource> source;
        private IEnumerator<TSource> enumerator;

        public CircularEnumerableIterator(IEnumerable<TSource> source)
        {
            this.source = source;
        }

        public override EnumerableIterator<TSource> Clone()
        {
            return new CircularEnumerableIterator<TSource>(this.source);
        }

        public override void Dispose()
        {
            if (this.enumerator != null)
            {
                this.enumerator.Dispose();
            }
            this.enumerator = null;
            base.Dispose();
        }

        public override bool MoveNext()
        {
            if (this.State != IteratorState.Used)
            {
                return false;
            }

            if (this.enumerator == null)
            {
                this.enumerator = this.source.GetEnumerator();
            }

            if (!this.enumerator.MoveNext())
            {
                this.enumerator.Dispose();
                this.enumerator = this.source.GetEnumerator();
                if (!this.enumerator.MoveNext())
                {
                    this.enumerator.Dispose();
                    return false;
                }
            }

            this.Current = this.enumerator.Current;
            return true;
        }
    }
}
