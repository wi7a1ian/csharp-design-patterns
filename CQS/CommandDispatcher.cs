﻿using System;
using System.Threading.Tasks;
using Autofac;
using CQS.Data;

namespace CQS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _Context;

        public CommandDispatcher(IComponentContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult
        {
            //Look up the correct CommandHandler in our IoC container and invoke the Handle method

            var _handler = _Context.Resolve<ICommandHandler<TParameter, TResult>>();
            return _handler.Handle(command);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult
        {
            //Look up the correct CommandHandler in our IoC container and invoke the async Handle method

            var _handler = _Context.Resolve<ICommandHandler<TParameter, TResult>>();
            return await _handler.HandleAsync(command);
        }
    }
}