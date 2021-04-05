using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Calendar
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        void CalendarSelection_SelectionChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<System.Int32> e)
        {
        }

        void Calendar_DisplayDateChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<System.Object> e)
        {
        }

        void Calendar_SelectionChanged(System.Object sender, Telerik.XamarinForms.Input.Calendar.CalendarSelectionChangedEventArgs<System.Object> e)
        {
        }

        void Calendar_TimeSlotTapped(System.Object sender, Telerik.XamarinForms.Input.TimeSlotTapEventArgs e)
        {
        }

        void Calendar_AppointmentTapped(System.Object sender, Telerik.XamarinForms.Input.AppointmentTappedEventArgs e)
        {
        }

        void Calendar_ViewChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<Telerik.XamarinForms.Input.CalendarViewMode> e)
        {
        }

        void Calendar_NativeControlLoaded(System.Object sender, System.EventArgs e)
        {
        }

        void Calendar_CellTapped(System.Object sender, Telerik.XamarinForms.Input.CalendarCell e)
        {
        }
    }
}
