using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Group;

class UpdateGroupCommand : BaseCommand
{
    public override string Name => "UpdateGroup";

    public override string Description => "Обновление данных о группе";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "updated_group_name", description: "Группа, которая будет обновлена"),
        new Argument(name: "new_group_name", description: "Новое имя для группы"),
        new Argument(name: "new_year", description: "Новый год для группы"),
    };

    private readonly MyContext ctx;
    private readonly List<GroupModel> groups;

    public UpdateGroupCommand(MyContext ctx, List<GroupModel> groups)
    {
        this.ctx = ctx;
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

        if (!PermittedActivities.Get(currentRole).Activities.Contains(TypeCommand.UpdateGroup))
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

        if (!int.TryParse(args[2], out int year) || !(2000 <= year && year <= 2050))
        {
            Console.WriteLine("Год должен быть целым числом в интервале (2000; 2050)");
            return;
        }

        (string prevName, int prevYear) = (group.Name, group.YearCreated);
        group.Name = args[1];
        group.YearCreated = year;
        Console.WriteLine($"Группа обновлена. ({prevName}, {prevYear}) --> ({group.Name}, {group.YearCreated})");
    }
}
