using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Command.Base;

namespace InterfaceLaba1.Command.Common;

public class GetAvailableCommand : BaseCommand
{
    public override string Name => "GetAvailableCommands";

    public override string Description => "получить комманды доступные для текущей роли";

    public override List<Argument> Arguments => new();

    private readonly MyContext ctx;

    public GetAvailableCommand(MyContext ctx)
    {
        this.ctx = ctx;
    }

    public override void Execute(List<string> args)
    {
        if (ctx.CurrentUser is null)
        {
            Console.WriteLine("Вы еще не прошли аутентификацию");
            return;
        }

        if (args.Count != Arguments.Count)
        {
            Console.WriteLine("Ошибка с кол-во аргументов");
            return;
        }

        var role = ctx.CurrentUser.Role;
        PermittedActivities.Get(role).Activities
            .ForEach(act => Console.WriteLine($"  * {act}"));
    }
}
