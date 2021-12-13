using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TheNoteBook
{
    /// <summary>
    /// DocumentViewerWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DocumentViewerWindow : Window
    {
        public DocumentViewerWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e){
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RTF檔案(*.rtf)|*.rtf|全部檔案(*.*)|*.*";
            if(dlg.ShowDialog() == true){
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }

        }

        private void Save_Executed(object sender, RoutedEventArgs e){
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RTF檔案(*.rtf)|*.rtf|全部檔案(*.*)|*.*";
            if(dlg.ShowDialog() == true){
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e){
            if (cmbFontFamily.SelectedItem != null) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e){
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
        }        
    }
}
