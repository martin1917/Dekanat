using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Group;

class DeleteGroupCommand : BaseCommand
{
    public override string Description => "Удаление группы";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "group_name", description: "Название группы")
    };

    private readonly List<GroupModel> groups;

    public DeleteGroupCommand(MyContext ctx, List<GroupModel> groups) : base(ctx)
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

        var group = groups.FirstOrDefault(g => g.Name == args[0]);
        if (group is null)
        {
            Console.WriteLine("Такой группы не существует");
            return;
        }

        groups.Remove(group);
        Console.WriteLine($"Группа {group.Name} удалена");
    }
}
