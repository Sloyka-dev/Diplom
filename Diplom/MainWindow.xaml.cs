using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Diplom.VoiceEngine;

namespace Diplom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(async () =>
            {

                await Task.Delay(1000);
                await VoiceManager.TextToSpeechAsync("В этом кратком руководстве для синтеза короткого блока введенного вами текста используется операция SpeakTextAsync. Вы также можете использовать длинный текст из файла и получить более точное управление стилями голоса, просодией и другими параметрами.");

                await Task.Delay(3000);
                await VoiceManager.SpeechToTextAsync();

            });

        }
    }
}