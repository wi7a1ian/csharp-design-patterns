using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CQS.Data;
using CQS.DataAccess;

namespace CQS
{
    public abstract class CommandHandler<TRequest, TResult> : ICommandHandler<TRequest, TResult>
        where TRequest : ICommand
        where TResult : IResult, new()
    {
        protected ApplicationDbContext ApplicationDbContext;

        protected CommandHandler(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
        }


        public TResult Handle(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();
            
            TResult _response;

            try
            {
                //do data validation
                //do authorization

                _response = DoHandle(command);
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

            return _response;
        }

        public async Task<TResult> HandleAsync(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TResult> _response;

            try
            {
                //do data validation
                //do authorization

                _response = DoHandleAsync(command);
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

            return await _response;
        }

        // Protected methods
        protected abstract TResult DoHandle(TRequest request);

        protected abstract Task<TResult> DoHandleAsync(TRequest request);
    }
}