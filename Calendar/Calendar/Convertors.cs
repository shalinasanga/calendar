using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Calendar
{
    public class TitleImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            if (value.ToString() == "Present")
            {
                return "present.png";
            }
            else if (value.ToString() == "Absence")
            {
                return "absence.png";
            }
            else//TID_TYPE_ONDUTY
            {
                return "onduty.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class TitleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.FromHex("#ffffff");
            }
            if (value.ToString() == "Present")
            {
                return Color.FromHex("#59a8e2");//"#596666FF";
            }
            else if (value.ToString() == "Absence")
            {
                return Color.FromHex("#f85454");
            }
            else//TID_TYPE_ONDUTY
            {
                return Color.FromHex("#F68AC0");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class TextToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            if (value.ToString().Equals("temporary"))
            {
                return "Event_Status_temp_Red.png";
            }
            else if (value.ToString().Equals("planing"))
            {
                return "Event_Status_planning_Yellow.png";
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class EventStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            if (value.ToString().Equals("temporary"))
            {
                return true;
            }
            else if (value.ToString().Equals("planing"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class LabelsToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            IList<EventLabelModel> labels = (IList<EventLabelModel>)value;
            int width = 0;
            for (int i = 0; i < labels.Count; i++)
            {
                if (i == 6)
                {
                    return 6 * 6;
                }
                width += 6;
            }
            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class ArgbToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = value;
            var param = parameter;

            if (color == null)
                return Color.LightGray;
            else
                color = Color.FromHex(color.ToString());

            //Color cl = (Color)color;//Color.FromHex(color);

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.White;
        }
    }
    public class StatStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Default;
            var StatStatus = (StatStatusEnum)value;

            switch (StatStatus)
            {
                case StatStatusEnum.NoCodesAdded:
                    color = Color.Red;
                    break;
                case StatStatusEnum.NoValuesAdded:
                    color = Color.Yellow;
                    break;
                case StatStatusEnum.Ok:
                    color = Color.Green;
                    break;
                default:
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.White;
        }
    }
    public class StatStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEnabled = true;
            var StatStatus = (StatStatusEnum)value;

            if (StatStatus == StatStatusEnum.NoCodesAdded)
            {
                isEnabled = false;
            }

            return isEnabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
    public class StatStatusToEnableColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color isEnabledColor = Color.FromHex("#00A99D");
            var StatStatus = (StatStatusEnum)value;

            if (StatStatus == StatStatusEnum.NoCodesAdded)
            {
                isEnabledColor = Color.FromHex("#d3d3d3");
            }

            return isEnabledColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
    public class StatStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = "";
            var StatStatus = (StatStatusEnum)value;

            switch (StatStatus)
            {
                case StatStatusEnum.NoCodesAdded:
                    image = "";
                    break;
                case StatStatusEnum.NoValuesAdded:
                    image = "ic_check_circle_uncheck_green36.png";
                    break;
                case StatStatusEnum.Ok:
                    image = "ic_check_circle_green36.png";
                    break;
                default:
                    break;

            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "checkbox-blank-circle-outline";
        }
    }
    public class PrivateAppointmentNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                //To check private value.ToString()==("<%Private%>") to value.ToString().Contains("<%Private%>") cos,all day events subject set subject + owner
                if (value.ToString().Contains("<%Private%>"))
                {
                    value = "Private";
                }
            }
            catch (Exception ex)
            {
                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class LocationCaptionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var caption = value;
            if (caption == null || (string)caption == String.Empty)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }

    }
}
