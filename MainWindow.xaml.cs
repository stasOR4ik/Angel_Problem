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
            for (int i = 0; i <= 1290; i += 15)
            {
                for (int j = 0; j <= 1290; j += 15)
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
                    if (Math.Abs(43 - i / 15) < 43 && Math.Abs(43 - j / 15) < 43)
                    {
                        square.Square.Fill = Brushes.White;
                    }
                    else
                    {
                        square.Square.Fill = Brushes.Yellow;
                    }
                    square.CellCondition = CellCondition.EMPTY;
                    Board.Feild[i / 15, j / 15] = square;
                    Table.Children.Add(square.Square);
                }
            }
        }

        private void SetAngelStartPoint()
        {
            Board.Feild[43, 43].Square.Fill = Brushes.Green;
            Board.Feild[43, 43].CellCondition = CellCondition.ANGEL;
            Angel.AngelPositionX = 43;
            Angel.AngelPositionY = 43;
            Devil.MoveCalculator = 1;
        }

        private void SetNewAngelSquare(MouseButtonEventArgs e)
        {
            int getMouseAngelPositionX = (int)(e.GetPosition(Table).X / 15);
            int getMouseAngelPositionY = (int)(e.GetPosition(Table).Y / 15);
            if (Board.Feild[getMouseAngelPositionX, getMouseAngelPositionY].CellCondition == CellCondition.EMPTY &&
                Math.Abs(Angel.AngelPositionX - getMouseAngelPositionX) <= 1 && 
                Math.Abs(Angel.AngelPositionY - getMouseAngelPositionY) <= 1)
            {
                Board.Feild[Angel.AngelPositionX, Angel.AngelPositionY].CellCondition = CellCondition.EMPTY;
                Board.Feild[Angel.AngelPositionX, Angel.AngelPositionY].Square.Fill = Brushes.White;
                Board.Feild[getMouseAngelPositionX, getMouseAngelPositionY].CellCondition = CellCondition.ANGEL;
                Board.Feild[getMouseAngelPositionX, getMouseAngelPositionY].Square.Fill = Brushes.Green;
                (int, int) previosAngelProjectionForDistance5 = NearestPointToTheBorder(5); // for if in 125 line 
                (int, int) previosAngelProjectionForDistance4 = NearestPointToTheBorder(4); // for if in 130 line
                (int, int) previosAngelProjectionForDistance3 = NearestPointToTheBorder(3);
                (int, int) previosAngelProjectionForDistance2 = NearestPointToTheBorder(2);
                bool IsPreviosAngelDistance4 = IsAngelNeareBorder(4);
                bool IsPreviosAngelDistance3 = IsAngelNeareBorder(3);
                bool IsPreviosAngelDistance2 = IsAngelNeareBorder(2);
                Angel.AngelPositionX = getMouseAngelPositionX;
                Angel.AngelPositionY = getMouseAngelPositionY;
                if (Devil.MoveCalculator < 11)
                {
                    SwitchRow(Devil.MoveCalculator, 5);
                }
                else if (Devil.MoveCalculator < 21)
                {
                    SwitchRow(Devil.MoveCalculator - 10, 81);
                }
                else if (Devil.MoveCalculator < 29)
                {
                    SwitchColumn(5, Devil.MoveCalculator - 20);
                }
                else if (Devil.MoveCalculator < 37)
                {
                    SwitchColumn(81, Devil.MoveCalculator - 28);
                }
                else if (IsAngelNeareBorder(1))
                {
                    if (NearestPointToTheBorder(1).Item1 == 1 || NearestPointToTheBorder(1).Item1 == 85)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (Board.Feild[NearestPointToTheBorder(1).Item1, NearestPointToTheBorder(1).Item2 + i].CellCondition == CellCondition.EMPTY)
                            {
                                SetDevilSquare((NearestPointToTheBorder(1).Item1, NearestPointToTheBorder(1).Item2 + i));
                                break;
                            }
                        }
                    }
                    else if (NearestPointToTheBorder(1).Item2 == 1 || NearestPointToTheBorder(1).Item2 == 85)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (Board.Feild[NearestPointToTheBorder(1).Item1 + i, NearestPointToTheBorder(1).Item2].CellCondition == CellCondition.EMPTY)
                            {
                                SetDevilSquare((NearestPointToTheBorder(1).Item1 + i, NearestPointToTheBorder(1).Item2));
                                break;
                            }
                        }
                    }
                }
                else if (IsAngelNeareBorder(2))
                {
                    if (NearestPointToTheBorder(2).Item1 == 1 || NearestPointToTheBorder(2).Item1 == 85)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (Board.Feild[NearestPointToTheBorder(2).Item1, NearestPointToTheBorder(2).Item2 + i].CellCondition == CellCondition.EMPTY)
                            {
                                SetDevilSquare((NearestPointToTheBorder(2).Item1, NearestPointToTheBorder(2).Item2 + i));
                                break;
                            }
                        }
                    }
                    else if (NearestPointToTheBorder(2).Item2 == 1 || NearestPointToTheBorder(2).Item2 == 85)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (Board.Feild[NearestPointToTheBorder(2).Item1 + i, NearestPointToTheBorder(2).Item2].CellCondition == CellCondition.EMPTY)
                            {
                                SetDevilSquare((NearestPointToTheBorder(2).Item1 + i, NearestPointToTheBorder(2).Item2));
                                break;
                            }
                        }
                    }
                }
                else if (IsAngelNeareBorder(3))
                {
                    (int, int) tempPoint;
                    if (IsPreviosAngelDistance2)
                    {
                        tempPoint = LeftOrRightPointFromAngel(previosAngelProjectionForDistance2, NearestPointToTheBorder(3));
                    }
                    else if (IsPreviosAngelDistance3)
                    {
                        tempPoint = LeftOrRightPointFromAngel(previosAngelProjectionForDistance3, NearestPointToTheBorder(3));
                    }
                    else
                    {
                        tempPoint = LeftOrRightPointFromAngel(previosAngelProjectionForDistance4, NearestPointToTheBorder(3));
                    }
                    if (previosAngelProjectionForDistance4.Item1 == NearestPointToTheBorder(3).Item1 &&
                        previosAngelProjectionForDistance4.Item2 == NearestPointToTheBorder(3).Item2)
                    {
                        if (tempPoint.Item1 == 1 || tempPoint.Item1 == 85)
                        {
                            tempPoint.Item2 += 2;
                        }
                        else
                        {
                            tempPoint.Item1 += 2;
                        }
                        if (Board.Feild[tempPoint.Item1, tempPoint.Item2].CellCondition == CellCondition.DEVIL)
                        {
                            tempPoint = previosAngelProjectionForDistance4;
                        }
                    }
                    else if (NearestPointToTheBorder(3).Item1 == previosAngelProjectionForDistance4.Item1 &&
                        NearestPointToTheBorder(3).Item2 == previosAngelProjectionForDistance4.Item2)
                    {
                        tempPoint = previosAngelProjectionForDistance4;
                    }
                    else if (Board.Feild[NearestPointToTheBorder(3).Item1, NearestPointToTheBorder(3).Item2].CellCondition == CellCondition.DEVIL)
                    {
                        if (NearestPointToTheBorder(3).Item1 == 1 || NearestPointToTheBorder(3).Item1 == 85)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (IsPreviosAngelDistance2)
                                {
                                    if (Board.Feild[NearestPointToTheBorder(3).Item1,
                                    NearestPointToTheBorder(3).Item2 + i *
                                    AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance2, NearestPointToTheBorder(3))].CellCondition == CellCondition.EMPTY)
                                    {
                                        tempPoint = (NearestPointToTheBorder(3).Item1,
                                            NearestPointToTheBorder(3).Item2 + i * AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance2, NearestPointToTheBorder(3)));
                                        break;
                                    }
                                }
                                else
                                {
                                    if (Board.Feild[NearestPointToTheBorder(3).Item1,
                                    NearestPointToTheBorder(3).Item2 + i * AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance4, NearestPointToTheBorder(3))].CellCondition == CellCondition.EMPTY)
                                    {
                                        tempPoint = (NearestPointToTheBorder(3).Item1, NearestPointToTheBorder(3).Item2 + i * AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance4, NearestPointToTheBorder(3)));
                                        break;
                                    }
                                }
                            }
                        }
                        else if (NearestPointToTheBorder(3).Item2 == 1 || NearestPointToTheBorder(3).Item2 == 85) //error with left boder
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (Board.Feild[NearestPointToTheBorder(3).Item1 + i *
                                    AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance2, NearestPointToTheBorder(3)), NearestPointToTheBorder(3).Item2].CellCondition == CellCondition.EMPTY)
                                {
                                    tempPoint = (NearestPointToTheBorder(3).Item1 + i * AngelLeftOrRightRelativeToPreviousMove(previosAngelProjectionForDistance2, NearestPointToTheBorder(3)),
                                        NearestPointToTheBorder(3).Item2);
                                    break;
                                }
                            }
                        }
                    }
                    SetDevilSquare(tempPoint);
                }
                else if (IsAngelNeareBorder(4))
                {
                    if (IsPreviosAngelDistance3)
                    {
                        SetDevilSquare(LeftOrRightPointFromAngel(previosAngelProjectionForDistance3, NearestPointToTheBorder(4)));
                    }
                    else if (Angel.AngelPositionY == 6 && (Angel.AngelPositionX == 5 || Angel.AngelPositionX == 81))
                    {
                        SetDevilSquare((NearestPointToTheBorder(4).Item1, NearestPointToTheBorder(4).Item2 + 1));
                    }
                    else
                    {
                        SetDevilSquare(LeftOrRightPointFromAngel(previosAngelProjectionForDistance5, NearestPointToTheBorder(4)));
                    }
                }
                else if (IsAngelNeareBorder(5))
                {
                    SetDevilSquare(NearestPointToTheBorder(5));
                }
                Devil.MoveCalculator++;
            }
        }

        private void SetDevilSquare((int, int) square)
        {
            Board.Feild[square.Item1, square.Item2].CellCondition = CellCondition.DEVIL;
            Board.Feild[square.Item1, square.Item2].Square.Fill = Brushes.Red;
        }

        private void SwitchRow(int X, int constY)
        {
            switch (X)
            {
                case int x when X <= 5:
                    Board.Feild[X, constY].CellCondition = CellCondition.DEVIL;
                    Board.Feild[X, constY].Square.Fill = Brushes.Red;
                    break;
                case int x when X >= 6:
                    Board.Feild[X + 75, constY].CellCondition = CellCondition.DEVIL;
                    Board.Feild[X + 75, constY].Square.Fill = Brushes.Red;
                    break;
            }
        }

        private void SwitchColumn(int constX, int Y)
        {
            switch (Y)
            {
                case int x when Y <= 4:
                    Board.Feild[constX, Y].CellCondition = CellCondition.DEVIL;
                    Board.Feild[constX, Y].Square.Fill = Brushes.Red;
                    break;
                case int x when Y >= 5:
                    Board.Feild[constX, Y + 77].CellCondition = CellCondition.DEVIL;
                    Board.Feild[constX, Y + 77].Square.Fill = Brushes.Red;
                    break;
            }
        }

        private int AngelLeftOrRightRelativeToPreviousMove((int, int) previousPoint, (int, int) nearestPointToTheBoder)
        {
            if (previousPoint.Item1 - nearestPointToTheBoder.Item1 == 1)
            {
                return 1;
            }
            else if (previousPoint.Item1 - nearestPointToTheBoder.Item1 == -1)
            {
                return -1;
            }
            else if (previousPoint.Item2 - nearestPointToTheBoder.Item2 == 1)
            {
                return 1;
            }
            else if (previousPoint.Item2 - nearestPointToTheBoder.Item2 == -1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private (int, int) NearestPointToTheBorder(int distanceFromBoder)
        {
            if (!IsAngelNeareBorder(4) || distanceFromBoder != 5)
            {
                if (Math.Abs(Angel.AngelPositionX - 1) == distanceFromBoder)
                {
                    return (1, Angel.AngelPositionY);
                }
                else if (Math.Abs(Angel.AngelPositionX - 85) == distanceFromBoder)
                {
                    return (85, Angel.AngelPositionY);
                }
                else if (Math.Abs(Angel.AngelPositionY - 1) == distanceFromBoder)
                {
                    return (Angel.AngelPositionX, 1);
                }
                else
                {
                    return (Angel.AngelPositionX, 85);
                }
            }
            else
            {
               return NearestPointToTheBorder(distanceFromBoder - 1);
            }
        }

        private bool IsAngelNeareBorder(int distance)
        {
            if (Math.Abs(Angel.AngelPositionX - 1) == distance || Math.Abs(Angel.AngelPositionX - 85) == distance ||
                Math.Abs(Angel.AngelPositionY - 1) == distance || Math.Abs(Angel.AngelPositionY - 85) == distance)
            {
                return true;
            }
            return false;

        }

        private (int, int) LeftOrRightPointFromAngel ((int, int) previousPoint, (int, int) nearestPointToTheBoder)
        {
            if (previousPoint.Item1 - nearestPointToTheBoder.Item1 == 1)
            {
                return (nearestPointToTheBoder.Item1 - 1, nearestPointToTheBoder.Item2);
            }
            else if (previousPoint.Item1 - nearestPointToTheBoder.Item1 == -1)
            {
                return (nearestPointToTheBoder.Item1 + 1, nearestPointToTheBoder.Item2);
            }
            else if (previousPoint.Item2 - nearestPointToTheBoder.Item2 == 1)
            {
                return (nearestPointToTheBoder.Item1, nearestPointToTheBoder.Item2 - 1);
            }
            else if (previousPoint.Item2 - nearestPointToTheBoder.Item2 == -1)
            {
                return (nearestPointToTheBoder.Item1, nearestPointToTheBoder.Item2 + 1);
            }
            else
            {
                if (nearestPointToTheBoder.Item1 == 85 || nearestPointToTheBoder.Item1 == 1)
                {
                    return (nearestPointToTheBoder.Item1, nearestPointToTheBoder.Item2 - 1);
                }
                else
                {
                    if ((Angel.AngelPositionY == 5 || Angel.AngelPositionY == 81) && (Angel.AngelPositionX == 6 || Angel.AngelPositionX == 7))
                    {
                        return (nearestPointToTheBoder.Item1 + 1, nearestPointToTheBoder.Item2);
                    }
                    return (nearestPointToTheBoder.Item1 - 1, nearestPointToTheBoder.Item2);
                }
             
            }
        }
    }
}
