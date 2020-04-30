using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Contacts.Droid.Converters
{
    public class InvertBoolConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}