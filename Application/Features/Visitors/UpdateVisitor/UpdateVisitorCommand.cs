namespace Application.Features.Visitors.UpdateVisitor;

public sealed record UpdateVisitorCommand : IRequest<Response<string>>, IValidatorRequest
{
    public string Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string NationalId { get; set; } = "";
}
