using System.Windows;
using Diplom.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace Diplom.VoiceEngine;

static internal class VoiceController
{

    public static void SuccessGetText(string Text)
    {

        switch (context.Stage)
        {

            case 0:

                if (FuzySearch.FuzzySearch("помощь", Text.ToLower()))
                {

                    //...
                    context.Stage = VoiceContext.ContextType.Help;
                    StartDialog();

                }
                else
                {

                    var res = FuzySearch.FuzzySearchList(new List<string>() { "турция", "египет" }, Text.ToLower());
                    if (res.Count != 0)
                    {

                        context.Country = res[0];
                        context.Stage = VoiceContext.ContextType.StageCountrySuccess;
                        StartDialog();
                        //...

                    }
                    else
                    {


                        context.Stage = VoiceContext.ContextType.StageCountryError;
                        StartDialog();

                    }
                }

                break;

        }

    }

    public static void SuccessReadText(string Text)
    {

        VoiceManager.SpeechToTextAsync();

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


            case VoiceContext.ContextType.Help:

                VoiceManager.TextToSpeechAsync(VoiceConst.HelpText);

                break;

            case VoiceContext.ContextType.StageCountrySuccess:

                VoiceManager.TextToSpeechAsync(string.Format(VoiceConst.CountrySuccessText, context.Country));

                break;

            case VoiceContext.ContextType.StageCountryError:

                VoiceManager.TextToSpeechAsync(VoiceConst.CountryErrorText);

                break;

        }

    }

}
