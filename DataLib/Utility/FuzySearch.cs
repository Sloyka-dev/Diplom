using DataLib.Models;

namespace DataLib.Utility;

public class FuzySearch
{

    public static string? FuzzySearchWordInList(List<string> data, string searchTerm, int maxDistance = 3)
    {

        string? res = null;
        int min = maxDistance + 1;

        foreach (var item in data)
        {
            int distance = LevenshteinDistance(item, searchTerm);
            if (distance <= maxDistance)
            {
                
                if(distance < min)
                {

                    res = item;
                    min = distance;

                }

            }
        }

        return res;
    }

    public static bool FuzzySearchList(List<string> data, string searchTerm, int maxDistance = 3)
    {

        foreach (var item in data)
        {
            int distance = LevenshteinDistance(item, searchTerm);
            if (distance <= maxDistance)
            {
                return true;
            }
        }

        return false;
    }

    public static bool FuzzySearch(string data, string searchTerm, int maxDistance = 3)
    {
        int distance = LevenshteinDistance(data, searchTerm);
        return distance <= maxDistance;
    }

    public static List<Tour> SearchTours(List<Tour> tours, string text)
    {

        var list = new List<Tour>();
        var rating = new Dictionary<int, Tour>();

        foreach (var item in tours)
        {

            var desc = $"{item.Name} {item.Description}";
            var rItem = (LevenshteinDistanceText(Tour.Regions[item.Region], text) * 1_000_000) + (LevenshteinDistanceText(desc, text) * 1_000) + item.Id;
            rating.Add(rItem, item);

        }

        var srRes = rating.Keys.Average();
        foreach (var key in rating.Keys.Order())
            if (key > srRes && key >= 1_000)
                list.Add(rating[key]);

        return list;

    }

    public static int LevenshteinDistanceText(string s, string t)
    {

        string[] sText = s.Split(' ');
        string[] tText = t.Split(' ');

        int count = 0;

        foreach (var sItem in sText)
            foreach (var tItem in tText)
                if (LevenshteinDistance(sItem, tItem) <= 3)
                    count++;

        return count;

    }

    static int LevenshteinDistance(string s, string t)
    {

        //удалить знаки препинания из слов

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++)
            d[i, 0] = i;
        for (int j = 0; j <= m; j++)
            d[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }

}
