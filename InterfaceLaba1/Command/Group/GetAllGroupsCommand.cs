using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Group;

class GetAllGroupsCommand : BaseCommand
{
    public override string Description => "Получить все группы";

    private readonly List<GroupModel> groups;

    public GetAllGroupsCommand(MyContext ctx, List<GroupModel> groups) : base(ctx)
    {
        this.groups = groups;
    }

    public override void Execute(List<string> args)
    {
        if (ctx.CurrentUser is null)
        {
            Console.WriteLine("Вы не прошли аутентификацию");
            return;
        }

        var currentRole = ctx.CurrentUser.Role;

        if (!PermittedActivities.Get(currentRole).TypesCommand.Contains(GetType()))
        {
            Console.WriteLine($"Данная комманда не разрешена вашей роли ({currentRole})");
            return;
        }

        if (args.Count != Arguments.Count)
        {
            Console.WriteLine("Ошибка с кол-во аргументов");
            return;
        }
        Console.WriteLine(string.Join("\n", groups.Select(g => $"{g.Name}")));
    }
}
