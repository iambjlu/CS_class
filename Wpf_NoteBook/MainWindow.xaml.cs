using System.Windows;

namespace NoteBook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow mywindow = new DocumentWindow();
            mywindow.Show();
        }
    }
}
