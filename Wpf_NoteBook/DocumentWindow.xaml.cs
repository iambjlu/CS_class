using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NoteBook
{
    public partial class DocumentWindow : Window
    {
        public DocumentWindow()
        {
            InitializeComponent();
            EnumerateSystomFonts();
            EnumerateFontSize();
        }

        private Color TextColor;
        private Brush GetTextColor = new SolidColorBrush(Colors.Black);

        private void EnumerateSystomFonts()
        {
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                fontComboBox.Items.Add(fontFamily.Source);
            }    
        }

        private void EnumerateFontSize()
        {
            for (var i = 1; i < 72; i++)
            {
                fontComboBox2.Items.Add((double)i);
            }
        }

        private void Oepn_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "讀取文字檔案";
            openFileDialog.DefaultExt = ".rtf";
            openFileDialog.Filter = "RTF檔案(*.rtf)|*.rtf|全部檔案(*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                FileStream fs = new FileStream(path, FileMode.Open);
                TextRange range = new TextRange(richTextBox1.Document.ContentStart, richTextBox1.Document.ContentEnd);

                range.Load(fs, System.Windows.DataFormats.Rtf);
                 label3.Content = path;
            }
        }

        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Title = "儲存文字檔案";
            saveFileDialog.DefaultExt = "*.rtf";
            saveFileDialog.Filter = "帶格式文件(*.rtf)|*.rtf|全部檔案(*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                FileStream fs = new FileStream(path, FileMode.Create);
            }
        }

        private void New_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentWindow mywindow = new DocumentWindow();
            mywindow.Show();
        }

        private void Close_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentWindow mywindow = new DocumentWindow();
            this.Close();
        }

        private void Cut_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Copy_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            richTextBox1.Paste();
        }
        private void SelectAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void CancelPrint_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            richTextBox1.Selection.Text = "";
        }

        private void fontComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TextSelection selection = richTextBox1.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(FontFamilyProperty, fontComboBox.SelectedItem);
            }
        }

        private void fontComboBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TextSelection selection = richTextBox1.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(FontSizeProperty, fontComboBox2.SelectedItem);
            }
        }

        private Color GetDialogColor()
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.ShowDialog();

            System.Drawing.Color dlgColor = dlg.Color;
            return Color.FromArgb(dlgColor.A, dlgColor.R, dlgColor.G, dlgColor.B);
        }

        private void ColorChangeClick(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox1.Selection;

            TextColor = GetDialogColor();
            GetTextColor = new SolidColorBrush(TextColor);
            ColorBtn.Background = GetTextColor;
            selection.ApplyPropertyValue(ForegroundProperty, GetTextColor);
        }

        private void BackColorBtn_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox1.Selection;

            TextColor = GetDialogColor();
            GetTextColor = new SolidColorBrush(TextColor);
            BackColorBtn.Background = GetTextColor;
            richTextBox1.Background = GetTextColor;
        }

        private void richTextBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            label2.Content = richTextBox1.Selection.Text.Length + Environment.NewLine;
        }
    }
}
