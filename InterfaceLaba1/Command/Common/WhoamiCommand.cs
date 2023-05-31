using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Command.Base;

namespace InterfaceLaba1.Command.Common;

public class WhoamiCommand : BaseCommand
{
    public override string Description => "узнать свою роль";

    public override List<Argument> Arguments => new();

    private readonly MyContext ctx;

    public WhoamiCommand(MyContext ctx)
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

        Console.WriteLine($"Вы: {ctx.CurrentUser?.Role}");
    }
}
