using System;
using System.Linq;
using System.Reflection;
using Foundation;
using Telerik.XamarinForms.InputRenderer.iOS;
using TelerikUI;
using UIKit;

namespace Calendar.iOS
{
    public class DayViewDataSource : CalendarDayViewDataSource
    {
        public DayViewDataSource(CalendarRenderer renderer)
            : base(renderer)
        {
        }
        public override UICollectionViewCell EventCellForItemAtIndexPath(TKCalendarDayView dayView, NSIndexPath indexPath)
        {
            try
            {
                var cell = base.EventCellForItemAtIndexPath(dayView, indexPath);
                var fields = cell.GetType().GetFields(
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance).ToList();
                if (fields.Count > 0)
                {
                    var style = fields.FirstOrDefault(a => a.Name == "style");
                    var cellStyle = style.GetValue(cell) as TKCalendarDayViewEventCellStyle;
                    if (cellStyle != null)
                    {
                        cellStyle.DecorationWidth = 0;
                    }
                }

                return cell;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
