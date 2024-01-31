class GroupedSubject : Subject
{
    public List<Group> Groups { get; set; } = new List<Group>();


    public override void DisplayDetails()
    {
        Console.WriteLine("Grouped Subject Details:");
        base.DisplayDetails();
        Console.WriteLine($"Groups: {string.Join(", ", Groups.Select(g => g.GroupNumber))}");
    }
}
