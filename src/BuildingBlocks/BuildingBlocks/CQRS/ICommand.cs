using MediatR;

namespace BuildingBlocks.CQRS;

// Unit is a special type from MediatR that basically means “no return value” (like void).
public interface ICommand : ICommand<Unit> { }


// keyword out is used on the generic type parameter TResponse
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}