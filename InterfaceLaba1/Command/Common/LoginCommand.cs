using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Command.Base;

namespace InterfaceLaba1.Command.Common;

public class LoginCommand : BaseCommand
{
    public override string Name => "Login";

    public override string Description => "аутентификация";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "login", description: "логин"),
        new Argument(name: "password", description: "пароль"),
    };

    private readonly MyContext ctx;
    private readonly List<Credentials> credentials;

    public LoginCommand(MyContext ctx, List<Credentials> credentials)
    {
        this.ctx = ctx;
        this.credentials = credentials;
    }

    public override void Execute(List<string> args)
    {
        if (args.Count != Arguments.Count)
        {
            Console.WriteLine("Ошибка с кол-во аргументов");
            return;
        }

        var user = credentials.FirstOrDefault(c => c.Login == args[0] && c.Password == args[1]);
        if (user is null)
        {
            Console.WriteLine("Такой учетной записи нет");
            return;
        }

        ctx.CurrentUser = user;
        Console.WriteLine($"Вы успешно прошли аутентификацию. Ваша роль: {user.Role}");
    }
}
