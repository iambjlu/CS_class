using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace Wpf_File
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Drink> drinks = new List<Drink>();  //宣告使用到Drink類別

        public MainWindow()
        {
            InitializeComponent();

                OpenFileDialog openFileDialog = new OpenFileDialog();  //開啟檔案
                openFileDialog.DefaultExt = "*.csv";
                openFileDialog.Filter = "CSV檔案|*.csv|所有檔案|*.*";

                if (openFileDialog.ShowDialog() == true)  //開啟檔案的視窗開著時
                {
                    string path = openFileDialog.FileName;

                    StreamReader sr = new StreamReader(path, Encoding.Default);
                    CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture);
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            Drink d = new Drink()
                            {
                                Name = csv.GetField("Name"),
                                Size = csv.GetField("Size"),
                                Price = csv.GetField<int>("Price")
                            };
                            drinks.Add(d);
                        }
                    }

                    foreach (Drink d in drinks)  //把每一個項目印出寫在TextBlock
                    {
                        TextBlock1.Text += $"{d.Name}{d.Size}{d.Price}\n";
                    }
                }
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)  //點擊儲存檔案的按鈕以儲存txt檔案
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "文字檔案|*.txt|所有檔案|*.*";

                if (saveFileDialog.ShowDialog() == true){  //存檔視窗開著的話
                    string path = saveFileDialog.FileName;

                    if (File.Exists(path))  //如果檔案存在
                    {
                        File.WriteAllText(path, $"訂單時間：{DateTime.Now}\n{ TextBlock1.Text}");
                    }
                else  //如果檔案不在的話就在txt檔案第一行寫上"新訂單"三個字
                {
                    StreamWriter sw = File.CreateText(path);
                    sw.Write($"新訂單:\n{TextBlock1.Text}");  //把TextBlock的內容
                    sw.Close();
                    }

                }
            }
        }

    }