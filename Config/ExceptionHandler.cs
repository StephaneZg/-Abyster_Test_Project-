
using Abyster_Test_Project.SharedKernel;
using MediatR;
using MediatR.Pipeline;

namespace Abyster_Test_Project.Config;

public abstract class ExceptionHandler<TRequest, TResponse> : RequestExceptionHandler<TRequest, TResponse, Exception>
    where TRequest : IRequest<TResponse>
    where TResponse : ErrorDetail, new()
{
    protected override void Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
    {
        var response = new TResponse(){
            message = exception.Message,
            statusCode = StatusCodes.Status400BadRequest
        };

        state.SetHandled(response);
    }
}