namespace Vanir.Utilities.Models
{
    public record Role
    {
        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}