using System;
using System.Collections.Generic;
using System.Collections;

namespace DoItYourself.Linq
{
    internal abstract class EnumerableIterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IEnumerator, IDisposable
    {
        public TSource Current { get; protected set; }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        protected IteratorState State { get; private set; }

        public abstract EnumerableIterator<TSource> Clone();

        public virtual void Dispose()
        {
            this.Current = default(TSource);
            this.State = IteratorState.Disposed;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            if (this.State == IteratorState.Ready)
            {
                this.State = IteratorState.Used;
                return this;
            }
            var iterator = this.Clone();
            iterator.State = IteratorState.Used;
            return iterator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract bool MoveNext();

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }

        protected enum IteratorState
        {
            Ready = 0,
            Used,
            Disposed
        }
    }
}