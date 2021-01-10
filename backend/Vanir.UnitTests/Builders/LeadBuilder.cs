using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class LeadBuilder
    {
        private readonly Lead _lead;

        public LeadBuilder() => _lead = new Lead();

        public Lead Build() => _lead;
    }
}