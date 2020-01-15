using Glovali.Common.Concurrent;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Glovali.Common.Infrastructure.Messaging
{
    /// <summary>
    /// Represents the <see cref="MessageListener{T}"/> class.
    /// </summary>
    /// <typeparam name="TMessage">The type of message.</typeparam>
    public sealed class MessageListener<TMessage> : IObserver<TMessage>, IDisposable
    {
        private readonly AsyncConcurrentQueue<object> _messageQueue = new AsyncConcurrentQueue<object>();
        private IDisposable _unsubscriber;

        /// <summary>
        /// Subscribes the message listener to an <see cref="IObservable{TMessage}"/> provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Subscribe(IObservable<TMessage> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        /// <summary>
        /// Unsubscribes the message listener; i.e. disposes the subscription.
        /// </summary>
        public void Unsubscribe()
        {
            Dispose();
        }

        /// <summary>
        /// Method to call on error.
        /// </summary>
        public void OnError(Exception exception)
        {
            Debug.WriteLine($"{GetType().FullName}: OnError called.");
            Debug.WriteLine(exception.Message);
        }

        /// <summary>
        /// Adds the message to the queue.
        /// </summary>
        /// <param name="value">The value; in this case the message.</param>
        public void OnNext(TMessage value)
        {
            _messageQueue.Enqueue(value);
        }

        /// <summary>
        /// Receive a message asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> with type parameter <see cref="TMessage"/>.</returns>
        public async Task<TMessage> ReceiveMessageAsync(CancellationToken cancellationToken)
        {
            return (TMessage)await _messageQueue.DequeueAsync(cancellationToken);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _unsubscriber?.Dispose();
        }

        /// <inheritdoc cref="IObserver{T}.OnCompleted"/>
        public void OnCompleted()
        {
            Debug.WriteLine($"{GetType().FullName}: Completed.");
        }
    }
}
