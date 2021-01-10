using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class DashboardBuilder
    {
        private readonly Dashboard _dashboard;

        public DashboardBuilder() => _dashboard = new Dashboard();

        public Dashboard Build() => _dashboard;
    }
}