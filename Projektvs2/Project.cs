using System;

class Project : Subject
{
    public int GroupNumber2 { get; set; }

    public override void DisplayDetails()
    {
        Console.WriteLine("Project Details:");
        base.DisplayDetails();
        Console.WriteLine($"Second Group Number: {GroupNumber2}");
        // Additional properties and methods specific to Project
    }
}
