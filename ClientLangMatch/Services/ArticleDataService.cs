using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Windows.System;

namespace ClientLangMatch.Services
{
    public class ArticleDataService : IArticleDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ArticleDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5277" : "https://localhost:7283";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        //-------------------------------------------

        public async Task AddArticleAsync(Article article, int userId, int langId)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize<Article>(article, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Article?userId={userId}&languageId={langId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> Article sucessfuly created!");
                }
                else
                {
                    Debug.WriteLine("-----> No successfull status code!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"------> Temos uma execessão: {ex.Message}");
            }
        }

        public async Task DeleteArticleAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/Article/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> Article sucessfuly deleted!");
                }
                else
                {
                    Debug.WriteLine("-----> No successfull status code!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"------> Temos uma execessão: {ex.Message}");
            }

            return;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            List<Article> articles = new List<Article>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return articles;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/Article");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    articles = JsonSerializer.Deserialize<List<Article>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("-----> No successfull status code!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"------> Temos uma execessão: {ex.Message}");
            }

            return articles;
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            Article article = new Article();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return article;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/Article/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    article = JsonSerializer.Deserialize<Article>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("-----> No successfull status code!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"------> Temos uma execessão: {ex.Message}");
            }

            return article;
        }
    }
}
