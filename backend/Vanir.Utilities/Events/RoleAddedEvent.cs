namespace Vanir.Utilities.Events
{
    public class RoleAddedEvent
    {
        public RoleAddedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}