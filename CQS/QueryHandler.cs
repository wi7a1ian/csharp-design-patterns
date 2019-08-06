using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CQS.Data;
using CQS.DataAccess;

namespace CQS
{
    public abstract class QueryHandler<TParameter, TResult> : IQueryHandler<TParameter, TResult>
        where TResult : IResult, new()
        where TParameter : IQuery, new()
    {
        protected ApplicationDbContext ApplicationDbContext;

        protected QueryHandler(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public TResult Retrieve(TParameter query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            TResult _queryResult;

            try
            {
                //do authorization and validatiopn

                //handle the query request
                _queryResult = Handle(query);
                
            }
            catch (Exception _exception)
            {
                // log
                throw;
            }
            finally
            {
                _stopWatch.Stop();
            }

            return _queryResult;
        }

        public async Task<TResult> RetrieveAsync(TParameter query)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TResult> _queryResult;

            try
            {
                //do authorization and validation

                //handle the query request
                _queryResult = HandleAsync(query);

            }
            catch (Exception _exception)
            {
                // log
                throw;
            }
            finally
            {
                _stopWatch.Stop();
                // Response for query {0} served (elapsed time: {1} msec)
            }


            return await _queryResult;
        }

        protected abstract TResult Handle(TParameter request);

        protected abstract Task<TResult> HandleAsync(TParameter request);

    }
}