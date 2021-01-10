using System;

namespace Vanir.Utilities.Events
{
    public class UserCreatedEvent
    {
        public UserCreatedEvent(string username, string email, string password, byte[] salt)
        {
            Username = username;
            Email = email;
            Password = password;
            Salt = salt;
        }

        public byte[] Salt { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}