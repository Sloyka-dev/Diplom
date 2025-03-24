using System.Windows;
using Diplom.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace Diplom.VoiceEngine;

static internal class VoiceController
{

    public static void SuccessGetText(string Text)
    {

        if (FuzySearch.FuzzySearchList(VoiceConst.SkipList, Text))
        {

            switch (context.Stage)
            {

                case VoiceContext.ContextType.Start:
                    context.Stage = VoiceContext.ContextType.StartSum;
                    break;

                case VoiceContext.ContextType.Help:
                    context.Stage = VoiceContext.ContextType.HelpSum;
                    break;

                case VoiceContext.ContextType.StageCountrySuccess:
                    context.Stage = VoiceContext.ContextType.StageCountrySuccessSum;
                    break;

                case VoiceContext.ContextType.StageCountryError:
                    context.Stage = VoiceContext.ContextType.StageCountryErrorSum;
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

        if (context.Stage == VoiceContext.ContextType.Start || context.Stage == VoiceContext.ContextType.StartSum)
        {

            var res = FuzySearch.FuzzySearchWordInList(VoiceConst.CountryList, Text);
            if(res != null)
            {

                context.Country = res;
                context.Stage = VoiceContext.ContextType.StageCountrySuccess;

            }
            else
            {

                context.Stage = VoiceContext.ContextType.StageCountryError;

            }

            StartDialog();
            return;

        }

    }

    public static void SuccessReadText(string Text)
    {


    }

    public static void Error(string ErrorCode, string ErrorDetails)
    {

        MessageBox.Show(ErrorCode + "\n" + ErrorDetails);

    }

    public static void Cancel(string Reason)
    {

        MessageBox.Show(Reason);

    }

    static VoiceContext context;

    public static void Init()
    {

        context = new VoiceContext();
        StartDialog();
        VoiceManager.SpeechToTextAsync();

    }

    static void DeleteContext()
    {
     
        context = new VoiceContext();
        StartDialog();

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

            case VoiceContext.ContextType.StageCountrySuccess:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.CountrySuccessText, context.Country));

                break;

            case VoiceContext.ContextType.StageCountrySuccessSum:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.CountrySuccessTextSum, context.Country));

                break;

            case VoiceContext.ContextType.StageCountryError:

                VoiceManager.TextToSpeechAsync(VoiceConst.CountryErrorText);

                break;

            case VoiceContext.ContextType.StageCountryErrorSum:

                VoiceManager.TextToSpeechAsync(VoiceConst.CountryErrorTextSum);

                break;

        }

    }

}
