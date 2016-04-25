using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FlowMetEFTest.ValueConverters
{
    class BooleanToGenderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var genderBoolean = (bool)value;
            return genderBoolean ? "Female" : "Male";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var genderString = (string)value;
            
            if (genderString == "Male")
            {
                return false;
            }
            else if (genderString == "Female")
            {
                return true;
            }
            else
            {
                throw new ArgumentException("The argument string must equivalent be either Male or Female");
            }
        }
    }
}
