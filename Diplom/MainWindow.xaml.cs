using System.Collections.Generic;
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
using DataLib.Models;
using Diplom.Utility;
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

            VoiceController.Init();

            SupportButton.Click += SupportButton_Click;
            SearchButton.Click += SearchButton_Click;

            Singleton = this;

        }

        public static MainWindow Singleton;

        public void onTourApiResult(List<Tour> tours)
        {

            foreach (var item in tours)
            {

                var element = new DataListElement(item);
                DataListView.Items.Add(element);

            }

        }


        public void onVoiceSearch(string text)
        {

            SearchText.Text = text;
            DataListView.Items.Clear();

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            DataListView.Items.Clear();
            var res = ApiHandler.GetToursAsync(SearchText.Text);
                        
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {

            new HelpWindow();

        }
    }
}