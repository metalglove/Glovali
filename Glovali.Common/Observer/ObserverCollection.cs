using System;
using System.Collections.Generic;

namespace Glovali.Common.Observer
{
    /// <summary>
    /// Represents the <see cref="ObserverCollection{T}"/> class.
    /// </summary>
    public sealed class ObserverCollection<T> : List<IObserver<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverCollection{T}"/> class.
        /// </summary>
        /// <param name="observer">The first observer.</param>
        public ObserverCollection(IObserver<T> observer)
        {
            Add(observer);
        }

        /// <summary>
        /// Calls all the <see cref="IObserver{T}.OnNext"/> callbacks in the list.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void OnNextAll(T obj)
        {
            ForEach(observer => observer.OnNext(obj));
        }

        /// <summary>
        /// Calls all the <see cref="IObserver{T}.OnError"/> callbacks in the list.
        /// </summary>
        /// <param name="exception">The exception which the error caused.</param>
        public void OnErrorAll(Exception exception)
        {
            ForEach(observer => observer.OnError(exception));
        }
    }
}
