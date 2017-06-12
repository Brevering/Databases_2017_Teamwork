using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfMovieSystem.Helpers
{
    public class MoviesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HashSet<Movie> collection = value as HashSet<Movie>;
            StringBuilder result = new StringBuilder();
            if (collection == null)
            {
                return string.Empty;
            }

            foreach (Movie movie in collection)
            {
                result.Append(movie.Title + ", ");
            }

            if (result.Length > 2)
            {
                result.Length -= 2;
            }

            return result.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
