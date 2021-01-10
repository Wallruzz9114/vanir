using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class OrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder() => _order = new Order();

        public Order Build() => _order;
    }
}