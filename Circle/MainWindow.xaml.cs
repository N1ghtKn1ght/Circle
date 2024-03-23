using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Point = Circle.Helper.Point;

namespace Circle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach(var item in Enum.GetValues(typeof(Helper.Vector)))
            {
                Orientation.Items.Add(item.ToString());
            }
        }

        private void DrawCoordinates(Point[] points)
        {
            Coordinates.Children.Clear();
            foreach (var point in points)
            {
                TextBlock tb = new TextBlock();
                tb.FontSize = 12;
                tb.Width = 100;
                tb.Text = $"({Math.Round(point.X, 1)}; {Math.Round(point.Y, 1)})";
                Coordinates.Children.Add(tb);
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 5;
                ellipse.Height = 5;
                ellipse.Fill = Brushes.Black;
                Coordinates.Children.Add(ellipse);

                float radius = (float)(Circle.Width / 2f);
                Point position = Helper.FindPoint(new Point() { X = 0, Y = 0 }, radius, point.Angle);
                Canvas.SetLeft(tb, position.X + (position.X < -2 ? -50 : 0));
                Canvas.SetTop(tb, -position.Y * 1.1);
                Canvas.SetLeft(ellipse, position.X);
                Canvas.SetTop(ellipse, -position.Y);
            }
        }

        private void NumbersDouble_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
                return;
            TextBox textBox = sender as TextBox;
            e.Handled = !(Char.IsDigit(e.Text, 0) || 
                (e.Text == "." && !textBox.Text.Contains(".") && textBox.Text.Length != 0) 
                || (e.Text == "-") && !textBox.Text.Contains("-") && textBox.Text.Length == 0);
        }
        private void NumbersInteger_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
                return;
            e.Handled = !Char.IsDigit(e.Text, 0);
        }

        private async void Calculating_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Container.Children)
            {
                if(item is TextBox text)
                {
                    if(string.IsNullOrEmpty(text.Text))
                    {
                        MessageBox.Show($"{text.Name} is empty");
                        return;
                    }
                }
            }
            Point[] points = new Point[0];
            float x1 = P1X.Text.ToFloat();
            float x2 = P2X.Text.ToFloat();
            float y1 = P1Y.Text.ToFloat();
            float y2 = P2Y.Text.ToFloat();
            float radius = Radius.Text.ToFloat();
            float degrees = Degrees.Text.ToFloat();
            Helper.Vector vector = (Helper.Vector)Orientation.SelectedIndex;
            int count = Count.Text.ToInteger();
            await Task.Run(() =>
            {
                try
                {
                    points = Helper.Calc(x1, x2,
                                         y1, y2,
                                         radius, degrees,
                                         vector, count);

                }
                catch (Exception ex)
                {
                    if (ex is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });

            DrawCoordinates(points);

        }
    }
}
