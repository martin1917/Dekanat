namespace InterfaceLaba1.Model;

public class GroupModel
{
    public string Name { get; set; }
    public int YearCreated { get; set; }
    public List<StudentModel> Students { get; set; } = new();

    public GroupModel(string name, int yearCreated)
    {
        Name = name;
        YearCreated = yearCreated;
    }

    public override string? ToString()
    {
        return $"Имя группы: {Name}\n" +
            $"Год образования: {YearCreated}\n" +
            $"Кол-во студентов: {Students.Count}";
    }
}
