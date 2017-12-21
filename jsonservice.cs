using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonService
    {
        private readonly JsonSerializer _serializer;

        public JsonService()
        {
            _serializer = new JsonSerializer();
        }
        
        //Generic method for deserializing a stream HttpResponse
        public async Task<T> CreateObjectFromHttpResponse<T>(HttpResponseMessage response)
        {

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var json = new JsonTextReader(reader))
                    {
                        return _serializer.Deserialize<T>(json);
                    }
                }
            }

        }

        //Convert object inro string content to send with post requests
        public StringContent ConvertObjectToStringContent(object obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            return new StringContent(data, Encoding.UTF8, "application/json");
        }
    }
