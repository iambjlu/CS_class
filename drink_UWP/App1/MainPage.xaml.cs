﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace App1
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Drink> drinks = new List<Drink>();
        List<OrderItem> order = new List<OrderItem>();
        string takeout;
        public MainPage()
        {
            this.InitializeComponent();
            InitializeComponent();

            //新增飲料品項至drinks清單
            drinks.Add(new Drink() { Name = "咖啡", Size = "大杯", Price = 60 });
            drinks.Add(new Drink() { Name = "咖啡", Size = "中杯", Price = 50 });
            drinks.Add(new Drink() { Name = "紅茶", Size = "大杯", Price = 30 });
            drinks.Add(new Drink() { Name = "紅茶", Size = "中杯", Price = 20 });
            drinks.Add(new Drink() { Name = "綠茶", Size = "大杯", Price = 30 });
            drinks.Add(new Drink() { Name = "綠茶", Size = "中杯", Price = 20 });

            //顯示所有飲料品項
            DisplayDrinks(drinks);

        }

        private void DisplayDrinks(List<Drink> myDrink)
        {
            foreach (Drink d in myDrink)
            {

                StackPanel sp = new StackPanel();  //動態產生StackPanel
                CheckBox cb = new CheckBox();
                //TextBox tb = new TextBox();
                Slider sl = new Slider();
                TextBlock lb = new TextBlock();

                cb.Content = d.Name + d.Size + d.Price;

                sl.Width = 100;
                sl.Value = 0;
                sl.Minimum = 0;
                sl.Maximum = 10;
                sl.TickPlacement = TickPlacement.BottomRight;

                lb.Width = 50;
                lb.Text = "";
                //tb.Width = 100;

                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);
                //sp.Children.Add(tb);

                drink_menu.Children.Add(sp);

            
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked == true) takeout = rb.Content.ToString();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            PlaceOrder(order);

            CompleteOrder(order, drinks);
        }

        private void CompleteOrder(List<OrderItem> myOrder, List<Drink> myDrink)
        {
            int total = 0;
            string message = "";
            int sellPrice = 0;

            TextBlock1.Text = "";
            TextBlock1.Text += $"您的飲料是{takeout}，訂購清單如下:\n";
            int j = 1;
            foreach (OrderItem oi in order)
            {
                total += oi.SubTotal;
                if (total >= 500)
                {
                    message = "訂購滿500元以上8折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.8));
                }
                else if (total <= 300)
                {
                    message = "訂購滿300元以上85折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.85));
                }
                else if (total <= 300)
                {
                    message = "訂購滿200元以上9折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.9));
                }
                else
                {
                    message = "訂購未滿200不打折";
                    sellPrice = total;
                }
                TextBlock1.Text += $"第{j}項:{drinks[oi.Index].Name}{drinks[oi.Index].Size}，每杯{drinks[oi.Index].Price}元，總共{oi.Quantity}杯，小計{oi.SubTotal}元。\n";
                j++;
            }
            TextBlock1.Text += $"\n訂購合計{total}元，{message}，售價{sellPrice}元。";
        }
        private void PlaceOrder(List<OrderItem> myOrder)
        {
            myOrder.Clear();
            for (int i = 0; i < drink_menu.Children.Count; i++)
            {
                StackPanel sp = drink_menu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);

                if (cb.IsChecked == true && quantity != 0)
                {
                    int price = drinks[i].Price;
                    myOrder.Add(new OrderItem() { Index = i, Quantity = quantity, SubTotal = quantity * price });
                }
            }
        }
    }
}
