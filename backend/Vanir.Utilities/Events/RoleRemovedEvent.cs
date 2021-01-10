namespace Vanir.Utilities.Events
{
    public class RoleRemovedEvent
    {
        public RoleRemovedEvent(string value)
        {
            Name = value;
        }

        public string Name { get; }
    }
}