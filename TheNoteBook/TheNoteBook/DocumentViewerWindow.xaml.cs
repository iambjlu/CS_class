using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string FileName = "新文件"; // 儲存當前檔案名稱
        public DocumentViewerWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source); // 設定文字風格的Items
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 }; // 設定文字大小的Items 

            // 取得當前游標所在的行與列
            TextPointer p1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer p2 = rtbEditor.Selection.Start;
            int c = p1.GetOffsetToPosition(p2);
            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            lb1.Content = $"第{currentLineNumber + 1}列，第{c}行|{FileName}"; //更改狀態列
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) // 開啟舊的rtf檔
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files|*.*"; // 設定開檔的類型
            if (dlg.ShowDialog() == true) // 開啟檔案
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open); // 無法開剛剛做完的檔案，可能需要close
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }

            FileName = System.IO.Path.GetFileName(dlg.FileName); // 取得當前檔案名稱

            //取得當前游標的下標
            int rtbEditorLine = Regex.Split(rtbEditor.Selection.Text.ToString(), "\n").Length;
            TextPointer p1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer p2 = rtbEditor.Selection.Start;
            int c = p1.GetOffsetToPosition(p2);
            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            lb1.Content = $"第{currentLineNumber + 1}列，第{c}行|{FileName}"; // 更新狀態列
        }

        private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e) // 開一個新的檔案
        {
            DocumentViewerWindow viewerWindow = new DocumentViewerWindow(); //設定一個新的視窗
            viewerWindow.Show(); // 顯示新的視窗
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close(); // 關閉當前的視窗
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e) // 儲存檔案
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files|*.*"; // 設定存檔類型
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.CreateNew); // 無法儲存相同檔名
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) // 改變字體
        {
            if (cmbFontFamily.SelectedValue != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e) // 改變文字大小
        {
            if (cmbFontSize.SelectedValue != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, Convert.ToDouble(cmbFontSize.SelectedValue.ToString()));
            }

        }


        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e) // 游標位置改變時的事件
        {
            // 取得列與行的下標
            int rtbEditorLine = Regex.Split(rtbEditor.Selection.Text.ToString(), "\n").Length;
            TextPointer p1 = rtbEditor.Selection.Start.GetLineStartPosition(0);
            TextPointer p2 = rtbEditor.Selection.Start;
            int c = p1.GetOffsetToPosition(p2);
            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            rtbEditor.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            lb1.Content = $"第{currentLineNumber + 1}列，第{c}行|{FileName}"; // 更改狀態列

            var temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderLine.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.SelectedItem = temp;

        }



        private void ColorBtn_Click(object sender, RoutedEventArgs e) // 更改文字顏色
        {
            Color brc = GetDialogColor(); // 設定顏色
            BrushConverter brushes = new BrushConverter(); // 建立一個rtbEditor設定顏色需要的類別
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, brushes.ConvertFromString(Convert.ToString(brc))); //更改選中rtbEditor文字顏色
            SolidColorBrush Btbrush = new SolidColorBrush(brc);
            ColorBtn.Background = Btbrush; // 改變按鈕的背景顏色
        }
        private Color GetDialogColor() // 叫出調色盤回傳選中顏色
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.ShowDialog();
            System.Drawing.Color returnColor = dlg.Color;
            return Color.FromArgb(returnColor.A, returnColor.R, returnColor.G, returnColor.B);

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) // 關閉文字文件
        {
            rtbEditor.Document.Blocks.Clear();
        }

        private void BackColorBtn_Click(object sender, RoutedEventArgs e) // 改變背景顏色
        {
            Color brc = GetDialogColor(); // 設定顏色
            SolidColorBrush brush = new SolidColorBrush(brc); //設定成背景設定顏色的類別
            BackColorBtn.Background = brush; // 設定背景顏色
            rtbEditor.Background = brush; // 改變按鈕顏色
        }


        //TextBoxBase.SelectionChanged="cmbFontSize_SelectionChanged"
    }
}
