using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.VoiceEngine;

public class VoiceContext
{

    public ContextType Stage { get; set; } = ContextType.Start;

    public string Country { get; set; }

    public enum ContextType
    {

        Start,
        Help,
        StageCountrySuccess,
        StageCountryError

    }

}
