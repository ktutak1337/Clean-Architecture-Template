using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Application.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
