using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using ClientLangMatch.Models;

namespace ClientLangMatch.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public UserDataService()
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
        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return users;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/User");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    users = JsonSerializer.Deserialize<List<User>>(content, _jsonSerializerOptions);
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

            return users;
        }


        public async Task AddUserAsync(User user)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize<User>(user, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/User", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> User sucessfuly created!");
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

        public async Task DeleteUserAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> User sucessfuly deleted!");
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


        public async Task UpdateUserAsync(User user)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize<User>(user, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/User/userId?userId={user.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> User sucessfuly created!");
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

        //-------------------------------------------

        public async Task<List<Article>> GetAllArticlesOfAnUser(int userId)
        {
            List<Article> userArticles = new List<Article>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return userArticles;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/User/articles/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    userArticles = JsonSerializer.Deserialize<List<Article>>(content, _jsonSerializerOptions);
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

            return userArticles;
        }

        public async Task<List<Post>> GetAllPostsOfAnUser(int userId)
        {
            List<Post> userPosts = new List<Post>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return userPosts;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/User/post/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    userPosts = JsonSerializer.Deserialize<List<Post>>(content, _jsonSerializerOptions);
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

            return userPosts;
        }

        public async Task<List<Language>> GetAllLanguagesOfAnUser(int userId)
        {
            List<Language> userLanguages = new List<Language>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return userLanguages;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/User/laguage/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    userLanguages = JsonSerializer.Deserialize<List<Language>>(content, _jsonSerializerOptions);
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

            return userLanguages;
        }

        public async Task<List<StudyLog>> GetAllStudyLogsOfAnUser(int userId)
        {
            List<StudyLog> userStudyLogs = new List<StudyLog>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return userStudyLogs;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/User/studyLog/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    userStudyLogs = JsonSerializer.Deserialize<List<StudyLog>>(content, _jsonSerializerOptions);
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

            return userStudyLogs;
        }
    }
}
