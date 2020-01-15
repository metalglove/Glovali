using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Glovali.Common.Application.Interfaces;
using Glovali.Common.Application.Serializers;
using Glovali.Common.Infrastructure;
using Glovali.Common.Infrastructure.Networking;
using Glovali.Common.Messages.Interfaces;
using Glovali.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glovali.Tests.Infrastructure
{
    [TestClass]
    public class TcpNetworkConnectorTests
    {
        private const int DefaultTimeOut = 5;
        public IMessageSerializer MessageSerializer { get; set; }
        public MessageProcessorMock MessageProcessor { get; set; }
        public IMessageTypeCache MessageTypeCache { get; set; }
        public IFactory<IMessageTypeCache, IEnumerable<Type>> MessageTypeCacheFactory { get; set; }
        public string Host { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Host = "localhost";
            MessageSerializer = new JsonMessageSerializer();
            MessageProcessor = new MessageProcessorMock(MessageSerializer);
            MessageTypeCacheFactory = new MessageTypeCacheFactory();
            List<Type> types = new List<Type>
            {
                typeof(TestRequestMessage)
            };
            MessageTypeCache = MessageTypeCacheFactory.Create(types);
        }

        [TestMethod]
        public async Task ConnectAsync_Should_Set_IsConnected_To_True()
        {
            using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(DefaultTimeOut * 3));
            _ = StartServer(cts.Token, 9989);
            TcpNetworkConnector tcpNetworkConnector = await StartClient(cts.Token, 9989);
            Assert.IsTrue(tcpNetworkConnector.IsConnected);
            cts.Cancel();
        }

        [TestMethod]
        public async Task SendMessageAsync_Should_Invoke_MessageProcessor()
        {
            using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(DefaultTimeOut * 3));
            _ = StartServer(cts.Token, 9988);
            TcpNetworkConnector tcpNetworkConnector = await StartClient(cts.Token, 9988);
            TestRequestMessage message = new TestRequestMessage() { Message = "Test123" };
            await tcpNetworkConnector.SendMessageAsync(message, cts.Token);
            IMessage messageInMessageProcessor = await MessageProcessor.GetMessageAsync(cts.Token);
            Assert.AreEqual(message.Id, messageInMessageProcessor?.Id);
            cts.Cancel();
        }

        private async Task StartServer(CancellationToken cancellationToken, int port)
        {
            Console.WriteLine("Starting listener...");
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            Console.WriteLine("Stared listener!");
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Awaiting new connection...");
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("Accepted connection!");
                _ = Task.Run(() =>
                {
                    TcpNetworkConnector networkConnector = new TcpNetworkConnector(MessageTypeCache, MessageSerializer, MessageProcessor, tcpClient);
                    networkConnector.Start();
                    Console.WriteLine("Started tcp network connector");
                }, cancellationToken);
            }
        }

        private async Task<TcpNetworkConnector> StartClient(CancellationToken cancellationToken, int port)
        {
            TcpNetworkConnector tcpNetworkConnector = new TcpNetworkConnector(MessageTypeCache, MessageSerializer, MessageProcessor, Host, port);
            await tcpNetworkConnector.ConnectAsync(cancellationToken);
            tcpNetworkConnector.Start();
            return tcpNetworkConnector;
        }
    }
}
