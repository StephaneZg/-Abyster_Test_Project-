
using Abyster_Test_Project.SharedKernel;
using MediatR;
using MediatR.Pipeline;

namespace Abyster_Test_Project.Config;

public class ExceptionHandler<TRequest, TResponse> : IRequestExceptionHandler<TRequest, TResponse, Exception>
    where TRequest : IRequest<TResponse>
    where TResponse : ErrorDetail, new()
{
    public async Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var response = new TResponse(){
            message = exception.Message,
            statusCode = StatusCodes.Status400BadRequest
        };

        state.SetHandled(response);
    }
}