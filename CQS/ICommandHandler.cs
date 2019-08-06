using System.Threading.Tasks;
using CQS.Data;

namespace CQS
{
    public interface ICommandHandler<in TParameter, TResult> where TParameter : ICommand
        where TResult : IResult
    {
        TResult Handle(TParameter command);

        Task<TResult> HandleAsync(TParameter command);
    }
}