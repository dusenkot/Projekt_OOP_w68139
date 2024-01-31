class Group
{
    public int GroupNumber { get; set; }
    public StudyDirection Direction { get; set; }

    public Group(int groupNumber, StudyDirection direction)
    {
        GroupNumber = groupNumber;
        Direction = direction;
    }
}
