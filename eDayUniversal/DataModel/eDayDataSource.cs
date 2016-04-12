using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// Модель данных, определяемая этим файлом, служит типичным примером строго типизированной
// по умолчанию.  Имена свойств совпадают с привязками данных из стандартных шаблонов элементов.
//
// Приложения могут использовать эту модель в качестве начальной точки и добавлять к ней дополнительные элементы или полностью удалить и
// заменить ее другой моделью, соответствующей их потребностям. Использование этой модели позволяет повысить качество приложения 
// скорость реагирования, инициируя задачу загрузки данных в коде программной части для App.xaml, если приложение 
// запускается впервые.

namespace eDay.Data
{

    /// <summary>
    /// Создает коллекцию групп и элементов с содержимым, считываемым из статического JSON-файла.
    /// 
    /// eDayDataSource инициализируется данными, считываемыми из статического JSON-файла, включенного в 
    /// проект.  Предоставляет пример данных как во время разработки, так и во время выполнения.
    /// </summary>
    public sealed class eDayDataSource
    {
        private static eDayDataSource _eventsDataSource = new eDayDataSource();
        private static Events group_events { get; set; }
        private static ObservableCollection<EventsByDay> _events = new ObservableCollection<EventsByDay>();
        public ObservableCollection<EventsByDay> events
        {
            get
            {
                ReplaceImg();
                return _events;
            }
        }
        private static void ReplaceImg()
        {
            foreach (EventsByDay evDay in _events)
            {
                foreach (Event ev in evDay)
                {
                    switch (ev.event_class)
                    {
                        case 1:
                            ev.img.path = "ms-appx:///Assets/new-1.png";
                            break;
                        case 2:
                            ev.img.path = "ms-appx:///Assets/new-2.png";
                            break;
                        case 3:
                            ev.img.path = "ms-appx:///Assets/new-3.png";
                            break;
                        case 9:
                            ev.img.path = "ms-appx:///Assets/new-9.png";
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает группу событий на определенную дату. Данные берутся из файла eDayData.json  
        /// </summary>
        /// <param name="day_date"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Event>> GetEventsByDateAsync(string day_date)
        {
            await GetGroupsEventsAsync();
            var matches = from IEnumerable<Event> ev in group_events.events where ev.First().date.Equals(day_date) select ev;
            if (matches.Count() > 0) return matches.First(); 
            return null;
        }
        /// <summary>
        /// Получает данные одного, выбранного события по его id. Данные берутся из файла eDayData.json
        /// </summary>
        /// <param name="idEvent"></param>
        /// <returns></returns>
        public static Event GetEvent(int idEvent)
        {
            var matches = from Event ev in _eventsDataSource.events
                          where ev.id.Equals(idEvent)
                          select ev;
            if (matches.Count() > 0) return matches.First();
            return null;
        }
        /// <summary>
        /// Возвращает коллекцию групп событий по дням. Данные берутся из файла eDayData.json
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<EventsByDay>> GetGroupsEventsAsync()
        {
            Uri dataUri = new Uri("ms-appx:///DataModel/eDayData.json");
            try
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText= string.Empty;
                if (file != null)
                {
                    IBuffer buffer = await FileIO.ReadBufferAsync(file);
                    DataReader reader = DataReader.FromBuffer(buffer);
                    byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                    reader.ReadBytes(fileContent);
                    Encoding encoding = Portable.Text.Encoding.GetEncoding(1251);
                    jsonText = encoding.GetString(fileContent, 0, fileContent.Length);
                }
                group_events =
                    JsonConvert.DeserializeObject<Events>(jsonText);
                _events = group_events.events;
                ReplaceImg();
            }
            catch (Exception e)
                {
#if DEBUG
                MessageDialog d = new MessageDialog(e.Message + " appx:///DataModel/eDayData.json");
                await d.ShowAsync();
#endif
                // file not found
            }

            return group_events.events;

        }
    }


}