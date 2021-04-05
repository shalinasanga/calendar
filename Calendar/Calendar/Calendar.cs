using System;

using Xamarin.Forms;

namespace Calendar
{
    public class Calendar : ContentPage
    {
        public Calendar()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

