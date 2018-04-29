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
            PrintGameField();
            SetAngelStartPoint();
        }

        private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetNewAngelSquare(e);
        }

        private void Table_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cell newAngelSquare = new Cell();
            newAngelSquare.Square = (Polygon)e.OriginalSource;
            newAngelSquare.Square.Fill = Brushes.Red;
            newAngelSquare.CellCondition = CellCondition.DEVIL;
        }

        private void PrintGameField()
        {
            for (int i = 0; i <= 1260; i += 15)
            {
                for (int j = 0; j <= 1260; j += 15)
                {
                    Cell square = new Cell();
                    Point firstPoint = new Point(i, j);
                    Point secondPoint = new Point(i, j + 15);
                    Point thirdPoint = new Point(i + 15, j);
                    Point fourthPoint = new Point(i + 15, j + 15);
                    PointCollection squarePointCollection = new PointCollection
                    {
                        firstPoint,
                        thirdPoint,
                        fourthPoint,
                        secondPoint
                    };
                    square.Square.Points = squarePointCollection;
                    square.Square.Stroke = Brushes.Black;
                    if (Math.Abs(42 - i / 15) < 42 && Math.Abs(42 - j / 15) < 42)
                    {
                        square.Square.Fill = Brushes.White;
                        square.CellCondition = CellCondition.EMPTY;
                    }
                    else
                    {
                        square.Square.Fill = Brushes.Red;
                        square.CellCondition = CellCondition.DEVIL;
                    }
                    Board.feild[i / 15, j / 15] = square;
                    Table.Children.Add(square.Square);
                }
            }
        }

        private void SetAngelStartPoint()
        {
            Board.feild[41, 41].Square.Fill = Brushes.Green;
            Board.feild[41, 41].CellCondition = CellCondition.ANGEL;
            Angel.AngelSquare = Board.feild[41, 41];
        }

        private void SetNewAngelSquare(MouseButtonEventArgs e)
        {
            Angel.AngelSquare.CellCondition = CellCondition.EMPTY;
            Angel.AngelSquare.Square.Fill = Brushes.White;
            Cell newAngelSquare = new Cell();
            newAngelSquare.Square = (Polygon)e.OriginalSource;
            newAngelSquare.Square.Fill = Brushes.Green;
            newAngelSquare.CellCondition = CellCondition.ANGEL;
            Angel.AngelSquare = newAngelSquare;
            Board.feild[1, 1].CellCondition = CellCondition.DEVIL;
        }
    }
}
