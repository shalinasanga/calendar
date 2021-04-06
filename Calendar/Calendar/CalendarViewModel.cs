using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace Calendar
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        public CalendarViewModel()
        {
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    //UserProfileSettings.LastSelectedCalendarDate = value;
                }
            }
        }
        private ObservableCollection<DetailEventModel> _EventListForCal =
           new ObservableCollection<DetailEventModel>();
        public ObservableCollection<DetailEventModel> EventListForCal
        {
            get { return _EventListForCal; }
            set
            {
                _EventListForCal = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DetailEventModel> _EventListForSelectedDay =
            new ObservableCollection<DetailEventModel>();
        public ObservableCollection<DetailEventModel> EventListForSelectedDay
        {
            get { return _EventListForSelectedDay; }
            set
            {
                _EventListForSelectedDay = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DetailEventModel> _InitialEventList;
        public ObservableCollection<DetailEventModel> InitialEventList
        {
            get { return _InitialEventList; }
            set
            {
                _InitialEventList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DetailEventModel> _InitialTidEventList;
        public ObservableCollection<DetailEventModel> InitialTidEventList
        {
            get { return _InitialTidEventList; }
            set
            {
                _InitialTidEventList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DetailEventModel> _TIDEventListForSelectedDay =
            new ObservableCollection<DetailEventModel>();
        public ObservableCollection<DetailEventModel> TIDEventListForSelectedDay
        {
            get { return _TIDEventListForSelectedDay; }
            set
            {
                _TIDEventListForSelectedDay = value;
                OnPropertyChanged("TIDEventListForSelectedDay");
            }
        }
        private CalendarViewMode _CalendarMode;
        public CalendarViewMode CalendarMode
        {
            get { return _CalendarMode; }
            set
            {
                _CalendarMode = value;
                OnPropertyChanged();
            }
        }
        public async Task ExecuteGetMonthViewEventsCommand()
        {
            try
            {
                if (SelectedDate == DateTime.MinValue)
                {
                    return;
                }

                if (SelectedDate == null)
                {
                    return;
                }


                DateTime startOfMonth;
                DateTime endOfMonth;
                if (CalendarMode == CalendarViewMode.Month || CalendarMode == CalendarViewMode.Agenda)
                {
                    startOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
                    endOfMonth = new DateTime(
                        SelectedDate.Year, SelectedDate.Month,
                        DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month));
                }
                else if (CalendarMode == CalendarViewMode.MultiDay)
                {
                    startOfMonth = SelectedDate.Date.AddDays(-1);
                    endOfMonth = SelectedDate.Date.AddDays(8);
                }
                else
                {
                    startOfMonth = SelectedDate.Date.AddDays(-8);
                    endOfMonth = SelectedDate.Date.AddDays(8);
                }


                //await eventHelper.CreateTidSlots(SelectedResource, startOfMonth, endOfMonth, FilterDataModel.ShowTidSlots).ConfigureAwait(true);
                //await eventHelper.GetEvents(startOfMonth, endOfMonth, SelectedLabelList, SelectedResource, SelectedFunctionList, SelectedEventStatusList, false).ConfigureAwait(true);
                //eventHelper.UpdatePrivateAppointments(FilterDataModel.PrivateBooking);
                //await eventHelper.ShowHideXbridgeAppointments(FilterDataModel.X_BridgeApps, startOfMonth, endOfMonth, SelectedResource).ConfigureAwait(true);

                // InitialEventList = eventHelper.GetEventList();
                // InitialTidEventList = eventHelper.GetTidEventList();
                //FavouriteResources = eventHelper.GetFavouriteResources();
                //FavouriteLabelList = eventHelper.GetFavLabelList();

                //if (FilterDataModel != null)
                //{
                //    if (FilterDataModel.SpecialAppointments)
                //    {
                //        await eventHelper.GetAppointmentswithFunctionsmissingResourceOrSpecialAttn(startOfMonth, endOfMonth, SelectedLabelList,
                //            SelectedResource, SelectedFunctionList, SelectedEventStatusList, false).ConfigureAwait(true);
                //    }
                //    else if (FilterDataModel.IncompleteAppointments)
                //    {
                //        await eventHelper.GetAppointmentswithFunctionsmissingResourceOrSpecialAttn(startOfMonth, endOfMonth,
                //            SelectedLabelList, SelectedResource, SelectedFunctionList, SelectedEventStatusList, true).ConfigureAwait(true);
                //    }
                //    else if (FilterDataModel.AppointmentsWithStat)
                //    {
                //        await eventHelper.ShowHideStatCodeCountAddedAppointments(true).ConfigureAwait(true);
                //    }

                //    // InitialEventList = eventHelper.GetEventList();
                //}
                var startdate = SelectedDate.AddDays(-8);
                ObservableCollection<DetailEventModel> tempList = new ObservableCollection<DetailEventModel>();
                List<EventLabelModel> labels = new List<EventLabelModel>();
                //labels.Add(new EventLabelModel { HexColor = "#FF0066" });
                //labels.Add(new EventLabelModel { HexColor = "#1A4BC0" });
                //labels.Add(new EventLabelModel { HexColor = "#804000" });

                for (int i = 0; i < 50; i++)
                {
                    if (i % 2 == 0)
                    {
                        tempList.Add(new DetailEventModel
                        {
                            Title = "Title" + i.ToString(),
                            StartDate = startdate.AddHours(1),
                            EndDate = startdate.AddHours(2),
                            Color = Color.LightBlue,
                            LocationCaption = "Room 1",
                            EventLableList = labels
                        });
                    }
                    else
                    {
                        tempList.Add(new DetailEventModel
                        {
                            Title = "Title" + i.ToString(),
                            StartDate = startdate.AddHours(-1),
                            EndDate = startdate.AddHours(-2),
                            Color = Color.LightSalmon,
                            LocationCaption = "Room 2"
                        });
                    }
                    startdate = startdate.AddDays(1);
                }

                //foreach (var item in InitialTidEventList)
                //{
                //    tempList.Add(item);
                //}

                //foreach (var item in InitialEventList)
                //{
                //    if (!tempList.Any(x => x.OccurenceID == item.OccurenceID))
                //    {
                //        item.LableListWidth = item.EventLableList.Count * 6;
                //        //item.Color = GetEventLabelDefaultColor(item);
                //        tempList.Add(item);
                //    }
                //}
                EventListForCal.Clear();
                EventListForCal = tempList;

                //if (CalendarMode == CalendarViewMode.Week)
                //{
                //    FilterEventsForSelectedDate(false);
                //}
            }
            catch (Exception exception)
            {

            }
        }
        //private async Task GenerateEvents()
        //{
        //    try
        //    {
        //        var startdate = SelectedDate.AddDays(-8);
        //        ObservableCollection<DetailEventModel> tempevents = new ObservableCollection<DetailEventModel>();
        //        for (int i = 0; i < 50; i++)
        //        {
        //            tempevents.Add(new DetailEventModel{
        //            });
        //        }
        //        return tempevents;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
