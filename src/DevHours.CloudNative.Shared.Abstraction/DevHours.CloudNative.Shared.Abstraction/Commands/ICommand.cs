using MediatR;

namespace DevHours.CloudNative.Shared.Abstraction.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
