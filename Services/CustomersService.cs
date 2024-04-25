using Grpc.Core;

namespace GrpcServer.Services;

public class CustomersService : Customer.CustomerBase
{
    private ILogger<CustomersService> logger;

    public CustomersService(ILogger<CustomersService> logger)
    {
        this.logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        var dictionary = new Dictionary<int, CustomerModel>()
        {
            { 1 , new CustomerModel
                  {
                    FirstName = "Jamie",
                    LastName = "Smith"
                  }
            },
            { 2 , new CustomerModel
                  {
                    FirstName = "Jane",
                    LastName = "Doe"
                  }
            },
            { 3 , new CustomerModel
                  {
                    FirstName = "Greg",
                    LastName = "Thomas"
                  }
            }
        };

        return Task.FromResult(dictionary[request.UserId]);
    }

    public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
    {
        var customers = new List<CustomerModel>()
        {
            new() {
                FirstName = "Paul",
                LastName = "Gates",
                Age = 50,
                EmailAddress = "paul@abv.bg",
                IsAlive = true
            },
            new() {
                FirstName = "Bill",
                LastName = "Allen",
                Age = 45,
                EmailAddress = "bill@abv.bg",
                IsAlive = true
            }
        };

        foreach (var customer in customers)
        {
            await responseStream.WriteAsync(customer);
        }
    }
}
