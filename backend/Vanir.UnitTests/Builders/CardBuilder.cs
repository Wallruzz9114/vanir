using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class CardBuilder
    {
        private readonly Card _card;

        public CardBuilder() => _card = new Card();

        public Card Build() => _card;
    }
}