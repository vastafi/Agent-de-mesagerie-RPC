﻿using Broker.Models;
using Broker.Services.Interfaces;
using Grpc.Core;
using GrpcAgent;
using System;
using System.Threading.Tasks;


namespace Broker.Services
{
    public class PublisherService: Publisher.PublisherBase
    {
        private readonly IMessageStorageService _messageStorage;
        public PublisherService(IMessageStorageService messageStorageService)
        {
            _messageStorage = messageStorageService;
        }
        public override Task<PublisherReply> PublishMessage(PublisherRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Received: {request.Topic} {request.Content}");

            var message = new Message(request.Topic, request.Content);
            _messageStorage.Add(message);

            return Task.FromResult(new PublisherReply()
            {
                IsSuccess = true
            });
        }
    }
}
