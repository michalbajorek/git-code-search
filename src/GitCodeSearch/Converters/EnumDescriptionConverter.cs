﻿using GitCodeSearch.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace GitCodeSearch.Converters;

internal class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Enum enumValue
            ? enumValue.GetDescription()
            : string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
