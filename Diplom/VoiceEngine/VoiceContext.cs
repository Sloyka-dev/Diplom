using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLib.Models;

namespace Diplom.VoiceEngine;

public class VoiceContext
{

    public ContextType Stage { get; set; } = ContextType.Start;

    public string SearchText { get; set; }
    public List<Tour> SearchResult { get; set; }
    public int CurrentItemNum { get; set; } = 0;
    public Tour CurrentTour { get => SearchResult[CurrentItemNum]; }

    public enum ContextType
    {

        Start,
        StartSum,
        Help,
        HelpSum,
        SearchFirstSuccess,
        SearchSuccess,
        SearchError

    }

}
