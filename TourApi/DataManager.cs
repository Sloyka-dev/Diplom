using DataLib.Models;
using Newtonsoft.Json;

namespace TourApi;

public class DataManager
{

    static DataManager()
    {

        using (StreamReader r = new StreamReader("Data.json"))
        {
            string json = r.ReadToEnd();
            Tours = JsonConvert.DeserializeObject<List<Tour>>(json);
        }

    }

    public static List<Tour> Tours { get; set; }

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
