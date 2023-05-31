using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Model;

namespace InterfaceLaba1.Command.Base.Command.Student;

class DeleteStudentCommand : BaseCommand
{
    public override string Description => "Удалить данные о студенте";

    public override List<Argument> Arguments => new()
    {
        new Argument(name: "group_name", description: "название группы"),
        new Argument(name: "student_id", description: "идентификатор студента")
    };

    private readonly MyContext ctx;
    private readonly List<GroupModel> groups;

    public DeleteStudentCommand(MyContext ctx, List<GroupModel> groups)
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

        var student = group.Students.FirstOrDefault(s => s.Id == studentId);
        if (student is null)
        {
            Console.WriteLine($"студента c идентификатором {studentId} не существует");
            return;
        }

        group.Students.Remove(student);
        Console.WriteLine($"студента c идентификатором {studentId} удален");
    }
}
