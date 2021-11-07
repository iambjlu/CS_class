using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Forms;
using System;
using System.Windows.Controls;
using RadioButton = System.Windows.Controls.RadioButton;
using System.Windows.Media.Imaging;
using System.IO;

namespace Wpf_Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Color currentStrokeColor = Colors.Red;
        Color currentFillColor = Colors.Yellow;
        Color currentCanvasColor;
        Brush currentFillBrush = new SolidColorBrush(Colors.Yellow);
        int currentStrokeThickness = 1;
        Brush currentStrokeBrush = new SolidColorBrush(Colors.Red);
        Brush currentCanvasBrush;
        string currentShape = "Line";
        string currentAction = "Draw";
        Point Ps, Pd; // 多邊形的起點與終點
        Point start, dest;
        bool first = true; // 判斷多邊形是不是第一個邊
        public MainWindow()
        {
            InitializeComponent();
        }

        private void myCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            switch (currentAction)
            {
                case "Draw":
                    //顯示目前滑鼠游標，如果滑鼠左鍵一直按著就取終點座標
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        myCanvas.Cursor = System.Windows.Input.Cursors.Pen;
                        dest = e.GetPosition(myCanvas);

                        myLabel.Content = $"座標點:({start}) - ({dest})";
                        if (currentShape == "FreeLine")
                        {
                            DrawFreeLine();
                        }
                    }
                    break;
                case "Erase":
                    //將滑鼠遇到物件移除
                    Shape selectedShape = e.OriginalSource as Shape;
                    myCanvas.Children.Remove(selectedShape);
                    if (myCanvas.Children.Count == 0)
                    {
                        myCanvas.Cursor = System.Windows.Input.Cursors.Arrow;
                    }
                    break;
                default:
                    return;

            }
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //判斷每次滑鼠左鍵點擊時是要畫什麼
            switch (currentShape)
            {
                case "Line":
                    first = true;
                    DrawLine();
                    break;
                case "Rectangle":
                    first = true;
                    DrawRectangle();
                    break;
                case "Ellipse":
                    first = true;
                    DrawEllipse();
                    break;
                case "Polygon":
                    DrawPolygon();
                    break;
                case "FreeLine":
                    first = true;
                    DrawFreeLine();
                    break;
                default:
                    return;
            }
            //回復原本的滑鼠游標
            myCanvas.Cursor = System.Windows.Input.Cursors.Arrow;

        }

        //double rend = start;
        private void DrawPolygon() // 畫多邊形
        {
            Line myLine = new Line();
            myLine.Stroke = currentStrokeBrush;
            myLine.StrokeThickness = currentStrokeThickness;
            if (first)//畫多邊形的第一個邊
            {
                Ps.X = start.X;
                Ps.Y = start.Y;
                Pd.X = start.X;
                Pd.Y = start.Y;
                first = false;
            }

            myLine.X1 = Ps.X;
            myLine.Y1 = Ps.Y;
            //如果把線接到起點
            if (dest.X < Pd.X + 15 && dest.X > Pd.X - 15 && dest.Y < Pd.Y + 15 && dest.Y > dest.Y - 15)
            {
                //System.Windows.MessageBox.Show("2");
                myLabel.Content = "繪製多邊形完成";
                myLine.X2 = Pd.X;
                myLine.Y2 = Pd.Y;
                first = true;
            }
            else
            {
                myLine.X2 = dest.X;
                myLine.Y2 = dest.Y;
                Ps.X = dest.X;
                Ps.Y = dest.Y;
            }
            myCanvas.Children.Add(myLine);


        }

        private void DrawFreeLine()//畫曲線
        {
            DrawLine();
            start = dest;
        }

        private void DrawEllipse()//畫圓形
        {
            AdjustPoint();
            double width = dest.X - start.X;
            double heigth = dest.Y - start.Y;
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = currentStrokeBrush,
                StrokeThickness = currentStrokeThickness,
                Fill = currentFillBrush,
                Width = width,
                Height = heigth
            };
            newEllipse.SetValue(Canvas.LeftProperty, start.X);
            newEllipse.SetValue(Canvas.TopProperty, start.Y);
            myCanvas.Children.Add(newEllipse);
        }

        private void DrawRectangle()//畫矩形
        {
            AdjustPoint();
            double width = dest.X - start.X;
            double heigth = dest.Y - start.Y;
            Rectangle newRectangle = new Rectangle()
            {
                Stroke = currentStrokeBrush,
                StrokeThickness = currentStrokeThickness,
                Fill = currentFillBrush,
                Width = width,
                Height = heigth
            };
            newRectangle.SetValue(Canvas.LeftProperty, start.X);
            newRectangle.SetValue(Canvas.TopProperty, start.Y);
            myCanvas.Children.Add(newRectangle);
        }

        private void AdjustPoint()
        {
            double X_min, Y_min, X_max, Y_max;

            X_min = Math.Min(start.X, dest.X);
            Y_min = Math.Min(start.Y, dest.Y);
            X_max = Math.Max(start.X, dest.X);
            Y_max = Math.Max(start.Y, dest.Y);

            start.X = X_min;
            start.Y = Y_min;
            dest.X = X_max;
            dest.Y = Y_max;
        }

        private void DrawLine()//畫直線
        {
            Line myLine = new Line();

            myLine.Stroke = currentStrokeBrush;
            myLine.StrokeThickness = currentStrokeThickness;
            myLine.X1 = start.X;
            myLine.Y1 = start.Y;
            myLine.X2 = dest.X;
            myLine.Y2 = dest.Y;

            myCanvas.Children.Add(myLine);

        }

        private void StrokeButton_Click(object sender, RoutedEventArgs e)//線條顏色
        {
            currentStrokeColor = GetDialogColor();
            currentStrokeBrush = new SolidColorBrush(currentStrokeColor);
            StrokeButton.Background = currentStrokeBrush;
        }

        private Color GetDialogColor()//呼叫選色彩
        {
            ColorDialog dlg = new ColorDialog();
            dlg.ShowDialog();
            System.Drawing.Color returnColor = dlg.Color;
            return Color.FromArgb(returnColor.A, returnColor.R, returnColor.G, returnColor.B);

        }

        private void StrokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //當調整線條粗細的Slider的值改變
            currentStrokeThickness = Convert.ToInt32(StrokeThicknessSlider.Value);

        }


        private void ShapeButton_Click(object sender, RoutedEventArgs e)//選擇要畫什麼形狀
        {
            var btn = sender as System.Windows.Controls.RadioButton;
            //RadioButton btn = sender as RadioButton;
            currentShape = btn.Content.ToString();
            //myCanvas.Cursor = System.Windows.Input.Cursors.Arrow;
            currentAction = "Draw";
            myLabel.Content = $"畫{currentShape}";
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)//填滿顏色
        {

            currentFillColor = GetDialogColor();
            currentFillBrush = new SolidColorBrush(currentFillColor);
            FillButton.Background = currentFillBrush;
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)//橡皮擦模式
        {
            currentAction = "Erase";
            if (myCanvas.Children.Count != 0)
            {
                myCanvas.Cursor = System.Windows.Input.Cursors.Hand;
                myLabel.Content = "橡皮擦模式";
            }

        }

        private void menuCheckBox_Checked(object sender, RoutedEventArgs e)//選單的確定是否顯示工作列
        {
            //System.Windows.MessageBox.Show("1");
            if (menuCheckBox.IsChecked == true)
            {
                CanvasmenuCheckBox.IsChecked = true;
                // System.Windows.MessageBox.Show("2");
                myToolBarTray.Visibility = Visibility.Visible;
            }
            else
            {
                CanvasmenuCheckBox.IsChecked = false;
                //System.Windows.MessageBox.Show("3");
                myToolBarTray.Visibility = Visibility.Collapsed;
            }
        }

        private void SaveCanvas_Click(object sender, RoutedEventArgs e)//存檔畫布為png、jpg檔
        {
            int w = Convert.ToInt32(myCanvas.RenderSize.Width);
            int h = Convert.ToInt32(myCanvas.RenderSize.Height);

            RenderTargetBitmap rtb = new RenderTargetBitmap(w, h, 64d, 64d, PixelFormats.Default);
            rtb.Render(myCanvas);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "儲存小畫家檔案";
            saveFileDialog.DefaultExt = "*.png|*.jpg";
            saveFileDialog.Filter = "PNG檔案(*.png)|*.png|JPG檔案(*.jpg)|*.jpg|全部檔案|*.*";

            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;
            //未加防呆
            using (var fs = File.Create(path))
            {
                png.Save(fs);
            }
        }

        //當選單的Slider數值改變時
        private void menuSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentStrokeThickness = Convert.ToInt32(menuSlider.Value);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)//清除全部按鈕
        {
            first = true;
            myCanvas.Children.Clear();
            myCanvas.Cursor = System.Windows.Input.Cursors.Arrow;
            myLabel.Content = "就緒";
        }

        private void CanvasColorButton_Click(object sender, RoutedEventArgs e)
        {
            currentCanvasColor = GetDialogColor();
            currentCanvasBrush = new SolidColorBrush(currentCanvasColor);
            CanvasColorButton.Background = currentCanvasBrush;
            myCanvas.Background = currentCanvasBrush;
        }

        private void myCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CanvasmenuCheckBox_Checked(object sender, RoutedEventArgs e)//工作列隱藏/顯示
        {
            //System.Windows.MessageBox.Show("1");
            if (CanvasmenuCheckBox.IsChecked == true)
            {
                menuCheckBox.IsChecked = true;
                //System.Windows.MessageBox.Show("2");
                myToolBarTray.Visibility = Visibility.Visible;
            }
            else
            {
                menuCheckBox.IsChecked = false;
                //System.Windows.MessageBox.Show("3");
                myToolBarTray.Visibility = Visibility.Collapsed;
            }
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//左下角座標、功能顯示
        {
            myCanvas.Cursor = System.Windows.Input.Cursors.Cross; // ~!~
            start = e.GetPosition(myCanvas);
            myLabel.Content = $"座標點:({start}) - ({dest})";
        }


        //-----客製功能表開始-----
        private void About_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("小畫家\n版本1.0\nCopyright © 小畫家作者\n保留一切權利。", "關於小畫家", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void bye(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(881);
        }
        //-----客製功能表結束-----



    }
}
