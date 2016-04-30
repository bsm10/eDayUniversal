using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента диалогового окна содержимого см. в разделе http://go.microsoft.com/fwlink/?LinkId=234238

namespace eDay
{
    public sealed partial class AddEvent : ContentDialog
    {
        public DateTime eventDatetime { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }

        public AddEvent(int eventClass)
        {
            InitializeComponent();
            Background = ColorEvent.ConvertToColor(eventClass);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            eventDatetime = new DateTime(DatePicker.Date.Year,
                                        DatePicker.Date.Month,
                                        DatePicker.Date.Day,
                                        TimePicker.Time.Hours,
                                        TimePicker.Time.Minutes,
                                        0);
            eventName = txtEvent.Text;
            eventDescription = txtDescription.Text;
        }

    private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
