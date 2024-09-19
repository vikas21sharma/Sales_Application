using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace Sales_Application.Exception
{
    public class ForbiddenException : System.Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class UnauthorizedAccessException : System.Exception
    {
        public UnauthorizedAccessException(string message) : base(message) { }
    }

    public class BadRequestException : System.Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class InternalServerError : System.Exception
    {
        public InternalServerError(string message) : base(message) { }
    }
    public class GlobalExceptionHandler() : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = StatusCodes.Status500InternalServerError; // Default to 500

            // You might want to have custom logic to set status codes based on exception types
            if (exception is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
            }
            else if (exception is BadRequestException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if(exception is ForbiddenException)
            {
                statusCode = StatusCodes.Status403Forbidden;
            }
           
            // Add more conditions for other exceptions as needed

            // Create the response object
            //logger.LogError(exception, exception.Message);
            var response = new ErrorResponse()
            {
                StatusCode = statusCode,
                ExceptionMessage = exception.Message,
                Title = statusCode switch
                {
                   
                    StatusCodes.Status404NotFound => "No Records!",
                    StatusCodes.Status401Unauthorized => "Not authorized to Access",
                    StatusCodes.Status403Forbidden => "The User is forbidden",
                    StatusCodes.Status400BadRequest => "Bad Request",
                    StatusCodes.Status500InternalServerError => "Internal Server Error",
                    _ => "An Error Occurred"// default
                }
            };

            Log.Error(exception.Message, "An Error Occured");

            // Set the response status code and content type
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            // Write the JSON response
            httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            bool result = true; // or some boolean expression
            return new ValueTask<bool>(result);
        }
    }
}


