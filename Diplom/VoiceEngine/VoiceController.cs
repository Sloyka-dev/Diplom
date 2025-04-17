using System.Diagnostics;
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
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.HelpList, Text))
        {

            context.Stage = VoiceContext.ContextType.Help;
            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.RegionList, Text))
        {

            context.Stage = VoiceContext.ContextType.Regions;
            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.OrderList, Text))
        {

            OrderTour(context.CurrentTour);
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.RepeatList, Text))
        {

            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.NextList, Text))
        {

            if (context.CurrentItemNum + 1 < context.SearchResult.Count) 
                context.CurrentItemNum++;
            StartDialog();
            return;

        }

        if (FuzySearch.FuzzySearchList(VoiceConst.PreviousList, Text))
        {

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

    public static void OrderTour(Tour tour)
    {

        context.Stage = VoiceContext.ContextType.Order;
        new HelpWindow(VoiceConst.HelpWindowTourText, tour.Name);
        Process.Start(new ProcessStartInfo(tour.URL) { UseShellExecute = true });
        StartDialog();

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
        context.Stage = tours.Count == 0 ? VoiceContext.ContextType.SearchError : VoiceContext.ContextType.SearchFirstSuccess;
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

        VoiceManager.StopSpeak();
        switch (context.Stage)
        {

            case VoiceContext.ContextType.Start:

                VoiceManager.TextToSpeechAsync(VoiceConst.WelcomeText);

                break;

            case VoiceContext.ContextType.Help:

                VoiceManager.TextToSpeechAsync(VoiceConst.HelpText);

                break;

            case VoiceContext.ContextType.Regions:

                VoiceManager.TextToSpeechAsync(VoiceConst.RegionListText);

                break;

            case VoiceContext.ContextType.SearchFirstSuccess:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.SearchSuccessFirstText, context.SearchResult.Count));

                break;

            case VoiceContext.ContextType.SearchSuccess:

                var current = context.CurrentTour;
                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.SearchSuccessText, 
                    current.Name, 
                    current.Description,
                    Tour.Regions[current.Region],
                    current.Cost));

                break;

            case VoiceContext.ContextType.SearchError:

                VoiceManager.TextToSpeechAsync(VoiceConst.SearchErrorText);

                break;

            case VoiceContext.ContextType.Order:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.HelpWindowTourText, context.CurrentTour.Name));

                break;

        }

    }

}
