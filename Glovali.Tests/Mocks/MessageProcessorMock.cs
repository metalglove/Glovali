using Glovali.Common.Application.Interfaces;
using Glovali.Common.Concurrent;
using Glovali.Common.Messages.Interfaces;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glovali.Tests.Mocks
{
    public class MessageProcessorMock : IMessageProcessor
    {
        private readonly IMessageSerializer _messageSerializer;
        private readonly AsyncConcurrentQueue<IMessage> _messages;

        public MessageProcessorMock(IMessageSerializer messageSerializer)
        {
            _messageSerializer = messageSerializer;
            _messages = new AsyncConcurrentQueue<IMessage>();
        }

        public Task ProcessMessageAsync(IMessage message, INetworkConnector networkConnector)
        {
            _messages.Enqueue(message);
            Console.WriteLine(Encoding.UTF8.GetString(_messageSerializer.Serialize(message).ToArray()));
            return Task.CompletedTask;
        }

        public Task<IMessage> GetMessageAsync(CancellationToken cancellationToken)
        {
            return _messages.DequeueAsync(cancellationToken);
        }
    }
}
