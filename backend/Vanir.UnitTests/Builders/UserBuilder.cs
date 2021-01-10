using Vanir.Utilities.Models;

namespace Vanir.UnitTests.Builders
{
    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder(string username, string email, string password) =>
            _user = new User(username, email, password);

        public User Build() => _user;
    }
}