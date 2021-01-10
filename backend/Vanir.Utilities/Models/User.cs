using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Events;
using Vanir.Utilities.Implentations;

namespace Vanir.Utilities.Models
{
    public class User : AggregateRoot
    {
        public User(string username, string email, string password)
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashedPassword = new PasswordHasher().HashPassword(salt, password);

            Apply(new UserCreatedEvent(username, email, hashedPassword, salt));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(UserCreatedEvent userCreatedEvent)
        {
            Salt = userCreatedEvent.Salt;
            Username = userCreatedEvent.Username;
            Email = userCreatedEvent.Email;
            Password = userCreatedEvent.Password;
            Roles = new HashSet<Role>();
        }

        protected void When(PasswordChangedEvent passwordChangedEvent) => Password = passwordChangedEvent.Password;

        protected void When(RoleAddedEvent roleAddedEvent) => Roles.Add(new Role(roleAddedEvent.Name));

        protected void When(RoleRemovedEvent roleRemovedEvent) => Roles.Add(new Role(roleRemovedEvent.Name));

        protected override void EnsureValidState() { }

        public void ChangePassword(string password) => Apply(new PasswordChangedEvent(password));

        public void AddRole(string name) => Apply(new RoleAddedEvent(name));

        public void RemoveRole(string value) => Apply(new RoleRemovedEvent(value));

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}