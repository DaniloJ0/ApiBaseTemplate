using Application.Customers.Create;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("Customers")]
[Authorize]
public class CustomerController(ISender mediator) : ApiController
{
    private readonly ISender _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        ErrorOr<Unit> customerResult = await _mediator.Send(command);

        return customerResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }
    
}
