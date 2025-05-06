using System.Globalization;

namespace NewDantecMarket.Convertisseurs
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Vérifie si la valeur de la chaîne n'est pas null ou vide
            return value is string stringValue && !string.IsNullOrEmpty(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}