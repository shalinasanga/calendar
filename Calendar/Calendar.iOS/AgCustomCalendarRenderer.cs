using System;
using System.Globalization;
using Calendar.iOS;
using Foundation;
using Telerik.XamarinForms.Input;
using Telerik.XamarinForms.InputRenderer.iOS;
using TelerikUI;
using Calendar;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AgCustomCalendar), typeof(AgCustomCalendarRenderer))]
namespace Calendar.iOS
{
    public class AgCustomCalendarRenderer : CalendarRenderer
    {
        public AgCustomCalendarRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<RadCalendar> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control?.Calendar != null && Element != null)
                {
                    NSCalendar calendar = new NSCalendar(NSCalendarType.Gregorian);
                    calendar.Locale = NSLocale.CurrentLocale;
                    this.Control.Calendar = calendar;

                    #region Month view
                    if (Control?.Presenter is TKCalendarMonthPresenter)
                    {
                        (Control.Presenter as TKCalendarMonthPresenter).Style.WeekNumberCellWidth = 55;
                    }
                    #endregion
                    #region Day view
                    else if (Control?.Presenter is TKCalendarDayViewPresenter)
                    {
                        if (this.Control.Presenter is TelerikUI.TKCalendarDayViewPresenter presenter)
                        {
                            if (e.NewElement != null)
                            {
                                if (e.NewElement.AppointmentsSource == null)
                                    return;

                                if (presenter != null)
                                {
                                    var dayView = presenter.DayView;
                                    var label = dayView.AllDayEventsView.LabelView as UILabel;
                                    label.TextAlignment = UITextAlignment.Center;
                                    label.Font = UIFont.SystemFontOfSize(10);
                                    label.TextColor = UIColor.Black;
                                    label.Text = "    " + "Allday";

                                    dayView.EventsView.Style.EventsSpacing = 0;

                                    //dayView.EventsView.RegisterClassForCell(typeof(DayViewCustomCell),
                                    //    DayViewCustomCell.Identifier);

                                    dayView.EventsView.UpdateLayout();
                                    dayView.AllDayEventsView.UpdateLayout();
                                    dayView.DataSource = new DayViewDataSource(this);
                                }
                            }
                        }




                    }
                    #endregion
                    #region Week view
                    else if (Control?.Presenter is TKCalendarMultiDayViewPresenter)
                    {
                        var presenter = this.Control.Presenter as TelerikUI.TKCalendarMultiDayViewPresenter;

                        if (e.NewElement != null)
                        {
                            //To fix the issue when navigate to week view from month view date selection
                            if (e.NewElement.DisplayDate.Date == DateTime.Now.Date)
                            {
                                e.NewElement.DisplayDate = GetFirstDayOfWeek(e.NewElement.DisplayDate);
                            }

                            if (e.NewElement.AppointmentsSource == null)
                                return;

                            if (presenter != null)
                            {
                                var weekView = presenter.MultiDayView;
                                var label = weekView.AllDayEventsView.LabelView as UILabel;
                                label.TextAlignment = UITextAlignment.Center;
                                label.Font = UIFont.SystemFontOfSize(10);
                                label.TextColor = UIColor.Black;
                                label.Text = "   " + "Allday";
                                label.Hidden = true;
                                weekView.EventsView.Style.EventsSpacing = 0;
                                weekView.EventsView.UpdateLayout();
                                weekView.AllDayEventsView.UpdateLayout();
                                weekView.DataSource = new DayViewDataSource(this);
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override CalendarDelegate CreateCalendarDelegateOverride()
        {
            return new CustomCalendarDelegate();
        }
        public DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }
    }
}
