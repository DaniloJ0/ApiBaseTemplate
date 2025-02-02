using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);  

    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(x => x.Id == id); 
}
