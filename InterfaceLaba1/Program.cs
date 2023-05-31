using InterfaceLaba1.Command.Auth;
using InterfaceLaba1.Command.Base;
using InterfaceLaba1.Command.Base.Command.Group;
using InterfaceLaba1.Command.Base.Command.Student;
using InterfaceLaba1.Command.Common;
using InterfaceLaba1.Model;

namespace InterfaceLaba1;

public class Program
{
    public static void Main(string[] args)
    {
        // фейк БД с учетками
        var credentials = new List<Credentials>()
        {
            new Credentials {Login = "student", Password = "123", Role = Role.Student},
            new Credentials {Login = "clerk", Password = "123", Role = Role.Clerk},
        };

        // Контекст программы (хранит текущего аутентифицированного юзера)
        var ctx = new MyContext();

        // фейк БД с группами
        var dbGroups = new List<GroupModel>();

        // регистрация поддерживаемых команд
        var commands = new List<BaseCommand>()
        {
            new AddGroupCommand(ctx, dbGroups),
            new GetGroupCommand(ctx, dbGroups),
            new GetAllGroupsCommand(ctx, dbGroups),
            new UpdateGroupCommand(ctx, dbGroups),
            new DeleteGroupCommand(ctx, dbGroups),

            new AddStudentCommand(ctx, dbGroups),
            new DeleteStudentCommand(ctx, dbGroups),
            new UpdateStudentCommand(ctx, dbGroups),
            new GetStudentsCommand(ctx, dbGroups),
            new GetStudentCommand(ctx, dbGroups),
            
            new LoginCommand(ctx, credentials),
            new LogoutCommand(ctx),
            new WhoamiCommand(ctx),
            new GetAvailableCommand(ctx)
        };

        // Начальный вывод
        Console.WriteLine(
            "Help - справка по всем коммандам\n" +
            "Help [command] - справка по камманде\n" +
            "q - выход из программы\n" +
            "---");

        // цикл обработки
        while (true)
        {
            Console.Write("\n> ");
            var partsLine = Console.ReadLine()?.Trim().Split(" ");
            if (partsLine == null || partsLine.Length == 0)
            {
                Console.WriteLine("Ошибка!!!");
                continue;
            }

            var cmd = partsLine[0];

            if (partsLine.Length == 1 && cmd == "Help")
            {
                commands.ForEach(x => Console.WriteLine($"{x}\n"));
                continue;
            }

            if (partsLine.Length == 2 && cmd == "Help")
            {
                Console.WriteLine(commands.FirstOrDefault(c => c.Name == partsLine[1]));
                continue;
            }

            if (partsLine.Length == 1 && cmd == "q")
            {
                break;
            }

            var command = commands.FirstOrDefault(c => c.Name == cmd);
            if (command is null)
            {
                Console.WriteLine("Не известная комманда");
                continue;
            }

            command.Execute(partsLine.Skip(1).ToList());
        }
    }
}
