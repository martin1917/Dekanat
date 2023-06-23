using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Command.Base;

namespace InterfaceLaba1.Command.Common;

public class LogoutCommand : BaseCommand
{
    public override string Description => "выйти из учетной записи";

    public LogoutCommand(MyContext ctx) : base(ctx)
    {

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

        ctx.CurrentUser = null;
        Console.WriteLine("Вы успешно вышли из учетной записи");
    }
}
