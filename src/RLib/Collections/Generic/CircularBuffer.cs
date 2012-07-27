using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RLib.Collections.Generic
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly Queue<T> _queue;

        private readonly int _maxSize;
		
		private static readonly object Locker = new object();

        public CircularBuffer(int maxSize)
        {
            if (maxSize < 1) throw new ArgumentOutOfRangeException("maxSize");

            _maxSize = maxSize;
            _queue = new Queue<T>(_maxSize);
        }

        public int MaxSize
        {
            get { return _maxSize; }
        }

        public bool IsFull
        {
            get { return _queue.Count == _maxSize; }
        }

        public void Add(T item)
        {
			lock(Locker)
			{
	            _queue.Enqueue(item);
	            while (_queue.Count > _maxSize)
	            {
	                _queue.Dequeue();
	            }
			}
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _queue.AsEnumerable().GetEnumerator();
        }
    }
}
