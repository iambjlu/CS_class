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
    /// Window2.xaml 的互動邏輯
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void MathButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.RadioButton;
            String currentMode = btn.Content.ToString();
            switch (currentMode){

                case "+":
                    Ans.Content = Convert.ToInt32(a.Text) + Convert.ToInt32(b.Text);
                    break;
                case "-":
                    Ans.Content = Convert.ToInt32(a.Text) - Convert.ToInt32(b.Text);
                    break;
                case "×":
                    Ans.Content = Convert.ToInt32(a.Text) * Convert.ToInt32(b.Text);
                    break;
                case "÷":
                    if (Convert.ToInt32(b.Text) != 0){
                        Ans.Content = Convert.ToInt32(a.Text) / Convert.ToInt32(b.Text);
                    }
                    else{
                        Ans.Content = "?";
                        System.Windows.MessageBox.Show("不可以除以0","警告", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                default:
                    return;

            }

        }
    }
}
