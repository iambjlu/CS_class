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
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        int chineseScore = 0;
        int mathScore = 0;
        int englishScore = 0;
        double total = 0;
       
        public Scores()
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

            tb.Text = $"國文: {chineseScore}\n數學: {mathScore}\n英文: {englishScore}\n總分: {total}";

            if (total >= 60)
            {
                tb.Background = Brushes.PaleGreen;
            }
            else
            {
                tb.Background = Brushes.Pink;
            }
        }
    }
}
