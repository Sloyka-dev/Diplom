using System.Windows;
using DataLib.Models;
using Diplom.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace Diplom.VoiceEngine;

static internal class VoiceController
{

    public static void SuccessGetText(string Text)
    {

        if (FuzySearch.FuzzySearchList(VoiceConst.SkipList, Text))
        {

            VoiceManager.StopSpeak();

            switch (context.Stage)
            {

                case VoiceContext.ContextType.Start:
                    context.Stage = VoiceContext.ContextType.StartSum;
                    break;

                case VoiceContext.ContextType.Help:
                    context.Stage = VoiceContext.ContextType.HelpSum;
                    break;
                    
            }

            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.HelpList, Text))
        {

            context.Stage = VoiceContext.ContextType.Help;
            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.NextList, Text))
        {

            VoiceManager.StopSpeak();
            if (context.CurrentItemNum + 1 < context.SearchResult.Count) 
                context.CurrentItemNum++;
            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.PreviousList, Text))
        {

            VoiceManager.StopSpeak();
            if (context.CurrentItemNum > 0)
                context.CurrentItemNum--;
            StartDialog();
            return;

        }

        DeleteContext();
        context.SearchText = Text;
        MainWindow.Singleton.onVoiceSearch(Text);

        ApiHandler.GetToursAsync(Text);
        return;

    }

    public static void onGetTourError()
    {

        VoiceManager.StopSpeak();
        context.Stage = VoiceContext.ContextType.SearchError;
        StartDialog();

    }

    public static void onGetTourResult(List<Tour> tours)
    {

        VoiceManager.StopSpeak();
        context.SearchResult = tours;
        context.Stage = VoiceContext.ContextType.SearchFirstSuccess;
        StartDialog();

    }

    public static void SuccessReadText(string Text)
    {

        if (context.Stage == VoiceContext.ContextType.SearchFirstSuccess)
        {
        
            context.Stage = VoiceContext.ContextType.SearchSuccess;
            StartDialog();
        
        }

    }

    public static void Error(string ErrorCode, string ErrorDetails)
    {

#if !RELEASE
        MessageBox.Show(ErrorCode + "\n" + ErrorDetails);
#endif

    }

    public static void Cancel(string Reason)
    {

#if !RELEASE
        MessageBox.Show(Reason);
#endif

    }

    static VoiceContext context;

    public static VoiceContext Context { get { return context; } }

    public static void Init()
    {

        context = new VoiceContext();
        StartDialog();
        VoiceManager.SpeechToTextAsync();

    }

    static void DeleteContext()
    {
     
        context = new VoiceContext();

    }

    static void StartDialog()
    {

        switch (context.Stage)
        {

            case VoiceContext.ContextType.Start:

                VoiceManager.TextToSpeechAsync(VoiceConst.WelcomeText);

                break;

            case VoiceContext.ContextType.StartSum:

                VoiceManager.TextToSpeechAsync(VoiceConst.WelcomeTextSum);

                break;

            case VoiceContext.ContextType.Help:

                VoiceManager.TextToSpeechAsync(VoiceConst.HelpText);

                break;

            case VoiceContext.ContextType.HelpSum:

                VoiceManager.TextToSpeechAsync(VoiceConst.HelpTextSum);

                break;

            case VoiceContext.ContextType.SearchFirstSuccess:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.SearchSuccessFirstText, context.SearchResult.Count));

                break;

            case VoiceContext.ContextType.SearchSuccess:

                var current = context.CurrentTour;
                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.SearchSuccessText, 
                    current.Name, 
                    current.Description, 
                    current.AdultCount, 
                    current.ChildCount, 
                    current.Cost));

                break;

            case VoiceContext.ContextType.SearchError:

                VoiceManager.TextToSpeechAsync(VoiceConst.SearchErrorText);

                break;


        }

    }

}
