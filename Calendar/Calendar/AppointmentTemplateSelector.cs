using System;
using Xamarin.Forms;

namespace Calendar
{
    public class AppointmentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AllDay { get; set; }
        public DataTemplate NormalAppointment { get; set; }
        public DataTemplate TID { get; set; }
        public DataTemplate XBridge { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var appointmentsTemplate = item as DetailEventModel;

            if (appointmentsTemplate.IsAllDay)
            {
                return this.AllDay;
            }
            else if (appointmentsTemplate.Detail == "Tid")
            {
                return this.TID;
            }
            else if (appointmentsTemplate.Detail == appointmentsTemplate.OccurenceID.ToString())
            {
                return this.NormalAppointment;
            }
            return this.XBridge;
        }
    }
}
