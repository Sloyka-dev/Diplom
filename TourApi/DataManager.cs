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

}
