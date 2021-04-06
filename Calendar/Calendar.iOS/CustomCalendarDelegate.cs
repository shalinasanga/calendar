using System;
using Telerik.XamarinForms.InputRenderer.iOS;
using TelerikUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Telerik.XamarinForms.Common.iOS;
using System.Globalization;
namespace Calendar.iOS
{
    public class CustomCalendarDelegate : CalendarDelegate
    {
        public override void UpdateVisualsForCell(TKCalendar calendar, TKCalendarCell cell)
        {
            try
            {
                if (cell == null)
                {
                    return;
                }
                if (calendar.Presenter is TKCalendarMonthPresenter)
                {
                    this.SetBordersWidth(cell, 0);

                    var weekkNumberCell = cell as TKCalendarWeekNumberCell;
                    if (weekkNumberCell != null)
                    {
                        if (!weekkNumberCell.Label.Text.Contains("v."))
                        {
                            weekkNumberCell.Label.Text = "v." + weekkNumberCell.Label.Text;
                        }
                    }
                }
                else
                {
                    var dayTitle = cell as TKCalendarTitleCell;
                    if (dayTitle != null)
                    {
                        CultureInfo cultureInfoCal = new CultureInfo(CultureInfo.CurrentCulture.Name);
                        System.Globalization.Calendar cal = cultureInfoCal.Calendar;
                        dayTitle.Label.Text = dayTitle.Label.Text +
                            " (" + "week" + " " +
                            cal.GetWeekOfYear(dayTitle.Owner.DisplayedDate.ToLocalDateTime(),
                            cultureInfoCal.DateTimeFormat.CalendarWeekRule,
                            cultureInfoCal.DateTimeFormat.FirstDayOfWeek) + ")";
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void SetBordersWidth(TKCalendarCell cell, int width)
        {
            cell.Style.TopBorderWidth = width;
            cell.Style.LeftBorderWidth = width;
            cell.Style.RightBorderWidth = width;
            cell.Style.BottomBorderWidth = width;
        }

        private void SetBordersColor(TKCalendarCell cell, Color color)
        {
            var uiColor = color.ToUIColor();

            cell.Style.TopBorderColor = uiColor;
            cell.Style.LeftBorderColor = uiColor;
            cell.Style.RightBorderColor = uiColor;
            cell.Style.BottomBorderColor = uiColor;
        }
    }
}
