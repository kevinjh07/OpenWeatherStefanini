using System;
using System.Globalization;
using Xamarin.Forms;

namespace OpenWeatherStefanini.Converters
{
    public class FavoritedCityToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = (bool)value ? "ic_fa_star.png" : "ic_fa_star_o.png";
            return ImageSource.FromFile(file);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
