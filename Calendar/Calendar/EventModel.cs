using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace Calendar
{
    public class EventModel : IAppointment, INotifyPropertyChanged
    {
        public Guid PK_EventID { get; set; }
        public Guid OccurenceID { get; set; }
        public Guid EventLabelColorID { get; set; }
        public int TempResId { get; set; }
        public int EventOptions { get; set; }
        public int OffSetStart { get; set; }
        public int OffSetEnd { get; set; }

        //These are mantatory fileds for Telerik 
        //so these are used for Subjetct,Start date and end date
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private Color _Color;
        public Color Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                OnPropertyChanged();
            }
        }
        public string Detail { get; set; }
        public bool IsAllDay { get; set; }

        public bool IsStatCodeAdded { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DetailEventModel : EventModel
    {
        public int EventLabelColor { get; set; }


        public string LocationCaption { get; set; }

        public DateTime ReminderDate { get; set; }


        public bool IsPrivate { get; set; }

        public bool IsSpecialAttention { get; set; }
        public Guid F_ContactID { get; set; }
        public Guid F_CreatedUser_ID { get; set; }
        public bool IsPublished { get; set; }
        public string Responsible { get; set; }
        public Guid SelectedResourceID { get; set; }


        public List<EventLabelModel> EventLableList { get; set; }

        public Dictionary<int, EventLabelModel> LabelsDictionary { get; set; }
        public bool HasAttachments { get; set; }
        public Guid LinkId { get; set; }
        public bool HasMissingResources { get; set; }
        public int EventStatusColor { get; set; }
        public int? RemindMinutesBefore { get; set; }
        public bool HasReminder { get; set; }
        public bool IsPublishedToSognDK { get; set; }
        public string EventStatusDescription { get; set; }


        public string EventMessage { get; set; }


        public bool IsReccuringEvent { get; set; }

        public bool HasLink { get; set; }
        public bool IsProsteModulAppointment { get; set; }


        public bool HasEventMessage { get; set; }


        public EventLabelModel PrimeLabel { get; set; }
        public bool IsIconListVisible { get; set; }
        public string EventStatusColorHex { get; set; }


        public string TimeString { get; set; }


        public ResourceItemModel SelectedResource { get; set; }
        public StatStatusEnum StatStatus { get; set; }

        public bool IsXbridgeAppointment { get; set; }

        public string EventPrimeLabelColorHex { get; set; }

        public int ItemType { get; set; }

        public ImageSource TidImgSource { get; set; }

        public Color TidColor { get; set; }

        public Guid EventTemplateID { get; set; }

        // public List<ListModel> test { get; set; }
        public int LableListWidth { get; set; }

        public DetailEventModel()
        {
            EventLableList = new List<EventLabelModel>();
            LabelsDictionary = new Dictionary<int, EventLabelModel>();
            // test = new List<ListModel>();
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void NotifyPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        //    }
        //}
    }

    public class SelectedLabelModel
    {
        public bool IsCancel { get; set; }
        public List<Guid> LabelIds { get; set; }

    }

    public class DetailPopUpEventModel : DetailEventModel
    {
        public string UsersList { get; set; }

        public string GroupsList { get; set; }
        public string InternalComment { get; set; }
        public bool IsInternalCommentVisible { get; set; }

        public bool IsInternalCommentEnabled { get; set; }

        public bool IsLabelsVisible { get; set; }
    }
    public class EventLabelModel
    {
        public Guid LabelId { get; set; }
        public string LabelCaption { get; set; }
        public int LabelColor { get; set; }
        public Nullable<Guid> F_OrganizationID { get; set; }
        public Nullable<bool> SystemDefined { get; set; }
        public string HexColor { get; set; }
        public bool IsSelect { get; set; }
    }
    public class ResourceItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public ResourceTypeDTO ResourceType { get; set; }
        public string ImageSource { get; set; }
        public bool IsCannotPlan { get; set; }
    }
    public enum StatStatusEnum { NoCodesAdded, NoValuesAdded, Ok };  // red . Yellow, Green
    public enum ResourceTypeDTO
    {
        tDummy,
        tOrg,
        tLoc,
        tEquip,
        tUser,
        tContact,
        tWaste,
        tGroup,
        tEvent,
        tAwayUser,
        tFavourites,
        tMySelf,
        tUserContact,
        AllContact,
        ExchangeUsers,
        All
    };
}
