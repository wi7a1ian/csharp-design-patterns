﻿using System;
using System.Threading.Tasks;
using Autofac;
using CQS.Data;

namespace CQS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _Context;

        public QueryDispatcher(IComponentContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult
        {
            //Look up the correct QueryHandler in our IoC container and invoke the retrieve method

            var _handler = _Context.Resolve<IQueryHandler<TParameter, TResult>>();
            return _handler.Retrieve(query);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult
        { 
            //Look up the correct QueryHandler in our IoC container and invoke the retrieve method

            var _handler = _Context.Resolve<IQueryHandler<TParameter, TResult>>();
            return await _handler.RetrieveAsync(query);
        }
    }
}