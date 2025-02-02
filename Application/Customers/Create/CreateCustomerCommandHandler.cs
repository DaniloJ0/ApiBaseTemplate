using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRespository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRespository = customerRespository ?? throw new ArgumentNullException(nameof(customerRespository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber) 
            return Error.Validation("Customer.PhoneNumber", "Phone Number is not a valid format.");

        Customer customer = new
        (
            new CustomerId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            command.Email,
            phoneNumber
         );

        await _customerRespository.Add( customer );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
