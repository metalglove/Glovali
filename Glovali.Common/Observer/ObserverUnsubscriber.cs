using System;

namespace Glovali.Common.Observer
{
    /// <summary>
    /// Represents the <see cref="ObserverUnsubscriber{T}"/> class.
    /// </summary>
    public sealed class ObserverUnsubscriber<T> : IDisposable
    {
        private readonly ObserverCollection<T> _observers;
        private readonly IObserver<T> _observer;

        /// <summary>
        /// Initializes an instance of the <see cref="ObserverUnsubscriber{T}"/> class.
        /// </summary>
        /// <param name="observerCollection">The observer collection.</param>
        /// <param name="observer">The observer.</param>
        public ObserverUnsubscriber(ObserverCollection<T> observerCollection, IObserver<T> observer)
        {
            _observers = observerCollection;
            _observer = observer;
        }

        /// <summary>
        /// Disposes the <see cref="_observer"/> from the <see cref="_observers"/>.
        /// </summary>
        public void Dispose()
        {
            if (_observer != null)
                _observers.Remove(_observer);
        }
    }
}
