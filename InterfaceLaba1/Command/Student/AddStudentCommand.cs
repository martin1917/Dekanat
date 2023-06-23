using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Student;

class AddStudentCommand : BaseCommand
{
    public override string Description => "Добавление данных о студенте";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "group_name", description: "Название группы, в которой учится студент"),
        new Argument(name: "first_name", description: "Имя"),
        new Argument(name: "last_name", description: "Фамилия"),
        new Argument(name: "patronymic", description: "Отчество"),
        new Argument(name: "birthday", description: "Дата рождения"),
    };

    private readonly List<GroupModel> groups;

    public AddStudentCommand(MyContext ctx, List<GroupModel> groups) : base(ctx)
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

        if (!DateTime.TryParse(args[4], out DateTime birthday))
        {
            Console.WriteLine("формат строки для дня рождения неверный!");
            return;
        }

        var student = new StudentModel(args[1], args[2], args[3], birthday);
        group.Students.Add(student);
        Console.WriteLine($"Добавлен студeнт\n{student}\nв группу {group.Name}");
    }
}
