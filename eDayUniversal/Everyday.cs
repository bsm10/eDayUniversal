﻿using System;
using System.Text;
using System.Net;
using System.IO;
//using Windows.Web.Http;
using System.ComponentModel;
using System.Collections.ObjectModel;
//using Microsoft.Phone.Info;
using Windows.System.Profile;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using Windows.Networking.Connectivity;
using System.Collections;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Net.Http;
using Windows.UI.Xaml;

namespace eDay
{
    public class ErrorStatus
    {
        public int success { get; set; }
        public string error_code { get; set; }
        public string error_description { get; set; }
        public string error_for_user { get; set; }
        public string working_time { get; set; }
        public override string ToString()
        {
            return error_description;
        }

    }
    public class LoginData
    {
        public int success { get; set; }
        public string token { get; set; }
        public string client_id { get; set; }
        public int new_notifications_count { get; set; }
        public int not_confirmed_events_count { get; set; }
        public float working_time { get; set; }
        public override string ToString()
        {
            return token;
        }


    }

    /// <summary>
    /// Универсальная модель данных элементов.
    /// </summary>
    //public class Event : INotifyPropertyChanged
    //{
    //    public Event(double event_id, string img, string time, string expert, string caption, bool unscheduled, string items_count, double confirmed)
    //    {
    //        Event_id = event_id;
    //        Img = img;
    //        Time = time;
    //        Expert = expert;
    //        Caption = caption;
    //        Items = new ObservableCollection<Items>();
    //        Confirmed = confirmed;
    //        Items_count = items_count;
    //        Unscheduled = unscheduled;
    //    }
    //    private string _caption;
    //    private double _confirmed;
    //    public double Event_id { get; set; }
    //    public string Img { get; set; }
    //    public string Time { get; set; }
    //    public string Expert { get; set; }
    //    public string Caption
    //    {
    //        get
    //        {
    //            return _caption;
    //        }
    //        set
    //        {
    //            if (Caption != value)
    //            {
    //                _caption = value;
    //                OnPropertyChanged("caption");
    //            }
    //        }
    //    }
    //    //public int confirmed { get; set; }
    //    public double Confirmed
    //    {
    //        get
    //        {
    //            return _confirmed;
    //        }
    //        set
    //        {
    //            if (_confirmed != value)
    //            {
    //                _confirmed = value;
    //                OnPropertyChanged("_confirmed");
    //            }
    //        }
    //    }
    //    public string Items_count { get; set; }
    //    public ObservableCollection<Items> Items { get; set; }
    //    public bool Unscheduled { get; set; }
    //    //public double _Class { get; set; }
    //    public override string ToString()
    //    {
    //        return Caption;
    //    }
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void OnPropertyChanged(string info)
    //    {
    //        PropertyChangedEventHandler handler = PropertyChanged;
    //        if (handler != null)
    //        {
    //            handler(this, new PropertyChangedEventArgs(info));
    //        }
    //    }

    //}

    /// <summary>
    /// Универсальная модель данных групп.
    /// </summary>
    public class Events
    {
        public Events()
        {
            events = new ObservableCollection<EventsByDay>();
        }
        public int success { get; set; }
        public string last_events_update { get; set; }
        public ObservableCollection<EventsByDay> events { get; set; }
        public bool DevMode { get; set; }
        public Result result { get; set; }
        public Debug debug { get; set; }
        public override string ToString()
        {
            return "Events count " + events.Count;
        }
    }
    public class EventsByDay : ICollection<Event>
    {
        public EventsByDay()
        {
            eventsByDay = new ObservableCollection<Event>();
        }

        public int Count
        {
            get
            {
                return ((ICollection<Event>)eventsByDay).Count;
            }
        }

        public string Date
        {
            get { return DateTime.Parse(eventsByDay[0].date).ToString("dd.MM.yyyy"); }
        }
        public string Day
        {
            get { return DateTime.Parse(eventsByDay[0].date).ToString("dddd"); }
        }

