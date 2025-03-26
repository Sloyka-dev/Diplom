namespace DataLib.Models;

public class Tour
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Region { get; set; }
    public int AdultCount { get; set; }
    public int ChildCount { get; set; }
    public decimal Cost { get; set; }
    public bool ForPeopleWithDisabilities { get; set; }

}
