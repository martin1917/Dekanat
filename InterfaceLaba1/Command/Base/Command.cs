using InterfaceLaba1.Command.Auth;

namespace InterfaceLaba1.Command.Base;

public abstract class BaseCommand
{
    protected readonly MyContext ctx;

    public virtual string Name => GetType().Name[..^"Command".Length];
    public abstract string Description { get; }
    public virtual List<Argument> Arguments { get; } = new();

    public BaseCommand(MyContext ctx)
    {
        this.ctx = ctx;
    }

    public abstract void Execute(List<string> args);

    public override string? ToString()
    {
        var args = Arguments.Select(a => $"[{a.Name}]");
        if (Arguments.Any())
        {
            return 
                $"{Description}\n" + 
                $"  ==> {Name} {string.Join(" ", args)}\n" + 
                $"\t* {string.Join("\n\t* ", Arguments)}";

        }
        else
        {
            return 
                $"{Description}\n" + 
                $"  ==> {Name} {string.Join(" ", args)}";
        }
    }
}