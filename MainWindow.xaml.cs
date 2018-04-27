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

namespace Angel_Problem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 800; i += 15)
            {
                for (int j = 0; j < 600; j += 15)
                {
                    Polygon square = new Polygon();
                    Point firstPoint = new System.Windows.Point(i, j);
                    Point secondPoint = new System.Windows.Point(i, j + 15);
                    Point thirdPoint= new System.Windows.Point(i + 15, j);
                    Point fourthPoint = new System.Windows.Point(i + 15, j + 15);
                    PointCollection squarePointCollection = new PointCollection();
                    squarePointCollection.Add(firstPoint);
                    squarePointCollection.Add(thirdPoint);
                    squarePointCollection.Add(fourthPoint);
                    squarePointCollection.Add(secondPoint);
                    square.Points = squarePointCollection;
                    square.Stroke = Brushes.Black;
                    square.Fill = Brushes.White;
                    Table.Children.Add(square);
                }
            }
        }

        private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AngelProblem.Title = DateTime.Now.ToLongTimeString();
        }
    }
}
