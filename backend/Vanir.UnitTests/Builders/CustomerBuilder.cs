using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class CustomerBuilder
    {
        private readonly Customer _customer;

        public CustomerBuilder() => _customer = new Customer();

        public Customer Build() => _customer;
    }
}