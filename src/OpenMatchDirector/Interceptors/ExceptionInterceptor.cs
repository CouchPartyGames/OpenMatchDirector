using System.Reflection.Metadata;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace OpenMatchDirector.Interceptors;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;
    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
        BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Starting call. Type/Method: {Type} / {Method}",
            context.Method.Type, context.Method.Name);
        
        return continuation(request, context);
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Async Client call. Type/Method: {Type} / {Method}",
            context.Method.Type, context.Method.Name);
        
        return continuation(request, context);
    }

    public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
        AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Async Client streaming call. Type/Method: {Type} / {Method}",
            context.Method.Type, context.Method.Name);
        
        return continuation(context);
    }

    public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Async Server streaming call. Type/Method: {Type} / {Method}",
            context.Method.Type, context.Method.Name);

        try
        {
            return continuation(request, context);
        }
        catch (Exception e)
        {
            LogError(e);
            throw;
        }
    }
    
    private void LogCall<TRequest, TResponse>(Method<TRequest, TResponse> method)
        where TRequest : class
        where TResponse : class
    {
        _logger.LogInformation($"Starting call. Name: {method.Name}. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
    }
    
    private void LogError(Exception ex)
    {
        _logger.LogError(ex, $"Call error: {ex.Message}");
    }
    
}