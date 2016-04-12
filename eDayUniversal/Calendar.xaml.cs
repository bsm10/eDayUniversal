using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Шаблон элемента пользовательского элемента управления задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234236

namespace eDay
{
    /// <summary>
    /// Класс возвращает 35 дней календаря 7х5 для заданного годы и месяца
    /// </summary>
    public sealed class MonthCalendar
    {
        public ObservableCollection<MonthDay> days { get; set; }
        public int currentYear { get; private set; }
        public int currentMonth { get; private set; }
        /// <summary>
        /// Добавляет месяц (может быть отрицательным)
        /// </summary>
        public void AddMonth(int month)
        {
            days = GetDaysOfMonth(currentYear, currentMonth+ month);
        }
        public void AddYear(int year)
        {
            days = GetDaysOfMonth(currentYear+year, currentMonth);
        }

        public MonthCalendar()
        {
            days = GetDaysOfMonth(DateTime.Now.Year, DateTime.Now.Month);

        }
        public MonthCalendar(int year = 2016,int month=1)
        {
            days = GetDaysOfMonth(year, month);
        }
        public ObservableCollection<MonthDay> GetDaysOfMonth(int year = 2016, int month = 1)
        {
            currentYear = year;
            currentMonth = month;
            ObservableCollection<MonthDay> dd = new ObservableCollection<MonthDay>();
            DateTime date = new DateTime(year, month, 1);
            int firstDay = (int)date.DayOfWeek;
            int delta = 0;
            switch (firstDay)
            {
                case 1:
                    delta = 0;
                    break;
                case 2:
                    delta = 1;
                    break;
                case 3:
                    delta = 2;
                    break;
                case 4:
                    delta = 3;
                    break;
                case 5:
                    delta = 4;
                    break;
                case 6:
                    delta = 5;
                    break;
                case 0:
                    delta = 6;//sunday - is 0 day of week!
                    break;
            }
            DateTime startDate = date.Subtract(TimeSpan.FromDays(delta));
            for (int i = 0; i < 35; i++)
            {
                MonthDay d = new MonthDay(startDate.AddDays(i));
                if (d.datetime.DayOfWeek == DayOfWeek.Saturday | d.datetime.DayOfWeek == DayOfWeek.Sunday)
                {
                    d.colorDay = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                else
                {
                    d.colorDay = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                }
                if (d.datetime.Month != month)
                {
                    d.colorDay.Opacity = 0.35;
                }
                d.VisibilityBorderToday = d.datetime.Date == DateTime.Now.Date ? Visibility.Visible : Visibility.Collapsed;

                dd.Add(d);
            }
            return dd;
        }

        //public string monthMMMMyyyy
        //{
        //    get
        //    {
        //        if (year == 0) year = DateTime.Now.Year;
        //        if (month == 0) month = DateTime.Now.Month;
        //        return new DateTime(year, month, 1).ToString("MMMM yyyy");
        //    }
        //}
    }
    public class MonthDay
    {
        public DateTime datetime { get; set; }
        //public Color color { get; set; }
        public SolidColorBrush colorDay { get; set; }
        public Visibility VisibilityBorderToday { get; set; }
        public MonthDay(DateTime date)
        {
            datetime = date;
        }
        public override string ToString()
        {
            return datetime.Day.ToString();
        }
    }

    public sealed partial class Calendar : UserControl
    {
        public event RoutedEventHandler ClickCalendarHandler;
        public DateTime TappedDate { get; private set; }

        public Calendar()
        {
            InitializeComponent();
            
        }
        public void SetMonth(int year,int month)
        {
            DataContext = new MonthCalendar(year, month);
        }
        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        public GridView GridCalendar
        {
            get { return gridView; }
        }

        private void gridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (ClickCalendarHandler != null)
            //    ClickCalendarHandler(sender, e);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock t = (TextBlock)e.OriginalSource;
            MonthDay day_tapped = (MonthDay)t.DataContext;
            TappedDate = day_tapped.datetime;

            if (ClickCalendarHandler != null)
                ClickCalendarHandler(sender, e);

        }
    }
}
