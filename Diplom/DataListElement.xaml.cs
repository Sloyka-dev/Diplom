using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Diplom.VoiceEngine;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для DataListElement.xaml
    /// </summary>
    public partial class DataListElement : UserControl
    {

        private Tour tour;

        public DataListElement(Tour tour)
        {
            InitializeComponent();

            this.tour = tour;

            NameLabel.Content = tour.Name;
            DescLabel.Content = Tour.Regions[tour.Region];
            PriceLabel.Content = tour.Cost + "₽ ночь";

            MouseUp += DataListElement_MouseUp;

        }

        private void DataListElement_MouseUp(object sender, MouseButtonEventArgs e)
        {

            VoiceController.OrderTour(tour);

        }
    }
}
