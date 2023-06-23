using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Group;

public class AddGroupCommand : BaseCommand
{
    public override string Description => "Добавление учебной группы";
    public override List<Argument> Arguments => new()
    {
        new Argument(name: "group_name", description: "название группы"),
        new Argument(name: "year", description: "год образования группы")
    };

    private readonly List<GroupModel> groups;

    public AddGroupCommand(MyContext ctx, List<GroupModel> groups) : base(ctx)
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

        if (!int.TryParse(args[1], out int year) || !(2000 <= year && year <= 2050))
        {
            Console.WriteLine("Год должен быть целым числом в интервале (2000; 2050)");
            return;
        }

        if (groups.Any(g => g.Name == args[0]))
        {
            Console.WriteLine("Такая группа уже существует");
            return;
        }

        groups.Add(new GroupModel(args[0], year));
        Console.WriteLine("Группа успешно создана");
    }
}
