using InterfaceLaba1.Command.Base.Command.Group;
using InterfaceLaba1.Command.Base.Command.Student;
using InterfaceLaba1.Command.Common;

namespace InterfaceLaba1.Command.Auth;

public class PermittedActivities
{
    public Role Role { get; init; }
    public List<Type> TypesCommand { get; init; }

    private PermittedActivities(Role role, List<Type> typesCommand)
    {
        Role = role;
        TypesCommand = typesCommand;
    }

    public static PermittedActivities Get(Role role) => role switch
    {
        Role.Student => studentActivities,
        Role.Clerk => clerkActivities,
        _ => throw new ArgumentException()
    };

    private static PermittedActivities studentActivities
        = new(Role.Student, new List<Type>
        {
            typeof(GetStudentsCommand),
            typeof(GetStudentsCommand),
            typeof(GetStudentCommand),
            typeof(GetGroupCommand),
            typeof(LoginCommand),
            typeof(LogoutCommand),
            typeof(WhoamiCommand),
            typeof(GetAvailableCommand)
        });

    private static PermittedActivities clerkActivities
        = new(Role.Clerk, new List<Type>
        {
            typeof(AddGroupCommand),
            typeof(GetGroupCommand),
            typeof(GetAllGroupsCommand),
            typeof(UpdateGroupCommand),
            typeof(DeleteGroupCommand),
            typeof(AddStudentCommand),
            typeof(DeleteStudentCommand),
            typeof(UpdateStudentCommand),
            typeof(GetStudentsCommand),
            typeof(GetStudentCommand),
            typeof(LoginCommand),
            typeof(LogoutCommand),
            typeof(WhoamiCommand),
            typeof(GetAvailableCommand)
        });
}
