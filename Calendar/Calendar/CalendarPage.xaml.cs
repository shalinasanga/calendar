using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.XamarinForms.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Calendar
{
    public partial class CalendarPage : ContentPage
    {
        private double MainPageHeight = Application.Current.MainPage.Height;
        public CalendarPage()
        {
            InitializeComponent();
            ViewModel = new CalendarViewModel()
            {
                SelectedDate = DateTime.Now
            };
            Calendar.SetStyleForCell = SetStyleForCell;
        }
        public CalendarViewModel ViewModel
        {
            get { return BindingContext as CalendarViewModel; }
            set { BindingContext = value; }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetEvents();
        }
        private CalendarCellStyle SetStyleForCell(CalendarCell cell)
        {

            if (cell.Type == CalendarCellType.DayName)
            {
                double fontSizeDay;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    fontSizeDay = 10;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    fontSizeDay = 16;
                }
                else
                {
                    fontSizeDay = 16;
                }
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.Transparent,
                    FontSize = fontSizeDay,

                    // Device.OnPlatform<double>(10, 16, 16),
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("999999"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    BorderThickness = 0
                };
            }


            if (cell.Type == CalendarCellType.WeekNumber)
            {
                return new CalendarCellStyle
                {
                    BackgroundColor = Color.White,
                    FontAttributes = FontAttributes.Italic,
                    FontSize = Device.RuntimePlatform == Device.Android ? 10 : 5,
                    TextColor = Color.Gray,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    BorderThickness = 0,
                    Padding = 0

                };
            }

            if (cell.Type == CalendarCellType.Title)
            {
                return new CalendarCellStyle
                {
                    FontSize = Device.RuntimePlatform == Device.Android ? 15 : 10,
                    FontAttributes = FontAttributes.Bold,

                };
            }
            Thickness thickness;
            if (Device.RuntimePlatform == Device.iOS)
            {
                thickness = new Thickness(0, 0, 0, 1);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                thickness = new Thickness(1);
            }
            else
            {
                thickness = new Thickness(0);
            }

            double fontSize;
            if (Device.RuntimePlatform == Device.iOS)
            {
                fontSize = 15;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                fontSize = 16;
            }
            else
            {
                fontSize = 0;
            }
            var defaultStyle = new CalendarCellStyle
            {

                BackgroundColor = Color.FromHex("EEEEEE"),
                BorderColor = Color.FromHex("CCCCCC"),
                BorderThickness = thickness, //Device.OnPlatform<Thickness>(new Thickness(0, 0, 0, 1), 1, 0),
                FontSize = fontSize, //Device.OnPlatform<double>(15, 16, 0),

                TextColor = Color.FromHex("333333")
            };

            var dayCell = cell as CalendarDayCell;

            if (dayCell != null)
            {
                if (dayCell.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    defaultStyle.TextColor = Color.FromHex("D42A28");
                }

                if (dayCell.IsFromCurrentMonth)
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.FontAttributes = FontAttributes.Bold;
                    }
                }
                else
                {
                    if (dayCell.IsToday)
                    {
                        defaultStyle.TextColor = Color.FromRgb(115, 174, 239);
                    }
                    else
                    {
                        defaultStyle.TextColor = Color.FromHex("999999");
                        defaultStyle.BackgroundColor = Color.FromHex("E5E5E5");
                    }
                }

                if (dayCell.IsSelected)
                {
                    defaultStyle.TextColor = Color.White;
                }

                return defaultStyle;
            }

            return null; // default style
        }

        void CalendarSelection_SelectionChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<System.Int32> e)
        {
            switch (e.NewValue)
            {
                case 0:
                    Calendar.ViewMode = Telerik.XamarinForms.Input.CalendarViewMode.Day;
                    break;
                case 1:
                    Calendar.ViewMode = Telerik.XamarinForms.Input.CalendarViewMode.MultiDay;
                    break;
                case 2:
                    Calendar.ViewMode = Telerik.XamarinForms.Input.CalendarViewMode.Month;
                    Calendar.WeekNumbersDisplayMode =
                        Telerik.XamarinForms.Common.DisplayMode.Show;
                    break;
                case 3:
                    Calendar.ViewMode = Telerik.XamarinForms.Input.CalendarViewMode.Week;
                    break;
                case 4:
                    Calendar.ViewMode = Telerik.XamarinForms.Input.CalendarViewMode.Agenda;
                    break;
                default:
                    break;
            }
        }
        void Calendar_ViewChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<Telerik.XamarinForms.Input.CalendarViewMode> e)
        {
            try
            {
                var vm = ViewModel as CalendarViewModel;
                if (e.NewValue == CalendarViewMode.Month || e.NewValue == CalendarViewMode.Agenda)
                {
                    Calendar.HeightRequest = MainPageHeight - 30;
                    Calendar.Margin = new Thickness(0);
                    Calendar.GridLinesDisplayMode =
                        Telerik.XamarinForms.Common.DisplayMode.Hide;
                    GetEvents();
                    // TidEventsList.IsVisible = false;
                    // EventsList.IsVisible = false;
                }
                else if (e.NewValue == CalendarViewMode.Day)
                {
                    Calendar.HeightRequest = MainPageHeight - 30;
                    Calendar.GridLinesDisplayMode =
                      Telerik.XamarinForms.Common.DisplayMode.Show;
                    Calendar.WeekNumbersDisplayMode =
                        Telerik.XamarinForms.Common.DisplayMode.Hide;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Calendar.Margin = new Thickness(-10, 0, 0, 0);
                    }
                    else
                    {
                        Calendar.Margin = new Thickness(-15, 0, 0, 0);
                    }
                    //  vm.ChangeEventsDefaultColor();
                    //TidEventsList.IsVisible = false;
                    // EventsList.IsVisible = false;
                    //Calendar.ScrollTimeIntoView(new TimeSpan(UserProfileSettings.StartWorkingHours, 0, 0));
                }
                else if (e.NewValue == CalendarViewMode.MultiDay)
                {
                    Calendar.HeightRequest = MainPageHeight - 30;
                    Calendar.GridLinesDisplayMode =
                      Telerik.XamarinForms.Common.DisplayMode.Show;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Calendar.Margin = new Thickness(-15, 0, 0, 0);
                    }
                    else
                    {
                        Calendar.Margin = new Thickness(-20, 0, 0, 0);

                    }
                    // vm.ChangeEventsDefaultColor();
                    // TidEventsList.IsVisible = false;
                    // EventsList.IsVisible = false;
                }
                else if (e.NewValue == CalendarViewMode.Week)
                {
                    // TidEventsList.IsVisible = true;
                    // EventsList.IsVisible = true;
                    Calendar.GridLinesDisplayMode =
                     Telerik.XamarinForms.Common.DisplayMode.Show;
                    Calendar.HeightRequest = 130;
                    MainGrid.RowDefinitions[1].Height = MainPageHeight - 30;
                    if (vm.InitialEventList == null || !vm.InitialEventList.Any())
                    {
                        GetEvents();
                    }
                    else
                    {
                        //   vm.ChangeEventsDefaultColor();
                        //   vm.FilterEventsForSelectedDate(false);
                    }
                }
            }
            catch (Exception exception)
            {

            }
        }
        void Calendar_DisplayDateChanged(System.Object sender, Telerik.XamarinForms.Common.ValueChangedEventArgs<System.Object> e)
        {
            if (e.NewValue == null || e.PreviousValue == null)
            {
                return;
            }

            CommonEventFetchDateValidation((DateTime)e.NewValue,
           (DateTime)e.PreviousValue);
        }

        void Calendar_SelectionChanged(System.Object sender, Telerik.XamarinForms.Input.Calendar.CalendarSelectionChangedEventArgs<System.Object> e)
        {
            if (e.NewValue == null)
            {
                return;
            }
            DateTime previousDateValue = DateTime.Now;
            if (Calendar.ViewMode == CalendarViewMode.Week && e.PreviousValue == null)
            {
                previousDateValue = ((DateTime)e.NewValue);
            }
            else if (e.PreviousValue == null)
            {
                return;
            }
            else
            {
                previousDateValue = (DateTime)e.PreviousValue;
            }

            if (Calendar.ViewMode != CalendarViewMode.Day)
            {
                CommonEventFetchDateValidation((DateTime)e.NewValue,
                previousDateValue);
            }
        }

        void Calendar_TimeSlotTapped(System.Object sender, Telerik.XamarinForms.Input.TimeSlotTapEventArgs e)
        {
        }

        void Calendar_AppointmentTapped(System.Object sender, Telerik.XamarinForms.Input.AppointmentTappedEventArgs e)
        {
        }

        void Calendar_NativeControlLoaded(System.Object sender, System.EventArgs e)
        {
            CalendarSelection.SelectedIndex = 2;
        }

        void Calendar_CellTapped(System.Object sender, Telerik.XamarinForms.Input.CalendarCell e)
        {
            try
            {
                if (Calendar.ViewMode == CalendarViewMode.Month ||
                    Calendar.ViewMode == CalendarViewMode.Agenda)
                {
                    CalendarSelection.SelectedIndex = 1;
                    CalendarDateCell cell = (CalendarDateCell)e;
                    if (cell != null)
                    {
                        Calendar.DisplayDate = GetFirstDayOfWeek(cell.Date);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        private async void CommonEventFetchDateValidation(DateTime newValue, DateTime preValue)
        {

            try
            {
                DateTime NewDateValue = newValue;
                if (NewDateValue.Year == DateTime.MinValue.Year ||
                    NewDateValue.Year == Calendar.MinDate.Year)
                {
                    return;
                }
                DateTime PrevDateValue = preValue;

                if (Calendar.ViewMode == CalendarViewMode.Month ||
                    Calendar.ViewMode == CalendarViewMode.Agenda)
                {
                    if (NewDateValue.Month != PrevDateValue.Month || NewDateValue.Year != PrevDateValue.Year)
                    {
                        (ViewModel as CalendarViewModel).SelectedDate = NewDateValue;
                        GetEvents();
                    }
                    else
                    {
                        return;
                    }
                }
                else if (Calendar.ViewMode == CalendarViewMode.MultiDay)
                {
                    //Calendar.DisplayDate = GetFirstDayOfWeek(NewDateValue);
                    if ((ViewModel as CalendarViewModel).SelectedDate.Date ==
                        NewDateValue.Date)
                    {
                        return;
                    }
                    else
                    {
                        (ViewModel as CalendarViewModel).SelectedDate = NewDateValue;
                        GetEvents();
                    }
                }
                else if (Calendar.ViewMode == CalendarViewMode.Week)
                {
                    if (PrevDateValue.Date == NewDateValue.AddDays(7).Date ||
                        PrevDateValue.Date == NewDateValue.AddDays(-7).Date)//Week changed, display date change fired
                    {
                        (ViewModel as CalendarViewModel).SelectedDate = NewDateValue;
                        GetEvents();
                        Calendar.SelectedDate = NewDateValue;
                    }
                    else
                    {
                        (ViewModel as CalendarViewModel).SelectedDate = NewDateValue;
                        //  (ViewModel as CalendarViewModel).FilterEventsForSelectedDate(false);
                    }
                }
                else
                {
                    DateTime StartOfThisWeek = GetFirstDayOfWeek(PrevDateValue);
                    DateTime EndOfThisWeek = StartOfThisWeek.AddDays(7);

                    if (NewDateValue.Date < StartOfThisWeek.Date || NewDateValue.Date > EndOfThisWeek.Date)
                    {
                        (ViewModel as CalendarViewModel).SelectedDate = NewDateValue;
                        GetEvents();
                    }
                    else
                    {
                        return;
                    }
                }
                //Calendar.AppointmentsSource = (ViewModel as CalendarViewModel).EventListForCal;
            }
            catch (Exception exception)
            {

            }
        }

        private void GetEvents()
        {

            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var vm = ViewModel as CalendarViewModel;
                    await vm.ExecuteGetMonthViewEventsCommand().ConfigureAwait(true);
                    //Calendar.ScrollTimeIntoView(new TimeSpan(UserProfileSettings.StartWorkingHours, 0, 0));
                });
            }
            catch (Exception exception)
            {

            }
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
