using ClientLangMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLangMatch.Services
{
    public interface IUserDataService
    {
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        //-----Tasks complementares

        Task<List<Article>> GetAllArticlesOfAnUser(int userId);
        Task<List<Post>> GetAllPostsOfAnUser(int userId);
        Task<List<Language>> GetAllLanguagesOfAnUser(int userId);
        Task<List<StudyLog>> GetAllStudyLogsOfAnUser(int userId);

    }
}
