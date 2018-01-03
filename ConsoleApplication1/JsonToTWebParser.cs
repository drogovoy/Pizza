using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class JsonToTWebParser
    {

        public T GetRecordFromJson<T>(string jsonFileUrl) where T : new()
        {
            using (WebClient w = new WebClient())
            {
                string json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(jsonFileUrl);
                }
                catch (Exception)
                {
                    // do something here
                }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        public async Task<IEnumerable<T>> GetRecordsFromJsonAsync<T>(string jsonFileUrl) where T : new()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(jsonFileUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        string json_data = await content.ReadAsStringAsync();
                        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<IEnumerable<T>>(json_data) : new List<T>();
                    }
                }
            }

        }
        public IEnumerable<T> GetRecordsFromJson<T>(string jsonFileUrl) where T : new()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(jsonFileUrl).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string json_data = content.ReadAsStringAsync().Result;
                        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<IEnumerable<T>>(json_data) : new List<T>();
                    }
                }
            }

        }
    }
}
