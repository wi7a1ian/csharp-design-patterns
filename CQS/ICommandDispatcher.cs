using System.Threading.Tasks;
using CQS.Data;
using ICommand = CQS.Data.ICommand;

namespace CQS
{
    /// <summary>
    /// Passed around to all allow dispatching a command and to be mocked by unit tests
    /// </summary>
    public interface ICommandDispatcher
    {
        TResult Dispatch<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult;

        Task<TResult> DispatchAsync<TParameter, TResult>(TParameter command) where TParameter : ICommand where TResult : IResult;
    }
}