        public ObservableCollection<Event> eventsByDay { get; set; }

        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<Event>)eventsByDay).IsReadOnly;
            }
        }

        public void Add(Event item)
        {
            ((ICollection<Event>)eventsByDay).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Event>)eventsByDay).Clear();
        }

        public bool Contains(Event item)
        {
            return ((ICollection<Event>)eventsByDay).Contains(item);
        }

        public void CopyTo(Event[] array, int arrayIndex)
        {
            ((ICollection<Event>)eventsByDay).CopyTo(array, arrayIndex);
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return ((ICollection<Event>)eventsByDay).GetEnumerator();
        }

        public bool Remove(Event item)
        {
            return ((ICollection<Event>)eventsByDay).Remove(item);
        }

        //public Event this[int index]
        //{
        //    get
        //    {
        //        return ((IList<Event>)eventsByDay)[index];
        //    }

        //    set
        //    {
        //        ((IList<Event>)eventsByDay)[index] = value;
        //    }
        //}

        //public int Count
        //{
        //    get
        //    {
        //        return ((IList<Event>)eventsByDay).Count;
        //    }
        //}
        //public bool IsReadOnly
        //{
        //    get
        //    {
        //        return ((IList<Event>)eventsByDay).IsReadOnly;
        //    }
        //}

        //public void Add(Event item)
        //{
        //    ((IList<Event>)eventsByDay).Add(item);
        //}

        //public void Clear()
        //{
        //    ((IList<Event>)eventsByDay).Clear();
        //}

        //public bool Contains(Event item)
        //{
        //    return ((IList<Event>)eventsByDay).Contains(item);
        //}

        //public void CopyTo(Event[] array, int arrayIndex)
        //{
        //    ((IList<Event>)eventsByDay).CopyTo(array, arrayIndex);
        //}

        //public IEnumerator<Event> GetEnumerator()
        //{
        //    return ((IList<Event>)eventsByDay).GetEnumerator();
        //}

        //public int IndexOf(Event item)
        //{
        //    return ((IList<Event>)eventsByDay).IndexOf(item);
        //}

        //public void Insert(int index, Event item)
        //{
        //    ((IList<Event>)eventsByDay).Insert(index, item);
        //}

        //public bool Remove(Event item)
        //{
        //    return ((IList<Event>)eventsByDay).Remove(item);
        //}

        //public void RemoveAt(int index)
        //{
        //    ((IList<Event>)eventsByDay).RemoveAt(index);
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IList<Event>)eventsByDay).GetEnumerator();
        //}
        public override string ToString()
        {
            return eventsByDay[0].date.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<Event>)eventsByDay).GetEnumerator();
        }
    }
    public class Item
    {
        public string id { get; set; }
        public string event_id { get; set; }
        public string item_type_id { get; set; }
        public string reference_item_id { get; set; }
        public string dish_count { get; set; }
        public object measure { get; set; }
        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbs { get; set; }
        public string cellulose { get; set; }
        public string kkal { get; set; }
        public object repeats { get; set; }
        public object sets { get; set; }
        public object rest { get; set; }
        public object exercises_count { get; set; }
        public string caption { get; set; }
        public override string ToString()
        {
            return caption;
        }

    }
    public class Event : INotifyPropertyChanged
    {
        private double _confirmed;

        private int _event_class;
        public int event_class
            { get { return _event_class; }
              set
                {
                _event_class = value;
                switch (_event_class)
                {
                    case 1:
                        ColorEvent = new SolidColorBrush(Color.FromArgb(0,255,100,80));
                        break;
                    case 2:
                        ColorEvent = new SolidColorBrush(Color.FromArgb(0, 100, 50, 80));
                        break;
                    case 3:
                        ColorEvent = new SolidColorBrush(Color.FromArgb(0, 150, 150, 180));
                        break;
                    case 9:
                        ColorEvent = new SolidColorBrush(Color.FromArgb(0, 255, 255, 80));
                        break;
                }
            }
        }
        public int id { get; set; }
        public Img img { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string event_name { get; set; }
        public string expert_name { get; set; }
        public int confirmed { get; set; }
        public double Confirmed
        {
            get
            {
                return _confirmed;
            }
            set
            {
                if (_confirmed != value)
                {
                    _confirmed = value;
                    OnPropertyChanged("_confirmed");
                }
            }
        }

        public string comment { get; set; }
        public Details details { get; set; }
        public string data_md5 { get; set; }
        public SolidColorBrush ColorEvent { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1} - {2}", date, time, event_name);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

    }
    public class Result
    {
        public ObservableCollection<object> errors { get; set; }
        public ObservableCollection<object> warnings { get; set; }
        public ObservableCollection<object> notifies { get; set; }
    }
    public class Messages
    {
        public ObservableCollection<object> errors { get; set; }
        public ObservableCollection<object> warnings { get; set; }
        public ObservableCollection<object> notifies { get; set; }
    }
    public class Img
    {
        public object path { get; set; }
        public object md5 { get; set; }
    }
    public class Debug
    {
        public string client_id { get; set; }
        public double runtime { get; set; }
        public string script { get; set; }
        public int queries { get; set; }
        public Messages messages { get; set; }
        public int responsesize { get; set; }
    }
    public class Performer
    {
        public string id { get; set; }
        public string expert_name { get; set; }
        public string expert_login { get; set; }
        public string avatar { get; set; }
        public string expert_sex { get; set; }
        public string task_id { get; set; }
        public string expert_id { get; set; }
        public string mail { get; set; }
    }
    public class Details
    {
        public Details()
        {
            items = new ObservableCollection<Item>();
            performers=new ObservableCollection<Performer>();
        }

        public string proteins { get; set; }
        public string fats { get; set; }
        public string carbs { get; set; }
        public string cellulose { get; set; }
        public string kkal { get; set; }
        public ObservableCollection<Item> items { get; set; }
        public string descr { get; set; }
        public object assoc_client { get; set; }
        public ObservableCollection<Performer> performers { get; set; }
        public override string ToString()
        {
            string sBuffer = string.Empty;
            foreach(Item i in items)
            {
                sBuffer += "- " + i.caption + "\r\n";
            }
            return sBuffer;
        }

    }
    //public class Event : INotifyPropertyChanged
    //{
    //    public string Caption
    //    {
    //        get
    //        {
    //            return _caption;
    //        }
    //        set
    //        {
    //            if (Caption != value)
    //            {
    //                _caption = value;
    //                OnPropertyChanged("caption");
    //            }
    //        }
    //    }
    //    public override string ToString()
    //    {
    //        return Caption;
    //    }
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void OnPropertyChanged(string info)
    //    {
    //        PropertyChangedEventHandler handler = PropertyChanged;
    //        if (handler != null)
    //        {
    //            handler(this, new PropertyChangedEventArgs(info));
    //        }
    //    }

    //}
    public static class Everyday
    {
        private static LoginData loginData = new LoginData();
        private static ErrorStatus errStatus = new ErrorStatus();
        public static string OSVersion = "Windows Phone";//Environment.OSVersion.ToString();
        public static string SERVER = "http://api.go.pl.ua/"; //"http://everyday.mk.ua/";
        public static List<string> listNotrfications = new List<string>();
        public static string deviceID
        {
            get
            {
                return GetDeviceID2();
            }
        }
        private static string response { get; set; }
        //private static string qry;
        private const string quote = "\"";
        public static int SUCCESS
        {
            get; set;
        }
        public static string Token
        {
            get
            {
                return loginData.token;
            }
        }
        public static string GetDataFromString(string sParametr, string StringResponse)
        {
            int i;
            int j;
            int k;
            i = (StringResponse.IndexOf(sParametr) + 1);
            if ((i > 0))
            {
                j = (StringResponse.IndexOf(":", (i - 1), StringComparison.Ordinal) + 1);
                k = (StringResponse.IndexOf(",", (j - 1), StringComparison.Ordinal) + 1);
                return StringResponse.Substring(j, (k - (j - 1))).Trim('\"', ',');
            }
            return "NoData";
        }
        private static string GetDeviceID2()
        {
            HardwareToken token = HardwareIdentification.GetPackageSpecificToken(null);
            IBuffer hardwareId = token.Id;

            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm("MD5");
            IBuffer hashed = hasher.HashData(hardwareId);

            string hashedString = CryptographicBuffer.EncodeToHexString(hashed);
            return hashedString;
        }
        private static string GetDeviceId()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes).Replace("-", "");
        }//Note: This function may throw an exception. 
        public static async Task LoginEveryday()
        {
            LoginDialog loginDialog = new LoginDialog();
            login_now:
            // Show Dialog если залогинились -> 
            var result = await loginDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await Login(loginDialog.Login, loginDialog.Password);
                ErrorStatus res = JsonConvert.DeserializeObject<ErrorStatus>(response) as ErrorStatus;
                if (res.success == 0)
                {
                    MessageDialog msgbox = new MessageDialog(res.error_for_user, "Ошибка!");
                    await msgbox.ShowAsync();
                    goto login_now;
                }
                loginData = JsonConvert.DeserializeObject<LoginData>(response);
                //Взять события на 5 дней
                //await GetEvents(DateTime.Today.ToString("yyyy-MM-dd"), (DateTime.Today+TimeSpan.FromDays(5)).ToString("yyyy-MM-dd"));
                //await Task.Delay(5000);
            }
        }
        private static async Task Login(string sLog, string sPass)
        {
            string postData=
            "&Devid=" + deviceID + 
            "&Platform=" + OSVersion +
            "&Query={\"login\":\"" + sLog + "\",\"pass\":\"" + sPass + "\"}";
            try
            {
                HttpClient client = new HttpClient();
                //response = await client.GetStringAsync(new Uri(postData));
                HttpResponseMessage resp = await client.PostAsync(new Uri(SERVER + "ios/Login.php?"), new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded"));
                resp.EnsureSuccessStatusCode();
                response = await resp.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Данные берутся с сервера. Скрипт rGetEvents.php
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="save_data"></param>
        /// <returns></returns>
        public static async Task GetEvents(string startDate, string endDate, bool save_data=true)
        {

            if (loginData == null) return;
            string postData = string.Format("Token={0}&Devid={1}&Platform={2}&Query={{\"date_start\":\"{3}\",\"date_end\":\"{4}\", \"grouped\":true}}",
                                        loginData.token, "bsm10", "Win 8.1", startDate, endDate);
            HttpClient client = new HttpClient();

            //Этот метод посылает GET запрос
            //response = await client.GetStringAsync(new Uri(SERVER + "ios/rGetEvents.php?" + postData));

            HttpResponseMessage resp = await client.PostAsync(new Uri(SERVER + "ios/rGetEvents.php?"), new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded") );
            resp.EnsureSuccessStatusCode();
            response = await resp.Content.ReadAsStringAsync();
            if (save_data) await saveStringToLocalFile(response);
        }
        public static async Task ConfirmEvent(Event eventForConfirm, int confirmed = 1)
        {
            if (loginData == null) return;
            string postData = string.Format("Token={0}&Devid={1}&Platform={2}&Query={{\"event_id\":{3},\"event_class\":{4}, \"confirmed\": {5}}}",
                                        loginData.token, "bsm10", "Win 8.1", eventForConfirm.id.ToString(), eventForConfirm.event_class.ToString(), confirmed.ToString());
            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.PostAsync(new Uri(SERVER + "ios/rConfirmEvent.php?"), new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded"));
            resp.EnsureSuccessStatusCode();
            response = await resp.Content.ReadAsStringAsync();
        }

        private static async Task saveStringToLocalFile(string content)
        {
            Uri fileUri = new Uri("ms-appx:///DataModel/eDayData.json");
            byte[] fileBytes = Encoding.UTF8.GetBytes(content.ToCharArray());
            try
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(fileUri);
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception ex)
            {
                string f = ex.ToString();
            }
        }

        private static async Task<string> readStringFromLocalFile(string filename)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            Stream stream = await local.OpenStreamForReadAsync(filename);
            string text;
            using (StreamReader reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

        /// <summary>
        /// Проверяет есть ли подключение к интернету. Возвращает Истину если Да
        /// </summary>
        /// <returns></returns>
        public static bool InternetAvailable()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null &&
                    connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }
    }

    public class IntToBool : IValueConverter
    {
        // This converts the DateTime object to the string to display.
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is int)
            {
                var val = (int)value; return (val == 0) ? false : true;
            }
            return null;
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is bool)
            {
                var val = (bool)value; return val ? 1 : 0;
            }
            return null;
            //throw new NotImplementedException();
        }
    }
    public class ClassToColor : IValueConverter
    {
        // This converts the Class (int) to the string to Color like ""#FF00D0"".
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string strColor = string.Empty;
            if (value != null && value is int)
            {
                var val = (int)value;
                switch (val)
                {
                    case 1:
                        strColor = "#FF7194BF";
                        break;
                    case 2:
                        strColor = "#FFB872A4";
                        break;
                    case 3:
                        strColor = "#FFFF9B49";
                        break;
                    case 9:
                        strColor = "#FF75A456";
                        break;
                }

                return strColor;
            }
            return null;
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class CheckToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is bool)
            {
                var val = (bool)value; return (val == false) ? Visibility.Visible : Visibility.Collapsed;
            }
            return null;
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class ConfirmedToOpacity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is int)
            {
                var val = (int)value;
                return (val == 0) ? 1 : 0.65;
            }
            return null;
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
