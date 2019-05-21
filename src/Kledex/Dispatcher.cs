﻿using System.Threading.Tasks;
using OpenCqrs.Bus;
using OpenCqrs.Commands;
using OpenCqrs.Domain;
using OpenCqrs.Events;
using OpenCqrs.Queries;

namespace OpenCqrs
{
    /// <inheritdoc />
    /// <summary>
    /// Dispatcher
    /// </summary>
    /// <seealso cref="T:OpenCqrs.IDispatcher" />
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandSender _commandSender;
        private readonly IDomainCommandSender _domainCommandSender;
        private readonly IEventPublisher _eventPublisher;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IBusMessageDispatcher _busMessageDispatcher;

        public Dispatcher(ICommandSender commandSender,
            IDomainCommandSender domainCommandSender,
            IEventPublisher eventPublisher, 
            IQueryProcessor queryProcessor, 
            IBusMessageDispatcher busMessageDispatcher)
        {
            _commandSender = commandSender;
            _domainCommandSender = domainCommandSender;
            _eventPublisher = eventPublisher;
            _queryProcessor = queryProcessor;
            _busMessageDispatcher = busMessageDispatcher;            
        }

        /// <inheritdoc />
        public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _commandSender.SendAsync(command);
        }

        /// <inheritdoc />
        public Task SendAsync<TCommand, TAggregate>(TCommand command) 
            where TCommand : IDomainCommand 
            where TAggregate : IAggregateRoot
        {
            return _domainCommandSender.SendAsync<TCommand, TAggregate>(command);
        }

        /// <inheritdoc />
        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return _eventPublisher.PublishAsync(@event);
        }

        /// <inheritdoc />
        public Task<TResult> GetResultAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _queryProcessor.ProcessAsync<TQuery, TResult>(query);
        }

        /// <inheritdoc />
        public Task DispatchBusMessageAsync<TMessage>(TMessage message) where TMessage : IBusMessage
        {
            return _busMessageDispatcher.DispatchAsync(message);
        }

        /// <inheritdoc />
        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandSender.Send(command);
        }

        /// <inheritdoc />
        public void Send<TCommand, TAggregate>(TCommand command) 
            where TCommand : IDomainCommand 
            where TAggregate : IAggregateRoot
        {
            _domainCommandSender.Send<TCommand, TAggregate>(command);
        }

        /// <inheritdoc />
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _eventPublisher.Publish(@event);
        }

        /// <inheritdoc />
        public TResult GetResult<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _queryProcessor.Process<TQuery, TResult>(query);
        }
    }
}