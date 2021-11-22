//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: HttpClient.cs
//摘要: HttpClient
//说明: 该功能需要用IHttpFactory改写
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System.Text;
using System.Linq;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

using iMaxSys.Max.Json.NamingPolicy;

namespace iMaxSys.Max.Net.Http
{
    /// <summary>
    /// HttpClient
    /// </summary>
    public static class HttpClient
    {
        private static JsonSerializerOptions _snakeJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All),
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        };

        private static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };


        public static async Task<string> Post(string url, Dictionary<string, string> data)
        {
            return await Post(url, null, data);
        }

        public static async Task<T?> Get<T>(string url, bool isSnakeFormatter)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                string result = await client.GetStringAsync(url);
                if (isSnakeFormatter)
                {
                    return JsonSerializer.Deserialize<T>(result, _snakeJsonSerializerOptions);
                }
                else
                {
                    return JsonSerializer.Deserialize<T>(result, _jsonSerializerOptions);
                }
            }
        }

        public static async Task<string> Get(string url, Dictionary<string, string> data)
        {
            string uri = "";
            if (data != null && data.Count > 0)
            {
                string query = string.Join("&", data.Select(x => $"{x.Key}={x.Value}"));
                uri = $"{url}?{query}";
            }
            else
            {
                uri = url;
            }
            using (var client = new System.Net.Http.HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }

        public static async Task<string> Post(string url, string credentials, Dictionary<string, string> data)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (!string.IsNullOrWhiteSpace(credentials))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                }
                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<T> Post<T>(string url, Dictionary<string, string> data, bool isSnakeFormatter)
        {
            return await Post<T>(url, null, data, isSnakeFormatter);
        }

        public static async Task<T?> Post<T>(string url, string credentials, Dictionary<string, string> data, bool isSnakeFormatter)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (credentials != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                }

                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));
                string json = await response.Content.ReadAsStringAsync();

                if (isSnakeFormatter)
                {
                    //return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { ContractResolver = new UnderscoreNamesContractResolver() });
                    return JsonSerializer.Deserialize<T>(json, _snakeJsonSerializerOptions);
                }
                else
                {
                    //return JsonConvert.DeserializeObject<T>(json);
                    return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
                }
            }
        }

        /// <summary>
        /// Post提交Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<T> PostJson<T>(string url, string json)
        {
            return await PostJson<T>(url, json, false);
        }

        /// <summary>
        /// Post提交Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<T?> PostJson<T>(string url, string json, bool isSnakeFormatter)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (isSnakeFormatter)
                {
                    return JsonSerializer.Deserialize<T>(result, _snakeJsonSerializerOptions);
                }
                else
                {
                    return JsonSerializer.Deserialize<T>(result, _jsonSerializerOptions);
                }
            }
        }

        /// <summary>
        /// Post提交Json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static async Task<string> PostXml(string url, string xml)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
                var response = await client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
                //XmlDocument xmlDocument = new XmlDocument();
                //xmlDocument.LoadXml(result);
                //string json = JsonConvert.SerializeXmlNode(xmlDocument);
                //return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { ContractResolver = new UnderscoreNamesContractResolver() });
            }
        }
    }
}