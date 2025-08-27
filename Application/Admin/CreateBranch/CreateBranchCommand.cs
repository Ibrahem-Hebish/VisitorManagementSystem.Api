using Application.Validation;

namespace Application.Admin.CreateBranch;

public record CreateBranchCommand(
    string BranchName,
    string Country,
    string City,
    string Street,
    string PhoneNumber,
    string Email)

    : IRequest<Response<string>>, IValidatorRequest
{ }
