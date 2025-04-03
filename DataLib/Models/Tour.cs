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

    public static Dictionary<int, string> Regions = new Dictionary<int, string>
        {
            { 1, "Россия" },
            { 2, "Франция" },
            { 3, "Италия" },
            { 4, "Испания" },
            { 5, "Германия" },
            { 6, "Нидерланды" },
            { 7, "Швеция" },
            { 8, "Греция" },
            { 9, "Турция" },
            { 10, "Чехия" },
            { 11, "Португалия" },
            { 12, "Ирландия" },
            { 13, "Австрия" },
            { 14, "Бельгия" },
            { 15, "Словакия" },
            { 16, "Хорватия" },
            { 17, "Мальта" }
        };

}
