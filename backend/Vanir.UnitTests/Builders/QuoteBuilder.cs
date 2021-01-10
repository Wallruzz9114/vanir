using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class QuoteBuilder
    {
        private readonly Quote _quote;

        public QuoteBuilder() => _quote = new Quote();

        public Quote Build() => _quote;
    }
}