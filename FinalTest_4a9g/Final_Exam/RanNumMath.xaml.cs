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
using System.Windows.Shapes;

namespace Final_Exam
{
    /// <summary>
    /// Interaction logic for RanNumMath.xaml
    /// </summary>
    public partial class RanNumMath : Window
    {
        int total = 0;
        public RanNumMath()
        {
            InitializeComponent();
            Random rnd = new Random();
            int randnum = rnd.Next(1, 11);
            lb_randnum.Content = randnum;

            cb1.Content = randnum * 1;
            cb2.Content = randnum * 2;
            cb3.Content = randnum * 3;
            cb4.Content = randnum * 4;
            cb5.Content = randnum * 5;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            total = 0;
            if (cb1.IsChecked==true)
            {
                total += Convert.ToInt32(cb1.Content);
            }

            if (cb2.IsChecked == true)
            {
                total += Convert.ToInt32(cb2.Content);
            }

            if (cb3.IsChecked == true)
            {
                total += Convert.ToInt32(cb3.Content);
            }

            if (cb4.IsChecked == true)
            {
                total += Convert.ToInt32(cb4.Content);
            }

            if (cb5.IsChecked == true)
            {
                total += Convert.ToInt32(cb5.Content);
            }

            //checked_lb.Content = $"{}";
            total_lb.Content = total;
        }
    }
}
