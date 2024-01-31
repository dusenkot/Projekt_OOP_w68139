using System;

class Lecture : Subject
{
    public override void DisplayDetails()
    {
        Console.WriteLine("Lecture Details:");
        base.DisplayDetails();
        // Additional properties and methods specific to Lecture
    }
}
