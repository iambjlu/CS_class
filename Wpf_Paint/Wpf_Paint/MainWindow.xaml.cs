using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Wpf_Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color currentStrokeColor = Colors.Red;
        Color currentFillColor = Colors.Yellow;
        Brush currentStrokeBrush = new SolidColorBrush(Colors.Red);
        Brush currentFillBrush = new SolidColorBrush(Colors.Yellow);
        int currentStrokeThickness = 1;
        string currentShape = "Line";
        string currentAction = "Draw";
        Point start, dest;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            myCanvas.Cursor = System.Windows.Input.Cursors.Cross;
            start = e.GetPosition(myCanvas);
            myLabel.Content = $"座標點:({start})-({dest})";

        }

        private void myCanvas_MouseMove(object sender,System.Windows.Input.MouseEventArgs e)
        {
            switch (currentAction) { 
                case "Draw":
                    //顯示目前滑鼠游標，如果滑鼠左鍵一直按著就取終點座標
                    if (e.LeftButton ==MouseButtonState.Pressed) {
                        myCanvas.Cursor = Cursors.Pen;
                        dest = e.GetPosition(myCanvas);
                        myLabel.Content = $"座標點:({start})-({dest})";
                    }
                    break;
                case "Erase":
                    //將滑鼠遇到的物件移除
                    {
                    }
                    break;
                default:
                    return;
            }
        }
        


        //-----客製功能表開始-----
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("小畫家\n版本1.0\nCopyright © 小畫家作者\n保留一切權利。", "關於小畫家", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void bye(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(881);
        }
        //-----客製功能表結束-----

        private void StrokeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveCanvas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuSlider_ValueChanged(object sender, RoutedEventArgs e)
        {

        }

    }
}
