using System;
using System.Windows.Data;

namespace wvconv {
    [ValueConversion(typeof(bool), typeof(bool))]
    class InverseBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => !(bool)value;
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotSupportedException();
    }
}
