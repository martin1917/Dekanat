using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Student;

class UpdateStudentCommand : BaseCommand
{
    public override string Description => "обновление данных о студенте";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "group_name", description: "название группы"),
        new Argument(name: "student_id", description: "идентификатор студента"),
        new Argument(name: "new_first_name", description: "новое имя"),
        new Argument(name: "new_last_name", description: "новая фамилия"),
        new Argument(name: "new_patronymic", description: "новое отчество"),
        new Argument(name: "new_birthday", description: "новый день рождения"),
    };

    private readonly MyContext ctx;
    private readonly List<GroupModel> groups;

    public UpdateStudentCommand(MyContext ctx, List<GroupModel> groups)
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

        if (!int.TryParse(args[1], out int studentId))
        {
            Console.WriteLine("идентификатор студента должен быть целым числом");
            return;
        }

        if (!DateTime.TryParse(args[5], out DateTime birthday))
        {
            Console.WriteLine("формат строки для дня рождения неверный!");
            return;
        }

        var student = group.Students.FirstOrDefault(s => s.Id == studentId);
        if (student is null)
        {
            Console.WriteLine($"студента c идентификатором {studentId} не существует");
            return;
        }

        student.FirstName = args[2];
        student.SecondName = args[3];
        student.Patronymic = args[4];
        student.Birthday = birthday;
        Console.WriteLine($"Данные о студенте {studentId} обновлены");
    }
}
