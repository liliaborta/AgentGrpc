using Broker.Models;
using Broker.Services.Interfaces;
using Grpc.Core;
using GrpcAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class PublisherService : Publisher.PublisherBase
    {
        private readonly IMessageStorageService _messagestorage;
        public PublisherService(IMessageStorageService messageStorageService)
        {
            _messagestorage = messageStorageService;
        }
        public override Task<PublishReply> PublishMessage(PublishRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Received: {request.Topic} {request.Content}");

            var message = new Message(request.Topic, request.Content);
            _messagestorage.Add(message);

            return Task.FromResult(new PublishReply()
            {
                IsSucces = true
            });
        }
    }
}
