using ErrorOr;
using MediatR;

namespace Application.Customers.Create;

public record CreateCustomerCommand(
    string Name,
    string LastName,
    string Email, 
    string PhoneNumber
) : IRequest<ErrorOr<Unit>>;
