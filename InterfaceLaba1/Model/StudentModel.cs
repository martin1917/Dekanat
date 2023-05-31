namespace InterfaceLaba1.Model;

public class StudentModel
{
    private static int ID = 0;

    public int Id { get; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }

    public StudentModel(string firstName, string secondName, string patronymic, DateTime birthday)
    {
        Id = ++ID;
        FirstName = firstName;
        SecondName = secondName;
        Patronymic = patronymic;
        Birthday = birthday;
    }

    public override string? ToString()
    {
        return $"Id: {Id}\n" +
            $"Имя: {FirstName}\n" +
            $"Фамилия: {SecondName}\n" +
            $"Отчество: {Patronymic}\n" +
            $"Дата рождения: {Birthday}";
    }
}