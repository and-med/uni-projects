using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComputationalMethods.Helpers
{
    static class Handler
    {
        public static RoutedEventHandler GetGotFocusEventHandler(string message)
        {
            return (sender, e) =>
            {
                TextBox tb = (TextBox)sender;
                if (tb.Text == message)
                {
                    tb.Text = "";
                    tb.Foreground = new SolidColorBrush(Colors.Black);
                }
            };
        }

        public static RoutedEventHandler GetLostFocusEventHandler(string message)
        {
            return (sender, e) =>
            {
                TextBox tb = (TextBox)sender;
                if (tb.Text == "")
                {
                    tb.Text = message;
                    tb.Foreground = new SolidColorBrush(Colors.LightGray);
                }
            };
        }
    }
}
