using MediatR;

namespace DevHours.CloudNative.Shared.Abstraction.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
