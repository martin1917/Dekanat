namespace InterfaceLaba1.Command.Auth;

public class PermittedActivities
{
    public Role Role { get; init; }
    public List<TypeCommand> Activities { get; init; }

    private PermittedActivities(Role role, List<TypeCommand> activities)
    {
        Role = role;
        Activities = activities;
    }

    public static PermittedActivities Get(Role role) => role switch
    {
        Role.Student => studentActivities,
        Role.Clerk => clerkActivities,
        _ => throw new ArgumentException()
    };

    private static PermittedActivities studentActivities
        = new(Role.Student, new List<TypeCommand>
        {
            TypeCommand.GetStudents,
            TypeCommand.GetStudent,
            TypeCommand.GetGroup,
            TypeCommand.Login,
            TypeCommand.Logout,
            TypeCommand.Whoami,
            TypeCommand.GetAvailableCommands
        });

    private static PermittedActivities clerkActivities
        = new(Role.Clerk, new List<TypeCommand>
        {
            TypeCommand.AddGroup,
            TypeCommand.GetGroup,
            TypeCommand.GetAllGroups,
            TypeCommand.UpdateGroup,
            TypeCommand.DeleteGroup,
            TypeCommand.AddStudent,
            TypeCommand.DeleteStudent,
            TypeCommand.UpdateStudent,
            TypeCommand.GetStudents,
            TypeCommand.GetStudent,
            TypeCommand.Login,
            TypeCommand.Logout,
            TypeCommand.Whoami,
            TypeCommand.GetAvailableCommands
        });
}
