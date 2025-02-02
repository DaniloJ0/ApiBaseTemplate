using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Customers;

public sealed class Customer : AggregateRoot
{
    public Customer() { }

    public Customer(CustomerId id, string name, string lastName, string email, PhoneNumber phoneNumber)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public CustomerId Id { get; set; }  
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PhoneNumber PhoneNumber { get; set; }
}
