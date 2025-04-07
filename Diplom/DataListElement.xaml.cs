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
            DescLabel.Content = tour.Description;
            PriceLabel.Content = tour.Cost + "₽";

        }
    }
}
