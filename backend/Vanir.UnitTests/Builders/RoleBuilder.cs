using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class RoleBuilder
    {
        private readonly Role _role;

        public RoleBuilder(string name) => _role = new Role(name);

        public Role Build() => _role;
    }
}