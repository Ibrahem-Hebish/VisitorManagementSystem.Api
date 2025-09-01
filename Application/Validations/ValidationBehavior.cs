using MediatR;
using System.Net;

namespace Application.Validation;

public class ValidationBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor,
    IEnumerable<IValidator<TRequest>> validators)

    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidatorRequest
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(
            validators.Select(v =>
            v.ValidateAsync(request, cancellationToken)));

            var failures = validationResult
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .ToList();

            if (failures.Count != 0)
            {
                httpContextAccessor.HttpContext!.Response.StatusCode =
                    (int)HttpStatusCode.BadRequest;

                throw new ValidationException(failures
                     .Select(x => x.ErrorMessage)
                     .FirstOrDefault());
            }

        }
        return await next.Invoke(cancellationToken);
    }
}

