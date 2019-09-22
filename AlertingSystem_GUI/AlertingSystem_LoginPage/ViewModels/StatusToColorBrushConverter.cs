using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using AlertingSystem_LoginPage.Models;


namespace AlertingSystem_LoginPage.ViewModels
{
    public class StatusToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string input = value as string;
            switch (input)
            {
                case "Active":
                    return Brushes.LightGreen;
                case "Alert":
                    return Brushes.Red;
                case "Admitted":
                    return Brushes.Orange;
                case "Discharged":
                    return Brushes.White;
                default:
                    return DependencyProperty.UnsetValue;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
