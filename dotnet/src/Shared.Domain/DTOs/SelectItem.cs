namespace Shared.Domain.DTOs
{
    public class SelectItem
    {
        public SelectItem(int value, string name)
        {
            Value = value;
            Name = name;
        }
        public int Value { get; private set; }
        public string Name { get; private set; }
    }
}
