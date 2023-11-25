using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientLangMatch.Services
{
    public class PostDataService : IPostDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public PostDataService()
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

        public async Task AddPostAsync(Post post, int userId)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize<Post>(post, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Post?userId={userId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> Post sucessfuly created!");
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

        public async Task DeletePostAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/Post/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> Post sucessfuly deleted!");
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

        public async Task<List<Post>> GetAllPostsAsync()
        {
            List<Post> posts = new List<Post>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return posts;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/Post");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    posts = JsonSerializer.Deserialize<List<Post>>(content, _jsonSerializerOptions);
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

            return posts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            Post post = new Post();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return post;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/Post/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    post = JsonSerializer.Deserialize<Post>(content, _jsonSerializerOptions);
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

            return post;
        }
    }
}
