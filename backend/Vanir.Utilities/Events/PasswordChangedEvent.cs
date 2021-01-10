namespace Vanir.Utilities.Events
{
    public class PasswordChangedEvent
    {
        public PasswordChangedEvent(string password) => Password = password;

        public string Password { get; }
    }
}