using System.Threading.Tasks;
using CQS.Data;

namespace CQS
{
    /// <summary>
    /// Dispatches a query and invokes the corresponding handler
    /// </summary>
    public interface IQueryDispatcher
    {
        TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult;

        Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IResult;
    }
}