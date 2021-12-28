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

namespace FinalTest
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        int chineseScore = 0;
        int mathScore = 0;
        int englishScore = 0;
        double total = 0;
        public Window1()
        {
            InitializeComponent();

            

        }

        private void slider_changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void cal_Click(object sender, RoutedEventArgs e)
        {
            chineseScore = Convert.ToInt32(chineseSlider.Value);
            mathScore = Convert.ToInt32(mathSlider.Value);
            englishScore = Convert.ToInt32(englishSlider.Value);
            total = (chineseScore * 0.5) + (mathScore * 0.3 + englishScore * 0.2);

            tb.Text = $"國文成績 {chineseScore}\n數學成績 {mathScore}\n英文成績 {englishScore}\n總分 {total}";

            if (total >= 60){
                tb.Background = Brushes.PaleGreen;
            }
            else {
                tb.Background = Brushes.Pink;
            }
        }
    }
}
