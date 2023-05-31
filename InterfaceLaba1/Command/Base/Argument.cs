namespace InterfaceLaba1.Command.Base;

public class Argument
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Argument(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string? ToString()
    {
        return $"{Name} - {Description}";
    }
}
