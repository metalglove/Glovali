using Glovali.Common.Application.Interfaces;
using Glovali.Common.Application.Serializers;
using Glovali.Common.Infrastructure;
using Glovali.Common.Infrastructure.Networking;
using Glovali.Common.Messages.Interfaces;
using Glovali.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Glovali.Tests.Infrastructure
{
    [TestClass]
    public class WSNetworkConnectorTests
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
            _ = StartServer(cts.Token, 9986);
            WSNetworkConnector wsNetworkConnector = await StartClient(cts.Token, 9986);
            Assert.IsTrue(wsNetworkConnector.IsConnected);
            cts.Cancel();
        }

        [TestMethod]
        public async Task SendMessageAsync_Should_Invoke_MessageProcessor()
        {
            using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(DefaultTimeOut * 3));
            _ = StartServer(cts.Token, 9987);
            TestRequestMessage message = new TestRequestMessage() { Message = "Test123" };
            WSNetworkConnector wsNetworkConnector = await StartClient(cts.Token, 9987);
            await wsNetworkConnector.SendMessageAsync(message, cts.Token);
            IMessage messageInMessageProcessor = await MessageProcessor.GetMessageAsync(cts.Token);
            Assert.AreEqual(message.Id, messageInMessageProcessor?.Id);
            cts.Cancel();
        }

        private async Task StartServer(CancellationToken cancellationToken, int port)
        {
            Console.WriteLine("Starting listener...");
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            Console.WriteLine("Started listener!");
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Awaiting new connection...");
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("Accepted connection!");
                _ = Task.Run(async () =>
                {
                    WSNetworkConnector networkConnector = new WSNetworkConnector(MessageTypeCache, MessageSerializer, MessageProcessor, tcpClient, "glovali");
                    await networkConnector.StartHandshakeAsServerAsync();
                    networkConnector.Start();
                    Console.WriteLine("Started websocket network connector");
                }, cancellationToken);
            }
        }

        private async Task<WSNetworkConnector> StartClient(CancellationToken cancellationToken, int port)
        {
            WSNetworkConnector wsNetworkConnector = new WSNetworkConnector(MessageTypeCache, MessageSerializer, MessageProcessor, Host, port, "glovali");
            await wsNetworkConnector.ConnectAsync(cancellationToken);
            await wsNetworkConnector.StartHandshakeAsClientAsync();
            wsNetworkConnector.Start();
            return wsNetworkConnector;
        }
    }
}
