namespace DataLib.Models;

public class Tour
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Region { get; set; }
    public int Cost { get; set; }
    public bool ForPeopleWithDisabilities { get; set; }
    public string URL { get; set; }

    public static Dictionary<int, string> Regions = new Dictionary<int, string>
        {
            { 1, "Турция, Кемер" },
            { 2, "Турция, Алания" },
            { 3, "Турция, Анталья" },
            { 4, "Турция, Белек" },
            { 5, "Россия, Краснодарский край" },
            { 6, "Россия, Санкт-Петербург" },
            { 7, "Россия, Калининградская область" },
            { 8, "Беларусь, Минск" },
        };

}
