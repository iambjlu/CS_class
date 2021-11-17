using System;
using System.Windows;

namespace Trangle
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Double num1, num2, num3;
        public MainWindow()
        {
            InitializeComponent();



        }

        private void calculate(object sender, RoutedEventArgs e)
        {

            //catch (FormatException)
            num1 = Double.Parse(tb1.Text.ToString());
            num2 = Double.Parse(tb2.Text.ToString());
            num3 = Double.Parse(tb3.Text.ToString());
            Double longest = 0.0;
            Double other1 = 0.0;
            Double other2 = 0.0;

            if (num1 > num2 && num1 > num3)
            {
                longest = num1;
                other1 = num2;
                other2 = num3;
            }
            if (num2 > num1 && num2 > num3)
            {
                longest = num2;
                other1 = num1;
                other2 = num3;
            }
            if (num3 > num1 && num3 > num2)
            {
                longest = num3;
                other1 = num1;
                other2 = num2;
            }

            if ((num1 + num2) > longest && (num2 + num3) > longest && (num1 + num3) > longest)
            {
                //System.Windows.MessageBox.Show("這是三角形");
                //System.Windows.MessageBox.Show(longest.ToString());
                if ((longest * longest) == ((other1 * other1) + (other2 * other2)))
                {
                    System.Windows.MessageBox.Show("這是直角三角形");
                }
                else if ((longest * longest) > ((other1 * other1) + (other2 * other2)))
                {
                    System.Windows.MessageBox.Show("這是鈍角三角形");
                }
                else if ((longest * longest) < ((other1 * other1) + (other2 * other2)))
                {
                    System.Windows.MessageBox.Show("這是銳角三角形");
                }
                else;
            }
            else
            {
                System.Windows.MessageBox.Show("這不是三角形");
            }

        }
    }
}
