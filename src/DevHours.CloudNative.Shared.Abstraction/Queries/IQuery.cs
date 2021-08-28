using MediatR;

namespace DevHours.CloudNative.Shared.Abstraction.Queries
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
