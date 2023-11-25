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
    public class StudyLogDataService : IStudyLogDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public StudyLogDataService()
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


        public async Task AddStudyLogAsync(StudyLog studyLog, int userId, int langId)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize<StudyLog>(studyLog, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/StudyLog?userId={userId}&languageId={langId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> StudyLog sucessfuly created!");
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

        public async Task DeleteStudyLogAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/StudyLog/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("-----> StudyLog sucessfuly deleted!");
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

        public async Task<List<StudyLog>> GetAllStudyLogsAsync()
        {
            List<StudyLog> studyLogs = new List<StudyLog>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return studyLogs;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/StudyLog");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    studyLogs = JsonSerializer.Deserialize<List<StudyLog>>(content, _jsonSerializerOptions);
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

            return studyLogs;
        }

        public async Task<StudyLog> GetStudyLogAsync(int id)
        {
            StudyLog studyLog = new StudyLog();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("-----> No internet access!");
                return studyLog;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/StudyLog/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    studyLog = JsonSerializer.Deserialize<StudyLog>(content, _jsonSerializerOptions);
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

            return studyLog;
        }
    }
}
